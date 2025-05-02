using AutoGestion.Dto;
using AutoGestion.Models;

namespace AutoGestion.interfaces.IVoucherPago
{
    public interface IVoucherPagoRepository
    {
        Task<IEnumerable<VoucherPago>> GetVouchers();
        Task<VoucherPago> GetVoucherById(string id);
        Task<IEnumerable<VoucherPago>> GetVouchersByEmpleadoId(string empleadoId);
        Task<VoucherPago> PostVoucher(VoucherPago voucher, List<DetalleVoucherPago> detalles);
        Task<VoucherPago> PutVoucher(string id, VoucherPago voucher, List<DetalleVoucherPago> detalles);
        Task<bool> DeleteVoucher(string id);
        Task<VoucherTemplateDto> GetVoucherTemplate();
    }
}
