
using AutoGestion.Models;
using AutoGestion.Models.ConfiguracionPuesto;

namespace AutoGestion.interfaces.IConfiguracion
{
    public interface IConfiguracionAprobacionService
    {
        Task<IEnumerable<ConfiguracionAprobacionDto>> GetAprobaciones();
        Task<ConfiguracionAprobacionDto> GetAprobacionById(string id);
        Task<IEnumerable<ConfiguracionAprobacionDto>> GetAprobacionesByEmpresaId();
        Task<IEnumerable<ConfiguracionAprobacionDto>> GetAprobacionesActivas();
        Task<IEnumerable<ConfiguracionAprobacionDto>> GetAprobacionesActivasByEmpresaId();
        Task<IEnumerable<ConfiguracionAprobacionDto>> PostAprobaciones(IEnumerable<ConfiguracionAprobacion> configuraciones);
        Task<IEnumerable<ConfiguracionAprobacionDto>> PutAprobaciones(IEnumerable<ConfiguracionAprobacionDto> configuraciones);
    }
}
