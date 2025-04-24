public interface IEmpleadoRepository
{
    Task<IEnumerable<Empleado>> GetEmpleados();
    Task<Empleado> GetEmpleadoById(string id);
    Task<Empleado> GetEmpleadoByPuesto(string id);
    Task<IEnumerable<Empleado>> GetEmpleadosActivos();
    Task<IEnumerable<Empleado>> GetEmpleadosDisponibles();
    Task<Empleado> PostEmpleados(Empleado empleado);
    Task<Empleado> PutEmpleados(string id, Empleado empleado);
    Task<Empleado> GetProfile(string idEmpleado);

}