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

                // Si la propiedad se llama "Empresas" y es una colección de objetos, iteramos para agregar un claim por cada elemento.
                if (property.Name == "Empresas" && propertyValue is IEnumerable<object> empresas)
                {
                    foreach (var empresa in empresas)
                    {
                        // Obtener las propiedades "id" y "nombre" del objeto empresa
                        var idProp = empresa.GetType().GetProperty("id")?.GetValue(empresa)?.ToString();
                        var nombreProp = empresa.GetType().GetProperty("nombre")?.GetValue(empresa)?.ToString();

                        if (!string.IsNullOrEmpty(idProp) && !string.IsNullOrEmpty(nombreProp))
                        {
                            // Agregar un claim con el valor formateado, por ejemplo "id|nombre"
                            claims.Add(new Claim("Empresas", $"{idProp}|{nombreProp}"));
                        }
                    }
                }
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
    }

}
