using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoGestion.interfaces;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;

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
            var claims = new List<Claim>();
            var properties = typeof(T).GetProperties();

            foreach (var property in properties)
            {
                var propertyValue = property.GetValue(data);
                if (propertyValue == null)
                    continue;

                // Si es una lista de strings, los agregamos individualmente.
                else if (propertyValue is IEnumerable<string> stringList)
                {
                    foreach (var s in stringList)
                    {
                        claims.Add(new Claim(property.Name, s));
                    }
                }
                else
                {
                    var valueString = propertyValue.ToString();
                    if (valueString != null)
                    {
                        claims.Add(new Claim(property.Name, valueString));
                    }
                }
            }

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

        public string GenerarPasswordAleatoria(int longitud = 10)
        {
            const string caracteres = "ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz23456789!@$?_-";
            var random = new Random();
            return new string(Enumerable.Range(0, longitud).Select(_ => caracteres[random.Next(caracteres.Length)]).ToArray());
        }
    }

}
