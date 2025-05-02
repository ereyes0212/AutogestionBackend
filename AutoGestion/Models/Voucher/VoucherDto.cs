public class DetalleVoucherPagoDto
{
    public string? Id { get; set; }
    public string TipoDeduccionId { get; set; } = default!;
    public string? TipoDeduccionNombre { get; set; }
    public decimal Monto { get; set; }
}

public class VoucherPagoDto
{
    public string? Id { get; set; }
    public string EmpleadoId { get; set; } = default!;
    public string? EmpleadoNombre { get; set; }
    public string? EmpleadoPuesto { get; set; }


    public DateTime FechaPago { get; set; }
    public int DiasTrabajados { get; set; }
    public decimal SalarioDiario { get; set; }
    public decimal SalarioMensual { get; set; }
    public decimal NetoPagar { get; set; }

    public string Observaciones { get; set; } = string.Empty;

    public List<DetalleVoucherPagoDto> Detalles { get; set; } = new();
}
