namespace AutoGestion.interfaces.ILogin
{
    public interface ILoginService
    {
        Task<string> Login(string username, string password);
        Task<string> ResetPassword(string username, string newPassword);
    }
}
