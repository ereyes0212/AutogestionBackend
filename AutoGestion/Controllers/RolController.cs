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
        public async Task<ActionResult<RoleWithPermissionsDto>> GetRolById(string id)
        {
            try
            {

                var roles = await _rolService.GetRolWithPermissionsById(id);
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
        [HttpGet("permisos")]
        public async Task<ActionResult<IEnumerable<RolDto>>> GetPermisos()
        {
            try
            {
                var roles = await _rolService.GetPermisos();
                if (roles == null || !roles.Any())
                    return NotFound("No se encontraron permisos.");
                return Ok(roles);
            }
            catch (Exception)
            {
                throw;
            }
        }        
        [HttpGet("permisosrol")]
        public async Task<ActionResult<IEnumerable<RolDto>>> GetPermisosRol()
        {
            try
            {
                var roles = await _rolService.GetPermisoRolDto();
                if (roles == null || !roles.Any())
                    return NotFound("No se encontraron permisos.");
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
        public async Task<ActionResult<RolDto>> CreateRoles([FromBody] RolCreateDto rolCreateDto)
        {
            try
            {
                // Llamamos al servicio pasando el DTO recibido
                var rolCreate = await _rolService.PostRol(rolCreateDto);

                // Retornamos el resultado que es un RolDto
                return Ok(rolCreate);
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateRoles(RolUpdateDto rolUpdateDto, string id)
        {
            try
            {
                var rolUpdate = await _rolService.PutRol(rolUpdateDto, id);
                return Ok(rolUpdate);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
