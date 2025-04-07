using AutoGestion.interfaces;
using AutoGestion.interfaces.Rol;
using AutoGestion.models.Rol;
using AutoGestion.Utils;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AutoGestion.services
{
    public class RolService: IRolesService
    {
        private readonly IRolRepository _rolrepository;
        private readonly IAsignaciones _AsinacionesService;
        public RolService (IRolRepository rolrepository, IAsignaciones asignacionesService)
        {
            _rolrepository = rolrepository;
            _AsinacionesService = asignacionesService;
        }

        public async Task<IEnumerable<RolDto>> GetRoles()
        {
            var roles = await _rolrepository.GetRoles();
            if (roles == null)
            {
                throw new KeyNotFoundException("Lista de roles Vacia.");
            }
            var rolesDto = roles.Select(r => new RolDto
            {
                Nombre = r.Nombre,
                Descripcion = r.Descripcion,
                Activo = r.Activo,
                Id = r.Id!  
            });
            return rolesDto;
        }
        public async Task<IEnumerable<RolDto>> GetRolesActivos()
        {
            var roles = await _rolrepository.GetRolesActivos();
            if (roles == null)
            {
                throw new KeyNotFoundException("Lista de roles Vacia.");
            }
            var rolesDto = roles.Select(r => new RolDto
            {
                Nombre = r.Nombre,
                Descripcion = r.Descripcion,
                Activo = r.Activo,
                Id = r.Id!,  
            });
            return rolesDto;
        }  
        
        public async Task<RolDto> GetRolById(string id)
        {
            var r = await _rolrepository.GetRolesById(id);
            if (r == null)
            {
                throw new KeyNotFoundException("Rol no encontrado.");
            }
            var rolesDto = new RolDto
            {
                Nombre = r.Nombre,
                Descripcion = r.Descripcion,
                Activo = r.Activo,
                Id = r.Id!,
            };
            return rolesDto;
        }
                
        public async Task<RolDto> PostRol(Role rol)
        {
            var token = _AsinacionesService.GetTokenFromHeader();
            rol.Id = _AsinacionesService.GenerateNewId();
            rol.Activo = true;
            rol.CreatedAt = _AsinacionesService.GetCurrentDateTime();
            rol.UpdatedAt = _AsinacionesService.GetCurrentDateTime();
            rol.adicionado_por = _AsinacionesService.GetClaimValue(token!, "User") ?? "Sistema";
            rol.modificado_por = _AsinacionesService.GetClaimValue(token!, "User") ?? "Sistema";
            await _rolrepository.PostRol(rol);
            var rolDto = new RolDto
            {
                Nombre = rol.Nombre,
                Descripcion= rol.Descripcion,
                Activo = rol.Activo,
                Id = rol.Id,
            };
            return rolDto;
        }        
        public async Task<RolDto> PutRol(Role rol, string id)
        {
            var token = _AsinacionesService.GetTokenFromHeader();
            var rolFound = await _rolrepository.GetRolesById(id);
            if (rolFound == null)
            {
                throw new KeyNotFoundException("Rol no encontrado.");
            }
            rolFound.ActualizarPropiedades(rol);
            rolFound.UpdatedAt = _AsinacionesService.GetCurrentDateTime();
            rolFound.modificado_por = _AsinacionesService.GetClaimValue(token!, "User") ?? "Sistema";
            rolFound.Activo = rol.Activo;
            await _rolrepository.PutRol(rolFound, id);
            return new RolDto
            {
                Nombre = rolFound.Nombre,
                Descripcion = rolFound.Descripcion,
                Activo = rolFound.Activo,
                Id = rolFound.Id!,
            };
        }        
        public async Task<bool> AssignPermissionsToRole(string id, List<string> ids)
        {
            var r = await _rolrepository.GetRolesById(id);
            if (r == null)
            {
                throw new KeyNotFoundException("Rol no encontrado.");
            }
            return await _rolrepository.AssignPermissions(id, ids);
        }

    }
}
