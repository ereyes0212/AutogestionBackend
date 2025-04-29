using AutoGestion.Models;

namespace AutoGestion.interfaces.iTipoDeduccion
{
    public interface ITipoDeduccionService
    {
        Task<IEnumerable<TipoDeduccionDto>> GetTipoDeducciones();
        Task<IEnumerable<TipoDeduccionDto>> GetTipoDeduccionesActivos();
        Task<TipoDeduccionDto> GetTipoDeduccionById(string id);
        Task<TipoDeduccionDto> PostTipoDeduccion(TipoDeduccion puesto);
        Task<TipoDeduccionDto> PutTipoDeduccion(string id, TipoDeduccion puesto);
    }
}
