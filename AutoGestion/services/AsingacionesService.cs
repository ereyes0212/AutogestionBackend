using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoGestion.interfaces;
using Microsoft.IdentityModel.Tokens;

namespace AutoGestion.services

{
    public class AsingacionesService : IAsignaciones
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AsingacionesService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }
        public string GenerateNewId()
        {
            return Guid.NewGuid().ToString();
        }        
        public DateTime GetCurrentDateTime()
        {
            return DateTime.Now;
        }
        public string EncriptPassword(string password)
        {
            var encriptPassword = BCrypt.Net.BCrypt.HashPassword(password);
            return encriptPassword;
        }
        public bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
        public string GenerateJwtToken<T>(T data)
        {
            // Lista de Claims que se generarán dinámicamente
            var claims = new List<Claim>();

            // Usamos reflexión para obtener las propiedades de T
            var properties = typeof(T).GetProperties();

            foreach (var property in properties)
            {
                var value = property.GetValue(data)?.ToString();
                if (value != null)
                {
                    claims.Add(new Claim(property.Name, value));
                }
            }

            // Generar el token JWT con los claims obtenidos
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public string? GetClaimValue(string token, string claimType)
        {
            var handler = new JwtSecurityTokenHandler();

            if (!handler.CanReadToken(token))
                return null;

            var jwtToken = handler.ReadJwtToken(token);
            var claim = jwtToken.Claims.FirstOrDefault(c => c.Type == claimType);

            return claim?.Value;
        }

        public string? GetTokenFromHeader()
        {
            return _httpContextAccessor.HttpContext?.Request.Headers["Authorization"]
                .FirstOrDefault()?.Split(" ").Last();
        }
    }

}
