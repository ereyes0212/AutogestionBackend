namespace AutoGestion.interfaces
{
    public interface IAsignaciones
    {
        string GenerateNewId();
        DateTime GetCurrentDateTime();
        string EncriptPassword(string password);
        bool VerifyPassword(string password, string hashedPassword);
        string GenerateJwtToken<T>(T data);
        string? GetClaimValue(string token, string claimType);
        string? GetTokenFromHeader();
        public string GenerarPasswordAleatoria(int longitud);
    }

}
