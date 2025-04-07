using AutoGestion.Models;

namespace AutoGestion.interfaces.IEmpleado
{
    public interface IEmpleadoRepository
    {
        Task<IEnumerable<Empleado>> GetEmpleados();
        Task<Empleado> GetEmpleadoById(string id);
        Task<IEnumerable<Empleado>> GetEmpleadoByEmpresaId(string id);
        Task<IEnumerable<Empleado>> GetEmpleadosActivos();
        Task<IEnumerable<Empleado>> GetEmpleadosActivosByEmpresaId(string id);
        Task<Empleado> PostEmpleados(Empleado empleado);
        Task<Empleado> PutEmpleados(string id, Empleado empleado);
    }
}
