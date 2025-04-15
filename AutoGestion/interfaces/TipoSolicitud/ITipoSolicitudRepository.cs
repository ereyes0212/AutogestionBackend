

public interface ITipoSolicitudRepository
{
    Task<IEnumerable<TipoSolicitud>> GetTipoSolicitud();
    Task<TipoSolicitud> GetTipoSolicitudById(string id);
    Task<IEnumerable<TipoSolicitud>> GetTipoSolicitudActivas();
    Task<TipoSolicitud> PostTipoSolicitud(TipoSolicitud tipoSolicitud);
    Task<TipoSolicitud> PutTipoSolicitud(string id, TipoSolicitud tipoSolicitud);
}

