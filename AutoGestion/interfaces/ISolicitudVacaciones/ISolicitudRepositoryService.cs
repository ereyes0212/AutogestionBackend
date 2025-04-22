using System.Collections.Generic;
using System.Threading.Tasks;
using AutoGestion.Models.SolicitudVacacionesDto;

namespace AutoGestion.Interfaces.ISolicitudVacaciones
{
    public interface ISolicitudVacacionesService
    {
        Task<IEnumerable<SolicitudVacacionDto>> GetSolicitudesAsync();
        Task<SolicitudVacacionDto> GetSolicitudByIdAsync(string id);
        Task<IEnumerable<SolicitudVacacionDto>> GetSolicitudesPorEmpleadoAsync();
        Task<SolicitudVacacionDto> CrearSolicitudAsync(SolicitudVacacionCreateDto dto);
        Task<SolicitudVacacionDto> ProcesarAprobacionAsync(string solicitudId, int nivel, bool aprobado, string comentarios);
        Task<IEnumerable<AprobacionVacacionDto>> GetAprobacionesPorEmpleado();

    }
}
