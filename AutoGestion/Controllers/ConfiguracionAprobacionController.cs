using AutoGestion.interfaces.IConfiguracion;
using AutoGestion.Models;
using AutoGestion.Models.ConfiguracionPuesto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutoGestion.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ConfiguracionAprobacionController : ControllerBase
    {
        private readonly IConfiguracionAprobacionService _configuracionService;

        public ConfiguracionAprobacionController(IConfiguracionAprobacionService configuracionService)
        {
            _configuracionService = configuracionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConfiguracionAprobacionDto>>> GetAprobaciones()
        {
            try
            {
                var aprobaciones = await _configuracionService.GetAprobaciones();
                return Ok(aprobaciones);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ConfiguracionAprobacionDto>> GetAprobacionById(string id)
        {
            try
            {
                var aprobacion = await _configuracionService.GetAprobacionById(id);
                return Ok(aprobacion);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("empresa/")]
        public async Task<ActionResult<IEnumerable<ConfiguracionAprobacionDto>>> GetAprobacionesByEmpresaId()
        {
            try
            {
                var aprobaciones = await _configuracionService.GetAprobacionesByEmpresaId();
                return Ok(aprobaciones);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("activos")]
        public async Task<ActionResult<IEnumerable<ConfiguracionAprobacionDto>>> GetAprobacionesActivas()
        {
            try
            {
                var aprobaciones = await _configuracionService.GetAprobacionesActivas();
                return Ok(aprobaciones);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("activos/empresa/")]
        public async Task<ActionResult<IEnumerable<ConfiguracionAprobacionDto>>> GetAprobacionesActivasByEmpresaId()
        {
            try
            {
                var aprobaciones = await _configuracionService.GetAprobacionesActivasByEmpresaId();
                return Ok(aprobaciones);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<ConfiguracionAprobacionDto>>> PostAprobaciones([FromBody] IEnumerable<ConfiguracionAprobacion> configuraciones)
        {
            try
            {
                var resultado = await _configuracionService.PostAprobaciones(configuraciones);
                return Ok(resultado);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut]
        public async Task<ActionResult<IEnumerable<ConfiguracionAprobacionDto>>> PutAprobaciones([FromBody] IEnumerable<ConfiguracionAprobacionDto> configuraciones)
        {
            try
            {
                var resultado = await _configuracionService.PutAprobaciones(configuraciones);
                return Ok(resultado);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
