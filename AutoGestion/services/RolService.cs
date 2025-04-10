using AutoGestion.interfaces;
using AutoGestion.interfaces.Rol;
using AutoGestion.models.Rol;
using AutoGestion.Models.Rol;
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
        public async Task<IEnumerable<PermisoDto>> GetPermisos()
        {
            var permisos = await _rolrepository.GetPermisos();
            if (permisos == null)
            {
                throw new KeyNotFoundException("Lista de permisos Vacia.");
            }
            var permispsDtp = permisos.Select(r => new PermisoDto
            {
                Nombre = r.Nombre,
                Descripcion = r.Descripcion,
                Activo = r.Activo,
                Id = r.Id!  
            });
            return permispsDtp;
        }        
        public async Task<IEnumerable<PermisoRolDto>> GetPermisoRolDto()
        {
            var permisos = await _rolrepository.GetPermisos();
            if (permisos == null)
            {
                throw new KeyNotFoundException("Lista de permisos Vacia.");
            }
            var permispsDtp = permisos.Select(r => new PermisoRolDto
            {
                Nombre = r.Nombre,
                Id = r.Id!  
            });
            return permispsDtp;
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

        public async Task<RoleWithPermissionsDto> GetRolWithPermissionsById(string id)
        {
            var role = await _rolrepository.GetRolesById(id);

            if (role == null)
            {
                throw new KeyNotFoundException("Rol no encontrado.");
            }

            // Aquí haces la conversión al DTO después de obtener la entidad desde el repositorio
            var roleDto = new RoleWithPermissionsDto
            {
                Id = role.Id,
                Nombre = role.Nombre,
                Descripcion = role.Descripcion,
                Activo = role.Activo,
                Permisos = role.RolePermisos.Select(rp => new PermisoRolDto
                {
                    Id = rp.Permiso.Id,
                    Nombre = rp.Permiso.Nombre
                }).ToList()
            };

            return roleDto;
        }



        public async Task<RolDto> PostRol(RolCreateDto rolCreateDto)
        {
            var token = _AsinacionesService.GetTokenFromHeader();

            // Mapeamos el DTO al objeto Role
            var rol = new Role
            {
                Id = _AsinacionesService.GenerateNewId(),
                Nombre = rolCreateDto.Nombre,
                Descripcion = rolCreateDto.Descripcion,
                Activo = rolCreateDto.Activo ?? true,  // Si no se pasa activo, por defecto se establece en true
                CreatedAt = _AsinacionesService.GetCurrentDateTime(),
                UpdatedAt = _AsinacionesService.GetCurrentDateTime(),
                adicionado_por = _AsinacionesService.GetClaimValue(token!, "User") ?? "Sistema",
                modificado_por = _AsinacionesService.GetClaimValue(token!, "User") ?? "Sistema"
            };

            // Creamos el rol (sin los permisos asociados aún)
            var createdRole = await _rolrepository.PostRol(rol);

            // Extraemos los Ids de los permisos desde el DTO
            var permisoIds = rolCreateDto.Permisos.Select(p => p.Id).ToList();

            // Llamamos al repositorio para asignar los permisos
            bool permisosAsignados = await _rolrepository.AssignPermissions(createdRole.Id, permisoIds);
            if (!permisosAsignados)
            {
                throw new Exception("Error al asignar permisos al rol.");
            }

            // Mapeamos la entidad creada a un DTO (esto incluye el rol, pero no los permisos)
            var rolDto = new RolDto
            {
                Id = createdRole.Id,
                Nombre = createdRole.Nombre,
                Descripcion = createdRole.Descripcion,
                Activo = createdRole.Activo,
            };

            return rolDto;
        }


        public async Task<RolDto> PutRol(RolUpdateDto rolUpdateDto, string id)
        {
            var token = _AsinacionesService.GetTokenFromHeader();

            // Obtenemos el rol existente desde el repositorio
            var rolFound = await _rolrepository.GetRolesById(id);
            if (rolFound == null)
            {
                throw new KeyNotFoundException("Rol no encontrado.");
            }

            rolFound.Nombre = rolUpdateDto.Nombre;
            rolFound.Descripcion = rolUpdateDto.Descripcion;
            rolFound.Activo = rolUpdateDto.Activo ?? rolFound.Activo;  
            rolFound.UpdatedAt = _AsinacionesService.GetCurrentDateTime();
            rolFound.modificado_por = _AsinacionesService.GetClaimValue(token!, "User") ?? "Sistema";

            await _rolrepository.PutRol(rolFound, id);

            var permisoIds = rolUpdateDto.Permisos.Select(p => p.Id).ToList();
            bool permisosActualizados = await _rolrepository.AssignPermissions(id, permisoIds);
            if (!permisosActualizados)
            {
                throw new Exception("No se pudieron actualizar los permisos del rol.");
            }

            var rolDto = new RolDto
            {
                Id = rolFound.Id!,
                Nombre = rolFound.Nombre,
                Descripcion = rolFound.Descripcion,
                Activo = rolFound.Activo
            };

            return rolDto;
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
