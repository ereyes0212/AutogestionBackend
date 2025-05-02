
using AutoGestion.Dto;
using AutoGestion.interfaces;
using AutoGestion.interfaces.IEmailService;
using AutoGestion.interfaces.IVoucherPago;
using AutoGestion.Models;

namespace AutoGestion.Services
{
    public class VoucherPagoService : IVoucherPagoService
    {
        private readonly IVoucherPagoRepository _voucherRepo;
        private readonly IAsignaciones _asignacionesService;
        private readonly IEmailService _emailService;
        private readonly IEmpleadoRepository _empleadoRepo;

        public VoucherPagoService(
            IVoucherPagoRepository voucherRepo,
            IAsignaciones asignacionesService,
            IEmailService emailService,
            IEmpleadoRepository empleadoRepo)
        {
            _voucherRepo = voucherRepo;
            _asignacionesService = asignacionesService;
            _emailService = emailService;
            _empleadoRepo = empleadoRepo;
        }

        public async Task<IEnumerable<VoucherPagoDto>> GetVouchers()
        {
            var vouchers = await _voucherRepo.GetVouchers();
            if (vouchers == null || !vouchers.Any())
                throw new KeyNotFoundException("No se encontraron vouchers de pago.");

            return vouchers.Select(MapToDto);
        }

        public async Task<VoucherPagoDto> GetVoucherById(string id)
        {
            var v = await _voucherRepo.GetVoucherById(id);
            if (v == null)
                throw new KeyNotFoundException($"VoucherPago con id {id} no encontrado.");

            return MapToDto(v);
        }

        public async Task<IEnumerable<VoucherPagoDto>> GetVouchersByEmpleadoId()
        {
            var token = _asignacionesService.GetTokenFromHeader();
            var IdEmpleado = _asignacionesService.GetClaimValue(token!, "IdEmpleado");
            var list = await _voucherRepo.GetVouchersByEmpleadoId(IdEmpleado);
            if (list == null || !list.Any())
                throw new KeyNotFoundException($"No se encontraron vouchers para el empleado {IdEmpleado}.");

            return list.Select(MapToDto);
        }

        public async Task<VoucherPagoDto> PostVoucher(VoucherPagoDto dto)
        {
            var token = _asignacionesService.GetTokenFromHeader();
            var now = _asignacionesService.GetCurrentDateTime();
            var user = _asignacionesService.GetClaimValue(token!, "User") ?? "Sistema";

            // 1) Mapear DTO a modelo
            var model = new VoucherPago
            {
                Id = _asignacionesService.GenerateNewId(),
                EmpleadoId = dto.EmpleadoId,
                FechaPago = dto.FechaPago,
                DiasTrabajados = dto.DiasTrabajados,
                SalarioDiario = dto.SalarioDiario,
                SalarioMensual = dto.SalarioMensual,
                NetoPagar = dto.NetoPagar,
                Observaciones = dto.Observaciones,
                created_at = now,
                updated_at = now,
                adicionado_por = user,
                modificado_por = user
            };

            // 2) Mapear detalles
            var detalles = dto.Detalles.Select(d => new DetalleVoucherPago
            {
                Id = _asignacionesService.GenerateNewId(),
                VoucherPagoId = model.Id!,
                TipoDeduccionId = d.TipoDeduccionId,
                Monto = d.Monto,
                created_at = now,
                updated_at = now,
                adicionado_por = user,
                modificado_por = user
            }).ToList();

            // 3) Guardar en BD
            var created = await _voucherRepo.PostVoucher(model, detalles);

            // 4) Envío de correo al empleado
            var empleado = await _empleadoRepo.GetEmpleadoById(dto.EmpleadoId);
            if (!string.IsNullOrWhiteSpace(empleado?.correo))
            {
                var email = empleado.correo!;
                var nombre = empleado.nombre + " " + empleado.apellido;
                var asunto = "Tu comprobante de pago está disponible";

                var salarioQuincenal = dto.SalarioMensual / 2;

                var cuerpo = $@"
<div style=""font-family: Arial, sans-serif; font-size:14px; color:#333;"">
  <h2>Hola {nombre},</h2>
  <p>Tu comprobante de pago del día <strong>{dto.FechaPago:dd/MM/yyyy}</strong> ha sido generado:</p>

  <p><strong>Resumen del pago:</strong></p>
  <ul>
    <li><strong>Días trabajados:</strong> {dto.DiasTrabajados}</li>
    <li><strong>Salario diario:</strong> Lps {dto.SalarioDiario:N2}</li>
    <li><strong>Salario mensual:</strong> Lps {dto.SalarioMensual:N2}</li>
    <li><strong>Salario quincenal:</strong> Lps {salarioQuincenal:N2}</li>
  </ul>

  <table style=""width:100%; border-collapse: collapse;"">
    <tr>
      <th style=""border:1px solid #ddd; padding:8px;"">Concepto</th>
      <th style=""border:1px solid #ddd; padding:8px; text-align:right;"">Monto</th>
    </tr>";

                foreach (var det in created.DetallesVoucherPago)
                {
                    var concepto = det.TipoDeduccion?.Nombre ?? det.TipoDeduccionId;
                    cuerpo += $@"
    <tr>
      <td style=""border:1px solid #ddd; padding:8px;"">{concepto}</td>
      <td style=""border:1px solid #ddd; padding:8px; text-align:right;"">Lps {det.Monto:N2}</td>
    </tr>";
                }

                cuerpo += $@"
    <tr>
      <td style=""border:1px solid #ddd; padding:8px; font-weight:bold;"">Neto a Pagar</td>
      <td style=""border:1px solid #ddd; padding:8px; text-align:right; font-weight:bold;"">Lps {created.NetoPagar:N2}</td>
    </tr>
  </table>

  <p style=""margin-top:20px;"">Puedes ver tu comprobante completo en el sistema.</p>
  <p style=""font-size:12px; color:#666;"">Este es un mensaje automático, por favor no respondas.</p>
</div>";

                await _emailService.SendEmailAsync(email, asunto, cuerpo);
            }

            return MapToDto(created);
        }


