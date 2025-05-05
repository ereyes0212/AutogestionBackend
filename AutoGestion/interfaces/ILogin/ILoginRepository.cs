using AutoGestion.Models;

namespace AutoGestion.interfaces.ILogin
{
    public interface ILoginRepository
    {
        Task<Usuario> GetUserByUsername(string username);
        Task<Usuario> ResetPassword(Usuario usuario);
        Task<List<string>> GetUserPermissions(string userId);
    }
}
