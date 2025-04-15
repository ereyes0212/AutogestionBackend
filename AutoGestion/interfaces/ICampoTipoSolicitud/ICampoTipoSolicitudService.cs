using AutoGestion.Models;
using AutoGestion.Models.CampoTipoSolicitudDto;

namespace AutoGestion.interfaces.ICampoTipoSolicitud
{
    public interface ICampoTipoSolicitudService
    {
        Task<IEnumerable<CampoTipoSolicitudDto>> GetCampoTipoSolicitud();
        Task<IEnumerable<CampoTipoSolicitudDto>> GetCampoTipoSolicitudActivos();
        Task<IEnumerable<CampoTipoSolicitudDto>> GetCampoTipoSolicitudByTipoSolicitud(string id);
        Task<IEnumerable<CampoTipoSolicitudDto>> GetCampoTipoSolicitudActivasByTipoSolicitud(string ig);
        Task<CampoTipoSolicitudDto> GetCampoTipoSolicitudById(string id);
        Task<CampoTipoSolicitudDto> PostCampoTipoSolicitud(CamposTipoSolicitud tipoSolicitud);
        Task<CampoTipoSolicitudDto> PutCampoTipoSolicitud(string id, CamposTipoSolicitud tipoSo);
    }
}