        public async Task<VoucherPagoDto> PutVoucher(string id, VoucherPagoDto dto)
        {
            var existing = await _voucherRepo.GetVoucherById(id);
            if (existing == null)
                throw new KeyNotFoundException($"VoucherPago con id {id} no encontrado.");

            var token = _asignacionesService.GetTokenFromHeader();
            var now = _asignacionesService.GetCurrentDateTime();
            var user = _asignacionesService.GetClaimValue(token!, "User") ?? "Sistema";

            // Actualizar campos principales
            existing.FechaPago = dto.FechaPago;
            existing.DiasTrabajados = dto.DiasTrabajados;
            existing.SalarioDiario = dto.SalarioDiario;
            existing.SalarioMensual = dto.SalarioMensual;
            existing.Observaciones = dto.Observaciones;
            existing.NetoPagar = dto.NetoPagar;
            existing.updated_at = now;
            existing.modificado_por = user;

            // Mapear nuevos detalles
            var detalles = dto.Detalles.Select(d => new DetalleVoucherPago
            {
                Id = d.Id ?? _asignacionesService.GenerateNewId(),
                VoucherPagoId = existing.Id!,
                TipoDeduccionId = d.TipoDeduccionId,
                Monto = d.Monto,
                created_at = existing.created_at,
                updated_at = now,
                adicionado_por = existing.adicionado_por,
                modificado_por = user
            }).ToList();

            var updated = await _voucherRepo.PutVoucher(id, existing, detalles);
            return MapToDto(updated);
        }

        public async Task<bool> DeleteVoucher(string id)
        {
            var ok = await _voucherRepo.DeleteVoucher(id);
            if (!ok)
                throw new KeyNotFoundException($"VoucherPago con id {id} no encontrado.");
            return true;
        }

        #region Mapeos
        private static VoucherPagoDto MapToDto(VoucherPago v) => new()
        {
            Id = v.Id,
            EmpleadoId = v.EmpleadoId,
            EmpleadoNombre = v.Empleado?.nombre + " " + v.Empleado?.apellido,
            EmpleadoPuesto = v.Empleado?.Puesto?.Nombre,
            FechaPago = v.FechaPago,
            NetoPagar = v.NetoPagar,
            DiasTrabajados = v.DiasTrabajados,
            SalarioDiario = v.SalarioDiario,
            SalarioMensual = v.SalarioMensual,

            Observaciones = v.Observaciones!,
            Detalles = v.DetallesVoucherPago
                                   .Select(d => new DetalleVoucherPagoDto
                                   {
                                       Id = d.Id,
                                       TipoDeduccionId = d.TipoDeduccionId,
                                       TipoDeduccionNombre = d.TipoDeduccion?.Nombre,
                                       Monto = d.Monto
                                   })
                                   .ToList()
        };


        public async Task<VoucherTemplateDto> GetVoucherTemplate()
        {
            return await _voucherRepo.GetVoucherTemplate();
        }
        #endregion
    }
}
