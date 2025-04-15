using AutoGestion.interfaces.ICampoTipoSolicitud;
using AutoGestion.Models;
using AutoGestion.Models.CampoTipoSolicitudDto;
using AutoGestion.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutoGestion.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CampoTipoSolicitudController : ControllerBase
    {
        private readonly ICampoTipoSolicitudService _CampoTipoSolicitudService;

        public CampoTipoSolicitudController(ICampoTipoSolicitudService campoTipoSolicitud)
        {
            _CampoTipoSolicitudService = campoTipoSolicitud;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CampoTipoSolicitudDto>>> GetCampoTipoSolicitud()
        {
            try
            {
                var campoTipoSoliciuted = await _CampoTipoSolicitudService.GetCampoTipoSolicitud();

                return Ok(campoTipoSoliciuted);
            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CampoTipoSolicitudDto>> GetCampoTipoSolicitudById(string id)
        {

            try
            {
                var campoTipoSolicitud = await _CampoTipoSolicitudService.GetCampoTipoSolicitudById(id);
                return Ok(campoTipoSolicitud);
            }
            catch (Exception)
            {
                throw;
            }

        }        
        [HttpGet("/tipo-solicitud{id}")]
        public async Task<ActionResult<CampoTipoSolicitudDto>> GetCampoTipoSolicitudByTipoSolicitud(string id)
        {

            try
            {
                var campoTipoSolicitud = await _CampoTipoSolicitudService.GetCampoTipoSolicitudByTipoSolicitud(id);
                return Ok(campoTipoSolicitud);
            }
            catch (Exception)
            {
                throw;
            }

        }
        [HttpGet("activos")]
        public async Task<ActionResult<IEnumerable<CampoTipoSolicitudDto>>> GetCampoTipoSolicitudActivos()
        {
            try
            {
                var campoTipoSoliciutdes = await _CampoTipoSolicitudService.GetCampoTipoSolicitudActivos();
                return Ok(campoTipoSoliciutdes);
            }
            catch (Exception)
            {
                throw;
            }

        }        
        [HttpGet("tipo-solicitud/activos/{id}")]
        public async Task<ActionResult<IEnumerable<CampoTipoSolicitudDto>>> GetCampoTipoSolicitudActivasByTipoSolicitud(string id)
        {
            try
            {
                var campoTipoSoliciutdes = await _CampoTipoSolicitudService.GetCampoTipoSolicitudActivasByTipoSolicitud(id);
                return Ok(campoTipoSoliciutdes);
            }
            catch (Exception)
            {
                throw;
            }

        }
        [HttpPost]
        public async Task<ActionResult<CampoTipoSolicitudDto>> CreateCampoTipoSolicitud(CamposTipoSolicitud campoTipoSolicitud)
        {
            try
            {
                var campoTipoSolicitudCreate = await _CampoTipoSolicitudService.PostCampoTipoSolicitud(campoTipoSolicitud);
                return Ok(campoTipoSolicitudCreate);
            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CampoTipoSolicitudDto>> UpdateTipoSoliciutd(CamposTipoSolicitud campoTipoSolicitud, string id)
        {
            try
            {
                var tipoSolicitudCreate = await _CampoTipoSolicitudService.PutCampoTipoSolicitud(id, campoTipoSolicitud);

                return Ok(tipoSolicitudCreate);
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
