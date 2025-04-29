using AutoGestion.Models;


    public interface ITipoDeduccionRepository
    {
        Task<IEnumerable<TipoDeduccion>> GetTipoDeducciones();
        Task<TipoDeduccion> GetTipoDeduccionById(string id);
        Task<IEnumerable<TipoDeduccion>> GetTipoDeduccionesActivos();
        Task<TipoDeduccion> PostTipoDeduccion(TipoDeduccion tipoDeduccion);
        Task<TipoDeduccion> PutTipoDeduccion(string id, TipoDeduccion tipoDeduccion);
    }
