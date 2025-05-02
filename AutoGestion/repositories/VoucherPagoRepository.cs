// AutoGestion.repositories/VoucherPagoRepository.cs
using AutoGestion.Models;
using Microsoft.EntityFrameworkCore;
using AutoGestion.interfaces.IVoucherPago;
using AutoGestion.Dto;

namespace AutoGestion.repositories
{
    public class VoucherPagoRepository : IVoucherPagoRepository
    {
        private readonly DbContextAutoGestion _ctx;
        public VoucherPagoRepository(DbContextAutoGestion ctx)
            => _ctx = ctx;

        public async Task<IEnumerable<VoucherPago>> GetVouchers()
            => await _ctx.VoucherPagos
                         .Include(v => v.DetallesVoucherPago)
                            .ThenInclude(d => d.TipoDeduccion)
                         .Include(v => v.Empleado)
                            .ThenInclude(e => e.Puesto)
                         .ToListAsync();

        public async Task<VoucherPago?> GetVoucherById(string id)
            => await _ctx.VoucherPagos
                         .Include(v => v.DetallesVoucherPago)
                            .ThenInclude(d => d.TipoDeduccion)
                         .Include(v => v.Empleado)
                            .ThenInclude(e => e.Puesto)
                         .FirstOrDefaultAsync(v => v.Id == id);


        public async Task<IEnumerable<VoucherPago>> GetVouchersByEmpleadoId(string empleadoId)
            => await _ctx.VoucherPagos
                         .Where(v => v.EmpleadoId == empleadoId)
                         .Include(v => v.DetallesVoucherPago)
                            .ThenInclude(d => d.TipoDeduccion)
                         .Include(v => v.Empleado)
                            .ThenInclude(e => e.Puesto)
                         .ToListAsync();


        public async Task<VoucherPago> PostVoucher(VoucherPago voucher, List<DetalleVoucherPago> detalles)
        {
            var entry = await _ctx.VoucherPagos.AddAsync(voucher);
            await _ctx.SaveChangesAsync();

            foreach (var d in detalles)
            {
                d.VoucherPagoId = voucher.Id!;
                await _ctx.DetalleVoucherPagos.AddAsync(d);
            }
            await _ctx.SaveChangesAsync();

            // Recarga el voucher con sus relaciones
            var result = await _ctx.VoucherPagos
                .Where(v => v.Id == voucher.Id)
                .Include(v => v.DetallesVoucherPago)
                    .ThenInclude(d => d.TipoDeduccion)
                .Include(v => v.Empleado)
                    .ThenInclude(e => e.Puesto)
                .FirstOrDefaultAsync();

            return result!;
        }


        public async Task<VoucherPago> PutVoucher(string id, VoucherPago voucher, List<DetalleVoucherPago> detalles)
        {
            _ctx.Entry(voucher).State = EntityState.Modified;
            var antiguos = _ctx.DetalleVoucherPagos.Where(d => d.VoucherPagoId == id);
            _ctx.DetalleVoucherPagos.RemoveRange(antiguos);
            foreach (var d in detalles)
            {
                d.VoucherPagoId = id;
                await _ctx.DetalleVoucherPagos.AddAsync(d);
            }
            await _ctx.SaveChangesAsync();
            return voucher;
        }

        public async Task<bool> DeleteVoucher(string id)
        {
            var voucher = await _ctx.VoucherPagos.FindAsync(id);
            if (voucher == null) return false;
            var detalles = _ctx.DetalleVoucherPagos.Where(d => d.VoucherPagoId == id);
            _ctx.DetalleVoucherPagos.RemoveRange(detalles);
            _ctx.VoucherPagos.Remove(voucher);
            await _ctx.SaveChangesAsync();
            return true;
        }
        public async Task<VoucherTemplateDto> GetVoucherTemplate()
        {
            // 1) Empleados activos + datos de usuario
            var empleados = await _ctx.Empleados
                .Where(e => e.activo)
                .Include(e => e.Puesto)
                .Select(e => new EmpleadoVoucherTemplateDto
                {
                    EmpleadoId = e.id!,
                    NombreCompleto = e.nombre + " " + e.apellido,
                    DiasTrabajados = 0,
                    SalarioDiario = 0,
                    SalarioMensual = 0
                })
                .ToListAsync();

            // 2) Tipos de deducción activos
            var tipos = await _ctx.TipoDeducciones
                .Where(td => td.Activo)
                .Select(td => new TipoDeduccionDto
                {
                    Id = td.Id!,
                    Nombre = td.Nombre,
                    Descripcion = td.Descripcion,
                    Activo = td.Activo
                })
                .ToListAsync();

            return new VoucherTemplateDto
            {
                Empleados = empleados,
                TiposDeducciones = tipos
            };
        }
    }
}
