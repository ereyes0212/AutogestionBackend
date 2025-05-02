// AutoGestion.Dto/VoucherTemplateDto.cs
namespace AutoGestion.Dto
{
    public class EmpleadoVoucherTemplateDto
    {
        public string EmpleadoId { get; set; } = default!;
        public string NombreCompleto { get; set; } = default!;

        // Columnas que el usuario deberá completar en el Excel
        public int DiasTrabajados { get; set; }
        public decimal SalarioDiario { get; set; }
        public decimal SalarioMensual { get; set; }
        public decimal NetoPagar { get; set; }
    }

    public class VoucherTemplateDto
    {
        public List<EmpleadoVoucherTemplateDto> Empleados { get; set; } = new();
        public List<TipoDeduccionDto> TiposDeducciones { get; set; } = new();
    }
}
