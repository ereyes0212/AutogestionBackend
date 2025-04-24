using AutoGestion.Models;

namespace AutoGestion.interfaces.IConfiguracion
{
    public interface IConfiguracionAprobacionRepository
    {
        Task<IEnumerable<ConfiguracionAprobacion>> GetAprobaciones();
        Task<ConfiguracionAprobacion> GetAprobacionesById(string id);
        Task<IEnumerable<ConfiguracionAprobacion>> GetAprobacionesActivos();

        Task DeleteAprobaciones();
        Task<IEnumerable<ConfiguracionAprobacion>> PostAprobaciones(IEnumerable<ConfiguracionAprobacion> configuraciones);
        Task<IEnumerable<ConfiguracionAprobacion>> PutAprobaciones(IEnumerable<ConfiguracionAprobacion> configuraciones);
    }
}
