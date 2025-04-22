public interface IEmpleadoRepository
{
    Task<IEnumerable<Empleado>> GetEmpleados();
    Task<Empleado> GetEmpleadoById(string id);
    Task<Empleado> GetEmpleadoByPuesto(string id);
    Task<IEnumerable<Empleado>> GetEmpleadoByEmpresaId(string id);
    Task<IEnumerable<Empleado>> GetEmpleadosActivos();
    Task<IEnumerable<Empleado>> GetEmpleadosDisponibles();
    Task<IEnumerable<Empleado>> GetEmpleadosActivosByEmpresaId(string id);
    Task<Empleado> PostEmpleados(Empleado empleado);
    Task<Empleado> PutEmpleados(string id, Empleado empleado);

    // Método para asignar empresas a un empleado
    Task AsignarEmpresaEmpleado(string empleadoId, List<string> empresaIds);
    Task<Empleado> GetProfile(string idEmpleado);

}