using AutoGestion.interfaces.IPuesto;
using AutoGestion.interfaces.iTipoDeduccion;
using AutoGestion.Models;
using AutoGestion.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutoGestion.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TipoDeduccionController : ControllerBase
    {
        private readonly ITipoDeduccionService _tipoDeduccion;

        public TipoDeduccionController(ITipoDeduccionService tipoDeduccion)
        {
            _tipoDeduccion = tipoDeduccion;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoDeduccionDto>>> GetTipoDeducciones()
        {
            try
            {
                var tipoDeducciones = await _tipoDeduccion.GetTipoDeducciones();

                return Ok(tipoDeducciones);
            }
            catch (Exception)
            {
                throw;
            }

        }


        [HttpPost]
        public async Task<ActionResult<TipoDeduccionDto>> CreateTIpoDeducciones(TipoDeduccion tipoDeduccion)
        {
            try
            {
                var tipoDeduccionCreate = await _tipoDeduccion.PostTipoDeduccion(tipoDeduccion);
                return Ok(tipoDeduccionCreate);
            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TipoDeduccionDto>> UpdateTipoDeduccion(TipoDeduccion tipoDeduccion, string id)
        {
            try
            {
                var puestoCreate = await _tipoDeduccion.PutTipoDeduccion(id, tipoDeduccion);

                return Ok(puestoCreate);
            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TipoDeduccionDto>> TIpoDeduccionById(string id)
        {

            try
            {
                var tipoDeduccion = await _tipoDeduccion.GetTipoDeduccionById(id);
                return Ok(tipoDeduccion);
            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpGet("activos")]
        public async Task<ActionResult<IEnumerable<TipoDeduccionDto>>> GetTipoDeduccionesActivos()
        {
            try
            {
                var TipoDeduccionesActivas = await _tipoDeduccion.GetTipoDeduccionesActivos();
                return Ok(TipoDeduccionesActivas);
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
