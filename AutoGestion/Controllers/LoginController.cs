using AutoGestion.interfaces.ILogin;
using AutoGestion.models;
using Microsoft.AspNetCore.Mvc;

namespace AutoGestion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDTo loginRequest)
        {
            if (loginRequest == null)
            {
                return BadRequest("Datos inválidos.");
            }

            try
            {
                // Realizar login y obtener el JWT
                var token = await _loginService.Login(loginRequest.username, loginRequest.password);
                return Ok(new { Token = token });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error en el servidor", details = ex.Message });
            }
        }        
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetDto loginRequest)
        {
            if (loginRequest == null)
            {
                return BadRequest("Datos inválidos.");
            }

            try
            {
                // Realizar login y obtener el JWT
                var token = await _loginService.ResetPassword(loginRequest.username, loginRequest.newPassword);
                return Ok(new { Token = token });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error en el servidor", details = ex.Message });
            }
        }
    }
}
