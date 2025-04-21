using AutoGestion.Models.SolicitudVacacionesDto;

namespace AutoGestion.interfaces.ISolicitudVacaciones
{
    public interface ISolicitudVacacionesService
    {
        Task<IEnumerable<SolicitudVacacionDto>> GetSolicitudes();
        Task<SolicitudVacacionDto> GetSolicitudById(string id);
        Task<IEnumerable<SolicitudVacacionDto>> GetSolicitudesPorEmpleado(string empleadoId);
        Task<SolicitudVacacionDto> CrearSolicitud(SolicitudVacacionCreateDto dto);
        Task<SolicitudVacacionDto> ProcesarAprobacion(string solicitudId, int nivel, bool aprobado, string comentarios);
    }
}
