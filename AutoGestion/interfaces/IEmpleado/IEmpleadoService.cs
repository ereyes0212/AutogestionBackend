using AutoGestion.Models;

namespace AutoGestion.interfaces.IEmpleado
{
    public interface IEmpleadoService
    {
        Task<IEnumerable<EmpleadoDTO>> GetEmpleados();
        Task<IEnumerable<EmpleadoDTO>> GetEmpleadosActivos();
        Task<IEnumerable<EmpleadoDTO>> GetEmpleadosDisponibles();

        Task<EmpleadoDTO> GetEmpleadoById(string id);
        Task<EmpleadoDTO> PostEmpleados(EmpleadoCreateDto empleado);
        Task<EmpleadoDTO> PutEmpleados(string id, EmpleadoCreateDto empleadoDto);
        Task<EmpleadoDTO> GetProfile();

    }
}
