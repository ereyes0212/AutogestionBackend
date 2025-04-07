using System.Net;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context); // Ejecuta la siguiente capa del middleware
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Se ha producido un error inesperado.");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        int statusCode;
        string message;
        string errorType = exception.GetType().Name;
        string details = exception.InnerException?.Message ?? exception.Message;

        switch (exception)
        {
            case FormatException:
                statusCode = (int)HttpStatusCode.BadRequest;
                message = "El formato de los datos enviados no es válido.";
                break;
            case NullReferenceException:
                statusCode = (int)HttpStatusCode.InternalServerError;
                message = "Se produjo un error al procesar la solicitud. Es posible que falten datos.";
                break;
            case BadHttpRequestException:
                statusCode = (int)HttpStatusCode.BadRequest;
                message = "La solicitud enviada es inválida o está mal formada.";
                break;
            case TimeoutException:
                statusCode = (int)HttpStatusCode.RequestTimeout;
                message = "La operación ha excedido el tiempo de espera permitido.";
                break;
            case NotImplementedException:
                statusCode = (int)HttpStatusCode.NotImplemented;
                message = "Esta funcionalidad aún no está implementada.";
                break;
            case UnauthorizedAccessException:
                statusCode = (int)HttpStatusCode.Unauthorized;
                message = "No tienes permisos para acceder a este recurso.";
                break;
            case DbUpdateException dbEx when dbEx.InnerException is MySqlException mysqlEx && mysqlEx.Number == 1062:
                statusCode = (int)HttpStatusCode.Conflict;
                message = "Error: Ya existe un registro con los mismos datos únicos.";
                break;

            case KeyNotFoundException:
                statusCode = (int)HttpStatusCode.NotFound;
                message = "Recurso no encontrado.";
                break;

            case DbUpdateException:
                statusCode = (int)HttpStatusCode.InternalServerError;
                message = "Error en la base de datos.";
                break;

            case ArgumentException:
                statusCode = (int)HttpStatusCode.BadRequest;
                message = "Error en los datos de entrada.";
                break;

            case ValidationException validationEx:
                statusCode = (int)HttpStatusCode.BadRequest;
                message = "Se han producido uno o más errores de validación.";
                details = JsonSerializer.Serialize(validationEx.Errors);
                break;

            default:
                statusCode = (int)HttpStatusCode.InternalServerError;
                message = "Se ha producido un error inesperado.";
                break;
        }

        var errorResponse = new ErrorResponse(statusCode, message, errorType, details);
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
    }
}
