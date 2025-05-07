using AutoGestion.interfaces.IPuesto;
using AutoGestion.interfaces.iTipoDeduccion;
using AutoGestion.interfaces.iTipoSeccion;
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
    public class TipoSeccionController : ControllerBase
    {
        private readonly ITipoSeccionService _tipoSeccion;

        public TipoSeccionController(ITipoSeccionService tipoSeccion)
        {
            _tipoSeccion = tipoSeccion;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoSeccionDTO>>> GetTipoSecciones()
        {
            try
            {
                var tipoSecciones = await _tipoSeccion.GetTipoSecciones();

                return Ok(tipoSecciones);
            }
            catch (Exception)
            {
                throw;
            }

        }


        [HttpPost]
        public async Task<ActionResult<TipoSeccionDTO>> PostTipoSeccion(TipoSeccion tipoSecciones)
        {
            try
            {
                var tipoSeccionCreate = await _tipoSeccion.PostTipoSeccion(tipoSecciones);
                return Ok(tipoSeccionCreate);
            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TipoSeccionDTO>> PutTipoSeccion(TipoSeccion tipoSeccion, string id)
        {
            try
            {
                var TipoSeccionCreate = await _tipoSeccion.PutTipoSeccion(id, tipoSeccion);

                return Ok(TipoSeccionCreate);
            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TipoSeccionDTO>> GetTipoSeccionById(string id)
        {

            try
            {
                var tipoSeccion = await _tipoSeccion.GetTipoSeccionById(id);
                return Ok(tipoSeccion);
            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpGet("activos")]
        public async Task<ActionResult<IEnumerable<TipoSeccionDTO>>> GetTipoSeccionesActivos()
        {
            try
            {
                var TipoSecciones = await _tipoSeccion.GetTipoSeccionesActivos();
                return Ok(TipoSecciones);
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
