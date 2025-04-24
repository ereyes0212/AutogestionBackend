using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoGestion.Models
{
    public class SolicitudVacacion
    {
        [Key, StringLength(36)]
        public string Id { get; set; }

        [Required, StringLength(36)]
        public string EmpleadoId { get; set; }
        [ForeignKey(nameof(EmpleadoId))]
        public Empleado Empleado { get; set; }

        [Required, StringLength(36)]
        public string PuestoId { get; set; }
        [ForeignKey(nameof(PuestoId))]
        public Puesto Puesto { get; set; }

        [Required]
        public DateTime FechaSolicitud { get; set; } = DateTime.UtcNow;

        [Required]
        public DateTime FechaInicio { get; set; }

        [Required]
        public DateTime FechaFin { get; set; }

        [NotMapped]
        public int DiasSolicitados
            => (int)(FechaFin.Date - FechaInicio.Date).TotalDays + 1;

        public bool? Aprobado { get; set; }  // null = pendiente, true/false = decisión tomada

        [StringLength(250)]
        public string Descripcion { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        public ICollection<SolicitudVacacionAprobacion> Aprobaciones { get; set; }
            = new List<SolicitudVacacionAprobacion>();
    }

    public class SolicitudVacacionAprobacion
    {
        [Key, StringLength(36)]
        public string Id { get; set; }

        [Required, StringLength(36)]
        public string SolicitudVacacionId { get; set; }
        [ForeignKey(nameof(SolicitudVacacionId))]
        public SolicitudVacacion SolicitudVacacion { get; set; }

        [Required, StringLength(36)]
        public string ConfiguracionAprobacionId { get; set; }
        [ForeignKey(nameof(ConfiguracionAprobacionId))]
        public ConfiguracionAprobacion Configuracion { get; set; }

        [StringLength(36)]
        public string? EmpleadoAprobadorId { get; set; }
        [ForeignKey(nameof(EmpleadoAprobadorId))]
        public Empleado? EmpleadoAprobador { get; set; }

        [Required]
        public int Nivel { get; set; } 

        [Required]
        public string Descripcion { get; set; }  

        [Required, StringLength(50)]
        public string Estado { get; set; }  // "Pendiente", "Aprobado", "Rechazado"

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? FechaDecision { get; set; }
        public string? Comentarios { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class VacacionesEmpleado
    {
        [Key, StringLength(36)]
        public string Id { get; set; }

        [Required, StringLength(36)]
        public string EmpleadoId { get; set; }
        [ForeignKey(nameof(EmpleadoId))]
        public Empleado Empleado { get; set; }

        [Required]
        public int Anio { get; set; }  // año al que corresponden estos días

        [Required]
        public decimal DiasDisponibles { get; set; }  // saldo actual de días

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}
