
using AutoGestion.Models;

namespace AutoGestion.Interfaces.ISolicitudVacaciones
{
    public interface ISolicitudVacacionesRepository
    {
        Task<IEnumerable<SolicitudVacacion>> GetSolicitudesAsync();
        Task<SolicitudVacacion?> GetSolicitudByIdAsync(string id);
        Task<IEnumerable<SolicitudVacacion>> GetSolicitudesPorEmpleadoAsync(string empleadoId);
        Task<IEnumerable<SolicitudVacacionAprobacion>> GetAprobacionesPorEmpleado(string empleadoId);
        Task<IEnumerable<SolicitudVacacionAprobacion>> GetAprobacionesPorEmpleadoHistorico(string empleadoId);

        Task<SolicitudVacacion> AddSolicitudAsync(SolicitudVacacion solicitud);
        Task<SolicitudVacacion> UpdateSolicitudAsync(SolicitudVacacion solicitud);
    }
}
