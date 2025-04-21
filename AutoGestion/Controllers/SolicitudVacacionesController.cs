using AutoGestion.Models.SolicitudVacacionesDto;
using AutoGestion.interfaces.ISolicitudVacaciones;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutoGestion.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitudVacacionesController : ControllerBase
    {
        private readonly ISolicitudVacacionesService _solicitudService;

        public SolicitudVacacionesController(ISolicitudVacacionesService solicitudService)
        {
            _solicitudService = solicitudService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SolicitudVacacionDto>>> GetSolicitudes()
        {
            try
            {
                var solicitudes = await _solicitudService.GetSolicitudes();
                return Ok(solicitudes);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SolicitudVacacionDto>> GetSolicitudById(string id)
        {
            try
            {
                var solicitud = await _solicitudService.GetSolicitudById(id);
                return Ok(solicitud);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("empleado/{empleadoId}")]
        public async Task<ActionResult<IEnumerable<SolicitudVacacionDto>>> GetSolicitudesPorEmpleado(string empleadoId)
        {
            try
            {
                var solicitudes = await _solicitudService.GetSolicitudesPorEmpleado(empleadoId);
                return Ok(solicitudes);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult<SolicitudVacacionDto>> CrearSolicitud([FromBody] SolicitudVacacionCreateDto solicitud)
        {
            try
            {
                var nuevaSolicitud = await _solicitudService.CrearSolicitud(solicitud);
                return Ok(nuevaSolicitud);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut("{id}/aprobar")]
        public async Task<ActionResult<SolicitudVacacionDto>> ProcesarAprobacion(string id, [FromQuery] int nivel, [FromQuery] bool aprobado, [FromQuery] string comentarios)
        {
            try
            {
                var solicitudActualizada = await _solicitudService.ProcesarAprobacion(id, nivel, aprobado, comentarios);
                return Ok(solicitudActualizada);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
