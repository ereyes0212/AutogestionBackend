using AutoGestion.interfaces.IReporteDiseño;
using AutoGestion.interfaces.IVoucherPago;
using AutoGestion.Models;
using AutoGestion.Models.ReporteDisenioDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;

namespace AutoGestion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReporteDiseñoController : ControllerBase
    {

        private readonly IReporteDiseñoService _reporteDiseño;

        public ReporteDiseñoController(IReporteDiseñoService registroService)
        {
            _reporteDiseño = registroService;
        }

        // GET: api/VoucherPago
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReporteDiseñoDto>>> GetVouchers()
        {
            try
            {
                var reportesDiseño = await _reporteDiseño.getReporteDiseños();
                return Ok(reportesDiseño);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // GET: api/VoucherPago/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ReporteDiseñoDto>> getReporteDiseñoById(string id)
        {
            try
            {
                var reporteDiseño = await _reporteDiseño.getReporteDiseñoById(id);
                return Ok(reporteDiseño);
            }
            catch (Exception)
            {
                throw;
            }
        }



        // POST: api/VoucherPago
        [HttpPost]
        public async Task<ActionResult<ReporteDiseñoDto>> postReporteDiseño(ReporteDiseño dto)
        {
            try
            {
                var created = await _reporteDiseño.postReporteDiseño(dto);
                return Ok(created);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // PUT: api/VoucherPago/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<ReporteDiseñoDto>> putReporteDiseño(string id, ReporteDiseño dto)
        {
            try
            {
                var updated = await _reporteDiseño.putReporteDiseño(id, dto);
                return Ok(updated);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
