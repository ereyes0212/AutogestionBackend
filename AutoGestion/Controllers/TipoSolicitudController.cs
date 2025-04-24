using AutoGestion.interfaces.IPuesto;

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
    public class TipoSolicitudController : ControllerBase
    {
        private readonly ITipoSolicitudService _tipoSolicitudService;

        public TipoSolicitudController(ITipoSolicitudService tipoSolicitud)
        {
            _tipoSolicitudService = tipoSolicitud;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoSolicitudDto>>> GetTipoSolicitud()
        {
            try
            {
                var tupoSoliciuted = await _tipoSolicitudService.GetTipoSolicitud();

                return Ok(tupoSoliciuted);
            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TipoSolicitudDto>> TipoSolicitudById(string id)
        {

            try
            {
                var tipoSoliciutd = await _tipoSolicitudService.GetTipoSolicitudById(id);
                return Ok(tipoSoliciutd);
            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpGet("activos")]
        public async Task<ActionResult<IEnumerable<TipoSolicitudDto>>> GetTipoSolicitudActivos()
        {
            try
            {
                var tipoSoliciutdes = await _tipoSolicitudService.GetTipoSolicitudActivos();
                return Ok(tipoSoliciutdes);
            }
            catch (Exception)
            {
                throw;
            }

        }
        [HttpPost]
        public async Task<ActionResult<TipoSolicitudDto>> CreateTipoSolicitud(TipoSolicitud tipoSolicitud)
        {
            try
            {
                var tipoSolicitudCreate = await _tipoSolicitudService.PostTipoSolicitud(tipoSolicitud);
                return Ok(tipoSolicitudCreate);
            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TipoSolicitudDto>> UpdateTipoSoliciutd(TipoSolicitud tipoSolicitud, string id)
        {
            try
            {
                var tipoSolicitudCreate = await _tipoSolicitudService.PutTipoSolicitud(id, tipoSolicitud);

                return Ok(tipoSolicitudCreate);
            }
            catch (Exception)
            {
                throw;
            }

        }


    }
}
