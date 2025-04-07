using AutoGestion.interfaces.Rol;
using AutoGestion.models.Rol;
using Microsoft.AspNetCore.Mvc;

namespace Inventario.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        private readonly IRolesService _rolService;

        public RolController(IRolesService rolService)
        {
            _rolService = rolService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RolDto>>> GetRoles()
        {
            try
            {

                var roles = await _rolService.GetRoles();
                return Ok(roles);
            }
            catch (Exception )
            {
                throw;
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<RolDto>> GetRolById(string id)
        {
            try
            {

                var roles = await _rolService.GetRolById(id);
                return Ok(roles);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet("activos")]
        public async Task<ActionResult<IEnumerable<RolDto>>> GetRolesActivos()
        {
            try
            {
                var roles = await _rolService.GetRolesActivos();
                if (roles == null || !roles.Any())
                    return NotFound("No se encontraron roles.");
                return Ok(roles);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost("{idRol}/asignar-permisos")]
        public async Task<ActionResult> AsignarPermisos(string idRol, List<string> idsPermisos)
        { 
            try
            {
                bool success = await _rolService.AssignPermissionsToRole(idRol, idsPermisos);
                if (success)
                {
                    return Ok("Permisos asignados correctamente.");
                }
                else
                {
                    return BadRequest("No se pudieron asignar todos los permisos.");
                }
            }
            catch (Exception)
            {
                throw;
            }


        }


        [HttpPost]
        public async Task<ActionResult<RolDto>> CreateRoles(Role rol)
        {
            try
            {
            var rolCreate = await _rolService.PostRol(rol);
            return Ok(rolCreate);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateRoles(string id, Role rol)
        {
            try
            {
            var rolUpdate = await _rolService.PutRol(rol, id);
            return Ok(rolUpdate);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
