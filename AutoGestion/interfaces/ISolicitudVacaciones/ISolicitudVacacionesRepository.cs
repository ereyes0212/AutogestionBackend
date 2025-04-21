using AutoGestion.Models.AutoGestion.Models;

namespace AutoGestion.interfaces.ISolicitudVacaciones
{
    public interface ISolicitudVacacionesRepository
    {
        Task<IEnumerable<SolicitudVacacion>> GetSolicitudes();
        Task<SolicitudVacacion> GetSolicitudById(string id);
        Task<IEnumerable<SolicitudVacacion>> GetSolicitudesPorEmpleado(string empleadoId);
        Task<SolicitudVacacion> AddSolicitud(SolicitudVacacion solicitud);
        Task<SolicitudVacacion> UpdateSolicitud(SolicitudVacacion solicitud);
    }
}
