using System.Globalization;
using System.Text;
using AspNetCoreRateLimit;
using AutoGestion.interfaces.IEmailService;
using AutoGestion.Models;
using CarWashBackend.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
var supportedCultures = new[] { new CultureInfo("es-ES") };

var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("es-ES"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
};
builder.Configuration.AddEnvironmentVariables();

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(80);
});

//var mysqlHost = Environment.GetEnvironmentVariable("MYSQL_HOST");
//var mysqlDatabase = Environment.GetEnvironmentVariable("MYSQL_DATABASE");
//var mysqlUser = Environment.GetEnvironmentVariable("MYSQL_USER");
//var mysqlPassword = Environment.GetEnvironmentVariable("MYSQL_PASSWORD");
//var connectionString = $"server={mysqlHost};database={mysqlDatabase};uid={mysqlUser};pwd={mysqlPassword}";


// Definir las credenciales de la base de datos de forma fija
var mysqlHost = "localhost";
var mysqlDatabase = "AutoGestion_DB";
var mysqlUser = "root";
var mysqlPassword = "P@ssWord.123";
var connectionString = $"server={mysqlHost};port=3306;database={mysqlDatabase};uid={mysqlUser};pwd={mysqlPassword}";
//Construir el ConnectionString para MySQL (incluyendo el puerto 3306)
// Add services to the container.

var hondurasTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central America Standard Time");
TimeZoneInfo.ClearCachedData();
CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("es-HN");
CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("es-HN");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DbContextAutoGestion>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

builder.Services.AddSingleton<IEmailService>(provider =>
{
    var host = "mail.tiempo.hn";
    var port = 587; // o el puerto que uses
    var username = "erick.reyes@tiempo.hn";
    var password = "3372ac8yz$B3Ougrhh##tqUc";
    var from = username;

    return new SmtpEmailService(host, port, username, password, from);
});


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
    {
        policy.WithOrigins("https://carwash-front-end.vercel.app")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

builder.Services.AddAuthorization();

builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));
builder.Services.AddInMemoryRateLimiting();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
builder.Services.AddMemoryCache();
builder.Services.AddHttpContextAccessor();
builder.Services.AddServices();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<DbContextAutoGestion>();

    try
    {
        Console.WriteLine("Aplicando migraciones...");
        context.Database.Migrate();
        Console.WriteLine("Migraciones aplicadas correctamente.");

        var seeder = new Seeder(context, false);
        seeder.Seed();
        Console.WriteLine("Seeding completo.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error al aplicar migraciones o al ejecutar el seeder: {ex.Message}");
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRequestLocalization(localizationOptions);
app.UseHttpsRedirection();
app.UseMiddleware<ExceptionMiddleware>();

app.UseIpRateLimiting();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", () =>
{
    var utcNow = DateTime.UtcNow;
    var hondurasTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, hondurasTimeZone);

    return Results.Ok(new
    {
        message = "API Autogestión levantada correctamente",
        utcDate = utcNow.ToString("yyyy-MM-ddTHH:mm:ss"),
        localDate = hondurasTime.ToString("yyyy-MM-ddTHH:mm:ss")
    });
});
app.UseCors("AllowSpecificOrigin");
app.MapControllers();

app.Run();
