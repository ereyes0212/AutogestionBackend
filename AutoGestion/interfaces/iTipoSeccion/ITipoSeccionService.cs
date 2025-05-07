using AutoGestion.Models;

namespace AutoGestion.interfaces.iTipoSeccion
{
    public interface ITipoSeccionService
    {
        Task<IEnumerable<TipoSeccionDTO>> GetTipoSecciones();
        Task<IEnumerable<TipoSeccionDTO>> GetTipoSeccionesActivos();
        Task<TipoSeccionDTO> GetTipoSeccionById(string id);
        Task<TipoSeccionDTO> PostTipoSeccion(TipoSeccion puesto);
        Task<TipoSeccionDTO> PutTipoSeccion(string id, TipoSeccion puesto);
    }
}
