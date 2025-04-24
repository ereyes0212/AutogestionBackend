// Models/SolicitudVacacionesDto/SolicitudVacacionCreateDto.cs
using System;

namespace AutoGestion.Models.SolicitudVacacionesDto
{
    public class SolicitudVacacionCreateDto
    {
        // Rango de fechas que pide
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }

        // Motivo / descripción
        public string Descripcion { get; set; }
    }
    public class SolicitudVacacionDto
    {
        public string Id { get; set; }
        public string EmpleadoId { get; set; }
        public string NombreEmpleado { get; set; }
        public string PuestoId { get; set; }
        public string Puesto { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        // Calculado: días solicitados = (FechaFin–FechaInicio)+1
        public int DiasSolicitados { get; set; }
        public bool? Aprobado { get; set; }    // null=pendiente, true/false=decisión
        public string Descripcion { get; set; }

        public List<AprobacionVacacionDto> Aprobaciones { get; set; }
            = new List<AprobacionVacacionDto>();
    }

    public class AprobacionVacacionDto
    {
        // Datos de la aprobación
        public string Id { get; set; }
        public string IdSolicitud { get; set; }

        public int Nivel { get; set; }
        public bool? Aprobado { get; set; } // true = aprobado, false = rechazado, null = pendiente
        public string Comentario { get; set; }
        public DateTime? FechaAprobacion { get; set; }

        // Datos del empleado que solicitó las vacaciones
        public string EmpleadoId { get; set; }
        public string NombreEmpleado { get; set; }
        public string PuestoId { get; set; }
        public string Puesto { get; set; }

        // Datos de la solicitud de vacaciones
        public DateTime? FechaSolicitud { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int DiasSolicitados { get; set; }
        public string Descripcion { get; set; }
    }

}
