using System.Collections.Generic;
using System.Threading.Tasks;
using AutoGestion.Models.SolicitudVacacionesDto;
using AutoGestion.Interfaces.ISolicitudVacaciones;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoGestion.services;

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

        // GET api/solicitudvacaciones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SolicitudVacacionDto>>> GetSolicitudesAsync()
        {
            var solicitudes = await _solicitudService.GetSolicitudesAsync();
            return Ok(solicitudes);
        }

        // GET api/solicitudvacaciones/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<SolicitudVacacionDto>> GetSolicitudByIdAsync(string id)
        {
            var solicitud = await _solicitudService.GetSolicitudByIdAsync(id);
            return Ok(solicitud);
        }

        // GET api/solicitudvacaciones/empleado/{empleadoId}
        [HttpGet("empleado/")]
        public async Task<ActionResult<IEnumerable<SolicitudVacacionDto>>> GetSolicitudesPorEmpleadoAsync()
        {
            var solicitudes = await _solicitudService.GetSolicitudesPorEmpleadoAsync();
            return Ok(solicitudes);
        }

        // POST api/solicitudvacaciones
        [HttpPost]
        public async Task<ActionResult<SolicitudVacacionDto>> CrearSolicitudAsync([FromBody] SolicitudVacacionCreateDto dto)
        {
            try
            {
                var nuevaSolicitud = await _solicitudService.CrearSolicitudAsync(dto);
                return Ok(nuevaSolicitud);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // PUT api/solicitudvacaciones/{id}/aprobar?nivel=1&aprobado=true&comentarios=OK
        [HttpPut("{id}/aprobar")]
        public async Task<ActionResult<SolicitudVacacionDto>> ProcesarAprobacionAsync(
            string id,
            [FromQuery] int nivel,
            [FromQuery] bool aprobado,
            [FromQuery] string comentarios)
        {
            var solicitudActualizada = await _solicitudService
                .ProcesarAprobacionAsync(id, nivel, aprobado, comentarios);
            return Ok(solicitudActualizada);
        }

        [HttpGet("aprobaciones/empleado")]
        public async Task<ActionResult<IEnumerable<AprobacionVacacionDto>>> GetAprobacionesPorEmpleado()
        {
            var aprobaciones = await _solicitudService.GetAprobacionesPorEmpleado();
            return Ok(aprobaciones);
        }        

        [HttpGet("aprobaciones/empleado/historico")]
        public async Task<ActionResult<IEnumerable<AprobacionVacacionDto>>> GetAprobacionesPorEmpleadoHistorico()
        {
            var aprobaciones = await _solicitudService.GetAprobacionesPorEmpleadoHistorico();
            return Ok(aprobaciones);
        }

    }
}
