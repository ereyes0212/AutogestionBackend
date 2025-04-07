
using AutoGestion.models.Usuario;
using AutoGestion.Models;
namespace AutoGestion.interfaces.IUsuario
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<Usuario>> GetUsuarios();
        Task<IEnumerable<Usuario>> GetUsuariosActivos();
        Task<Usuario> GetUsuarioById(string id);
        Task<Usuario> PostUsuario(Usuario usuario);
        Task<Usuario> PutUsuario(Usuario usuario);
    }
}
