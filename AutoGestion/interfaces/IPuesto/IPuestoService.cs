
using AutoGestion.Models;

namespace AutoGestion.interfaces.IPuesto
{
    public interface IPuestoService
    {
        Task<IEnumerable<PuestoDto>> GetPuestos();
        Task<IEnumerable<PuestoDto>> GetPuestosActivos();
        Task<PuestoDto> GetPuestosById(string id);
        Task<PuestoDto> PostPuestos(Puesto puesto);
        Task<PuestoDto> PutPuestos(string id, Puesto puesto);
    }
}
