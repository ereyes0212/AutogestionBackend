using AutoGestion.Models;

namespace AutoGestion.interfaces.IConfiguracion
{
    public interface IConfiguracionAprobacionRepository
    {
        Task<IEnumerable<ConfiguracionAprobacion>> GetAprobaciones();
        Task<ConfiguracionAprobacion> GetAprobacionesById(string id);
        Task<IEnumerable<ConfiguracionAprobacion>> GetAprobacionesByEmpresaId(string id);
        Task<IEnumerable<ConfiguracionAprobacion>> GetAprobacionesActivos();
        Task<IEnumerable<ConfiguracionAprobacion>> GetAprobacionesActivosByEmpresaId(string id);
        Task DeleteAprobacionesByEmpresaId(string empresaId);
        Task<IEnumerable<ConfiguracionAprobacion>> PostAprobaciones(IEnumerable<ConfiguracionAprobacion> configuraciones);
        Task<IEnumerable<ConfiguracionAprobacion>> PutAprobaciones(IEnumerable<ConfiguracionAprobacion> configuraciones);
    }
}
