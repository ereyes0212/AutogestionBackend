namespace AutoGestion.Models.ReporteDisenioDto
{
    public class ReporteDiseñoDto
    {
        public string? Id { get; set; }
        public string Empleado { get; set; }
        public string TipoSeccion { get; set; }
        public string TipoSeccionId { get; set; }

        public DateTime? FechaRegistro { get; set; }
        public int PaginaInicio { get; set; }
        public int PaginaFin { get; set; }
        public TimeOnly HoraInicio { get; set; }
        public TimeOnly HoraFin { get; set; }
        public string? Observacion { get; set; }
    }
}
