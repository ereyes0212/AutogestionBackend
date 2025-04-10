using AutoGestion.interfaces.IEmpleado;
using AutoGestion.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoGestion.Repositories
{
    public class EmpleadoRepository : IEmpleadoRepository
    {
        private readonly DbContextAutoGestion _dbContextAutoGestion;

        public EmpleadoRepository(DbContextAutoGestion dbContextAutoGestion)
        {
            _dbContextAutoGestion = dbContextAutoGestion;
        }

        // Trae todos los empleados, incluyendo Usuario, Puesto y la colección de EmpleadoEmpresas -> Empresa.
        public async Task<IEnumerable<Empleado>> GetEmpleados()
        {
            return await _dbContextAutoGestion.Empleados
                .Include(e => e.Usuario)
                .Include(e => e.Puesto)
                .Include(e => e.EmpleadoEmpresas)
                    .ThenInclude(ee => ee.Empresa)
                .ToListAsync();
        }

        // Trae empleados sin usuario asignado
        public async Task<IEnumerable<Empleado>> GetEmpleadosDisponibles()
        {
            return await _dbContextAutoGestion.Empleados
                .Where(e => e.Usuario == null)
                .Include(e => e.Puesto)
                .Include(e => e.EmpleadoEmpresas)
                    .ThenInclude(ee => ee.Empresa)
                .ToListAsync();
        }

        // Trae un empleado por Id, incluyendo Usuario, Puesto y las empresas asociadas
        public async Task<Empleado?> GetEmpleadoById(string id)
        {
            return await _dbContextAutoGestion.Empleados
                .Where(e => e.id == id)
                .Include(e => e.Usuario)
                .Include(e => e.Puesto)
                .Include(e => e.EmpleadoEmpresas)
                    .ThenInclude(ee => ee.Empresa)
                .FirstOrDefaultAsync();
        }

        // Trae empleados asociados a una empresa específica
        public async Task<IEnumerable<Empleado>> GetEmpleadoByEmpresaId(string id)
        {
            return await _dbContextAutoGestion.Empleados
                .Where(e => e.EmpleadoEmpresas.Any(ee => ee.Empresa.Id == id))
                .Include(e => e.Usuario)
                .Include(e => e.Puesto)
                .Include(e => e.EmpleadoEmpresas)
                    .ThenInclude(ee => ee.Empresa)
                .ToListAsync();
        }

        // Trae empleados activos
        public async Task<IEnumerable<Empleado>> GetEmpleadosActivos()
        {
            return await _dbContextAutoGestion.Empleados
                .Where(e => e.activo)
                .Include(e => e.Usuario)
                .Include(e => e.Puesto)
                .Include(e => e.EmpleadoEmpresas)
                    .ThenInclude(ee => ee.Empresa)
                .ToListAsync();
        }

        // Trae empleados activos asociados a una empresa específica
        public async Task<IEnumerable<Empleado>> GetEmpleadosActivosByEmpresaId(string id)
        {
            return await _dbContextAutoGestion.Empleados
                .Where(e => e.activo && e.EmpleadoEmpresas.Any(ee => ee.Empresa.Id == id))
                .Include(e => e.Usuario)
                .Include(e => e.Puesto)
                .Include(e => e.EmpleadoEmpresas)
                    .ThenInclude(ee => ee.Empresa)
                .ToListAsync();
        }

        // Crea un nuevo empleado
        public async Task<Empleado> PostEmpleados(Empleado empleado)
        {
            var result = await _dbContextAutoGestion.Empleados.AddAsync(empleado);
            await _dbContextAutoGestion.SaveChangesAsync();
            return result.Entity;
        }

        // Actualiza un empleado existente
        public async Task<Empleado> PutEmpleados(string id, Empleado empleado)
        {
            _dbContextAutoGestion.Entry(empleado).State = EntityState.Modified;
            await _dbContextAutoGestion.SaveChangesAsync();
            return empleado;
        }

        // Asigna empresas a un empleado
        public async Task AsignarEmpresaEmpleado(string empleadoId, List<string> empresaIds)
        {
            // Obtener el empleado con sus relaciones
            var empleado = await _dbContextAutoGestion.Empleados
                .Include(e => e.EmpleadoEmpresas)
                .FirstOrDefaultAsync(e => e.id == empleadoId);

            if (empleado == null)
            {
                throw new ArgumentException("Empleado no encontrado.");
            }

            // Eliminar relaciones existentes
            _dbContextAutoGestion.EmpleadoEmpresa.RemoveRange(empleado.EmpleadoEmpresas);

            // Crear nuevas relaciones
            var nuevasRelaciones = empresaIds.Select(empresaId => new EmpleadoEmpresa
            {
                EmpleadoId = empleadoId,
                EmpresaId = empresaId
            });

            await _dbContextAutoGestion.EmpleadoEmpresa.AddRangeAsync(nuevasRelaciones);
            await _dbContextAutoGestion.SaveChangesAsync();
        }

    }
}
