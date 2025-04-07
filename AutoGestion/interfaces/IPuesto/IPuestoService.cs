using AutoGestion.models.Empresa;
using AutoGestion.Models;

namespace AutoGestion.interfaces.IPuesto
{
    public interface IPuestoService
    {
        Task<IEnumerable<PuestoDto>> GetPuestos();
        Task<IEnumerable<PuestoDto>> GetPuestosActivos();
        Task<IEnumerable<PuestoDto>> GetPuestosActivosByEmpresaId();
        Task<IEnumerable<PuestoDto>> GetPuestosByEmpresaId();
        Task<PuestoDto> GetPuestosById(string id);
        Task<PuestoDto> PostPuestos(Puesto puesto);
        Task<PuestoDto> PutPuestos(string id, Puesto puesto);
    }
}
