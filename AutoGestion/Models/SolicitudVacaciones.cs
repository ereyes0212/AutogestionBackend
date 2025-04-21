namespace AutoGestion.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    namespace AutoGestion.Models
    {
        public class SolicitudVacacion
        {
            [Key]
            [StringLength(36)]
            public string Id { get; set; }

            // Referencia al empleado y su puesto
            [Required, StringLength(36)]
            public string EmpleadoId { get; set; }

            [Required, StringLength(36)]
            public string PuestoId { get; set; }

            [Required]
            public DateTime FechaIngreso { get; set; }   

            [Required]
            public DateTime FechaSolicitud { get; set; } 
            [Required]
            public string PeriodoVacaciones { get; set; }

            // Saldo de días
            [Required]
            public int DiasPendientesFecha { get; set; }    // Días que le quedan hasta la fecha de solicitud
            [Required]
            public int TotalDiasAutorizados { get; set; }   // Total de días que solicita aprobarse

            // Fechas de disfrute
            [Required]
            public DateTime FechaGoce { get; set; }     // Fecha de inicio del período de vacaciones
            [Required]
            public DateTime FechaRegreso { get; set; }  // Fecha de regreso al trabajo

            // Saldo tras autorización
            public int TotalDiasPendientes { get; set; }  // Días que quedarán tras el período aprobado

            // Estado y metadatos
            [Required]
            public bool? Aprobado { get; set; }  // true = aprobado completamente, false = rechazado, null = en proceso
            [StringLength(250)]
            public string Descripcion { get; set; } // Observaciones generales

            public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
            public DateTime? UpdatedAt { get; set; }

            // Relaciones
            [ForeignKey("EmpleadoId")]
            public Empleado Empleado { get; set; }

            [ForeignKey("PuestoId")]
            public Puesto Puesto { get; set; }

            public ICollection<SolicitudVacacionAprobacion> Aprobaciones { get; set; }
                = new List<SolicitudVacacionAprobacion>();
        }

        public class SolicitudVacacionAprobacion
        {
            [Key, StringLength(36)]
            public string Id { get; set; }

            [Required, StringLength(36)]
            public string SolicitudVacacionId { get; set; }

            [Required, StringLength(36)]
            public string ConfiguracionAprobacionId { get; set; }

            [StringLength(36)]
            public string? EmpleadoAprobadorId { get; set; }

            [Required]
            public int Nivel { get; set; }

            [Required, StringLength(50)]
            public string Estado { get; set; }  // "Pendiente", "Aprobado", "Rechazado"

            public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
            public DateTime? FechaDecision { get; set; }
            public string? Comentarios { get; set; }
            public DateTime? UpdatedAt { get; set; }

            [ForeignKey("SolicitudVacacionId")]
            public SolicitudVacacion SolicitudVacacion { get; set; }

            [ForeignKey("ConfiguracionAprobacionId")]
            public ConfiguracionAprobacion Configuracion { get; set; }

            [ForeignKey("EmpleadoAprobadorId")]
            public Empleado? EmpleadoAprobador { get; set; }
        }
    }

}
