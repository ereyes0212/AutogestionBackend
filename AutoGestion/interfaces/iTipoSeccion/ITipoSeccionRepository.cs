using AutoGestion.Models;

namespace AutoGestion.interfaces.iTipoSeccion
{

    public interface ITipoSeccionRepository
    {
        Task<IEnumerable<TipoSeccion>> GetTipoSeccion();
        Task<TipoSeccion> GetTipoSeccionById(string id);
        Task<IEnumerable<TipoSeccion>> GetTipoSeccionActivos();
        Task<TipoSeccion> PostTipoSeccion(TipoSeccion tipoDeduccion);
        Task<TipoSeccion> PutTipoSeccion(TipoSeccion tipoDeduccion);
    }
}
