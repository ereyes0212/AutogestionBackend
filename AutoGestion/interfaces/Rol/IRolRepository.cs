using AutoGestion.models.Rol;
using AutoGestion.Models;
using Microsoft.AspNetCore.Mvc;

namespace AutoGestion.interfaces.Rol
{
    public interface IRolRepository
    {
        Task<IEnumerable<Role>> GetRoles();
        Task<IEnumerable<Permiso>> GetPermisos();
        Task<IEnumerable<Permiso>> GetPermisosRol();
        Task<IEnumerable<Role>> GetRolesActivos();
        Task<Role> GetRolesById(string id);
        Task<Role> PostRol(Role rol);
        Task<Role> PutRol(Role rol, string id);
        Task<bool> AssignPermissions(string roleId, List<string> permisoIds);
    }
}
