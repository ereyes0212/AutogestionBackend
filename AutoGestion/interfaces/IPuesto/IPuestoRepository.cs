using AutoGestion.Models;

namespace AutoGestion.interfaces.IPuesto
{
    public interface IPuestoRepository
    {
        Task<IEnumerable<Puesto>> GetPuestos();
        Task<Puesto> GetPuestosById(string id);
        Task<IEnumerable<Puesto>> GetPuestosByEmpresaId(string id);
        Task<IEnumerable<Puesto>> GetPuestosActivos();
        Task<IEnumerable<Puesto>> GetPuestosActivosByEmpresaId(string id);
        Task<Puesto> PostPuestos(Puesto Puesto);
        Task<Puesto> PutPuestos(string id, Puesto Puesto);
    }
}
