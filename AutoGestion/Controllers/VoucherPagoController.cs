
using AutoGestion.Dto;
using AutoGestion.interfaces.IVoucherPago;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutoGestion.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class VoucherPagoController : ControllerBase
    {
        private readonly IVoucherPagoService _voucherService;

        public VoucherPagoController(IVoucherPagoService voucherService)
        {
            _voucherService = voucherService;
        }

        // GET: api/VoucherPago
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VoucherPagoDto>>> GetVouchers()
        {
            try
            {
                var vouchers = await _voucherService.GetVouchers();
                return Ok(vouchers);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // GET: api/VoucherPago/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<VoucherPagoDto>> GetVoucherById(string id)
        {
            try
            {
                var voucher = await _voucherService.GetVoucherById(id);
                return Ok(voucher);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // GET: api/VoucherPago/empleado/{empleadoId}
        [HttpGet("empleado")]
        public async Task<ActionResult<IEnumerable<VoucherPagoDto>>> GetVouchersByEmpleado()
        {
            try
            {
                var list = await _voucherService.GetVouchersByEmpleadoId();
                return Ok(list);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // POST: api/VoucherPago
        [HttpPost]
        public async Task<ActionResult<VoucherPagoDto>> CreateVoucher(VoucherPagoDto dto)
        {
            try
            {
                var created = await _voucherService.PostVoucher(dto);
                return Ok(created);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // PUT: api/VoucherPago/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<VoucherPagoDto>> UpdateVoucher(string id, VoucherPagoDto dto)
        {
            try
            {
                var updated = await _voucherService.PutVoucher(id, dto);
                return Ok(updated);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // DELETE: api/VoucherPago/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteVoucher(string id)
        {
            try
            {
                await _voucherService.DeleteVoucher(id);
                return NoContent();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("template")]
        public async Task<ActionResult<VoucherTemplateDto>> GetTemplate() => Ok(await _voucherService.GetVoucherTemplate());
    }
}
