using AutoGestion.Models;

namespace AutoGestion.interfaces.ICampoTipoSolicitud
{
    public interface ICampoTipoSolicitudRepository
    {
        Task<IEnumerable<CamposTipoSolicitud>> GetCamposTipoSolicitud();
        Task<IEnumerable<CamposTipoSolicitud>> GetCamposTipoSolicitudByTipoSolicitud(string id);
        Task<IEnumerable<CamposTipoSolicitud>> GetCampoTipoSolicitudActivasByTipoSolicitud(string ig);
        Task<CamposTipoSolicitud> GetCampoTipoSolicitudById(string id);
        Task<IEnumerable<CamposTipoSolicitud>> GetCampoTipoSolicitudActivas();
        Task<CamposTipoSolicitud> PostCampoTipoSolicitud(CamposTipoSolicitud tipoSolicitud);
        Task<CamposTipoSolicitud> PutCampoTipoSolicitud(string id, CamposTipoSolicitud tipoSolicitud);

    }
}
