using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoGestion.Models
{
    public class ReporteDiseño
    {
        [Key]
        [StringLength(36)]
        public string? Id { get; set; }
        [StringLength(36)]
        public string? EmpleadoId { get; set; }        
        [StringLength(36)]
        public string SeccionId { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public int PaginaInicio { get; set; }
        public int PaginaFin { get; set; }
        public TimeOnly HoraInicio { get; set; }
        public TimeOnly HoraFin { get; set; }
        public string? Observacion { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public string? adicionado_por { get; set; }
        public string? modificado_por { get; set; }

        [ForeignKey("EmpleadoId")]
        public Empleado? Empleado { get; set; }        
        [ForeignKey("SeccionId")]
        public TipoSeccion? Seccion { get; set; }
    }
}
