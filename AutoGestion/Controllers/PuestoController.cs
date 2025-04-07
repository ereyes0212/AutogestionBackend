using AutoGestion.interfaces.IEmpleado;
using AutoGestion.interfaces.IPuesto;
using AutoGestion.models.Empresa;
using AutoGestion.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutoGestion.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]

    public class PuestoController : ControllerBase
    {
        private readonly IPuestoService _puestoService;

        public PuestoController(IPuestoService puestoService)
        {
            _puestoService = puestoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PuestoDto>>> GetPuestos()
        {
            try
            {
                var empleados = await _puestoService.GetPuestos();

                return Ok(empleados);
            }
            catch (Exception)
            {
                throw;
            }

        }
        [HttpGet("empresaId")]
        public async Task<ActionResult<IEnumerable<PuestoDto>>> GetPuestosByEmpresaId()
        {
            try
            {
                var puestos = await _puestoService.GetPuestosByEmpresaId();

                return Ok(puestos);
            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpPost]
        public async Task<ActionResult<PuestoDto>> CreatePuestos(Puesto puesto)
        {
            try
            {
                var puestoCreate = await _puestoService.PostPuestos(puesto);
                return Ok(puestoCreate);
            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PuestoDto>> UpdatePuestos(Puesto puesto, string id)
        {
            try
            {
                var puestoCreate = await _puestoService.PutPuestos(id, puesto);

                return Ok(puestoCreate);
            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PuestoDto>> PuestosById(string id)
        {

            try
            {
                var empleadoCreate = await _puestoService.GetPuestosById(id);
                return Ok(empleadoCreate);
            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpGet("activos")]
        public async Task<ActionResult<IEnumerable<PuestoDto>>> GetPuestosActivos()
        {
            try
            {
                var puestos = await _puestoService.GetPuestosActivos();
                return Ok(puestos);
            }
            catch (Exception)
            {
                throw;
            }

        }
        [HttpGet("activos/empresa")]
        public async Task<ActionResult<IEnumerable<PuestoDto>>> GetPuestosActivosByEmpresaId()
        {
            try
            {
                var puestos = await _puestoService.GetPuestosActivosByEmpresaId();
                return Ok(puestos);
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
