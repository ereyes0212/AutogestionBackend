using System.Runtime.CompilerServices;
using AutoGestion.models.Rol;
using AutoGestion.Models.Rol;

namespace AutoGestion.interfaces.Rol
{
    public interface IRolesService
    {
        Task<IEnumerable<RolDto>> GetRoles();
        Task<IEnumerable<PermisoDto>> GetPermisos();
        Task<IEnumerable<PermisoRolDto>> GetPermisoRolDto();
        Task<IEnumerable<RolDto>> GetRolesActivos();
        Task<RoleWithPermissionsDto> GetRolWithPermissionsById(string id);
        Task<RolDto> PostRol(RolCreateDto rolCreateDto);
        Task<RolDto> PutRol(RolUpdateDto rolUpdateDto, string id);
        Task<bool> AssignPermissionsToRole(string roleId, List<string> ids);
    }
}
