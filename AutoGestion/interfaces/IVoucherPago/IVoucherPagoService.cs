using AutoGestion.Dto;

namespace AutoGestion.interfaces.IVoucherPago
{
    public interface IVoucherPagoService
    {
        Task<IEnumerable<VoucherPagoDto>> GetVouchers();
        Task<VoucherPagoDto> GetVoucherById(string id);
        Task<IEnumerable<VoucherPagoDto>> GetVouchersByEmpleadoId();
        Task<VoucherPagoDto> PostVoucher(VoucherPagoDto dto);
        Task<VoucherPagoDto> PutVoucher(string id, VoucherPagoDto dto);
        Task<bool> DeleteVoucher(string id);
        Task<VoucherTemplateDto> GetVoucherTemplate();
    }
}
