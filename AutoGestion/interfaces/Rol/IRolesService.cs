using System.Runtime.CompilerServices;
using AutoGestion.models.Rol;

namespace AutoGestion.interfaces.Rol
{
    public interface IRolesService
    {
        Task<IEnumerable<RolDto>> GetRoles();
        Task<IEnumerable<RolDto>> GetRolesActivos();
        Task<RolDto> GetRolById(string id);
        Task<RolDto> PostRol(Role rol);
        Task<RolDto> PutRol(Role rol, string id);
        Task<bool> AssignPermissionsToRole(string roleId, List<string> ids);
    }
}
