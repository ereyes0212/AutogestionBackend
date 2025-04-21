// Models/SolicitudVacacionesDto/SolicitudVacacionCreateDto.cs
using System;

namespace AutoGestion.Models.SolicitudVacacionesDto
{
    public class SolicitudVacacionCreateDto
    {
        public string EmpleadoId { get; set; }
        public string EmpresaId { get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime FechaSolicitud { get; set; } = DateTime.UtcNow;
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int DiasPendientesFecha { get; set; }
        public int TotalDiasSolicitados { get; set; }
        public string Descripcion { get; set; }
        public string PuestoId { get; set; }
    }
    public class SolicitudVacacionDto
    {
        public string Id { get; set; }
        public string EmpleadoId { get; set; }
        public string EmpresaId { get; set; }
        public string NombreEmpleado { get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int DiasPendientes { get; set; }
        public int TotalDiasSolicitados { get; set; }
        public bool? Aprobado { get; set; }
        public string Descripcion { get; set; }
        public List<AprobacionVacacionDto> Aprobaciones { get; set; } = new();
    }
    public class AprobacionVacacionDto
    {
        public string Id { get; set; }
        public int Nivel { get; set; }
        public bool? Aprobado { get; set; }
        public string Comentario { get; set; }
        public DateTime? FechaAprobacion { get; set; }
    }
}

