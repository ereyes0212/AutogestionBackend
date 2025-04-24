
using AutoGestion.Models;

    public interface ITipoSolicitudService
    {
        Task<IEnumerable<TipoSolicitudDto>> GetTipoSolicitud();
        Task<IEnumerable<TipoSolicitudDto>> GetTipoSolicitudActivos();
        Task<TipoSolicitudDto> GetTipoSolicitudById(string id);
        Task<TipoSolicitudDto> PostTipoSolicitud(TipoSolicitud tipoSolicitud);
        Task<TipoSolicitudDto> PutTipoSolicitud(string id, TipoSolicitud tipoSolicitud);
    }
