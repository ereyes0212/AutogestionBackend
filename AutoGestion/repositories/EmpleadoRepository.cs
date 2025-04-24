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

        public async Task<IEnumerable<Empleado>> GetEmpleados()
        {
            return await _dbContextAutoGestion.Empleados
                .Include(e => e.Usuario)
                .Include(e => e.Puesto)
                .Include(e => e.Jefe)
                .ToListAsync();
        }

        // Trae empleados sin usuario asignado
        public async Task<IEnumerable<Empleado>> GetEmpleadosDisponibles()
        {
            return await _dbContextAutoGestion.Empleados
                .Where(e => e.Usuario == null)
                .Include(e => e.Usuario)
                .Include(e => e.Puesto)
                .Include(e => e.Jefe)
                .ToListAsync();
        }
        public async Task<Empleado> GetEmpleadoByPuesto(string id)
        {
            return await _dbContextAutoGestion.Empleados
                .Where(e => e.puesto_id == id)
                .Include(e => e.Puesto)
                .Include(e => e.Jefe)
                .FirstOrDefaultAsync();
        }


        public async Task<Empleado?> GetEmpleadoById(string id)
        {
            return await _dbContextAutoGestion.Empleados
                .Where(e => e.id == id)
                .Include(e => e.Usuario)
                .Include(e => e.Puesto)
                .Include(e => e.Jefe)
                .FirstOrDefaultAsync();
        }


        public async Task<Empleado?> GetProfile(string idEmpleado)
        {
            return await _dbContextAutoGestion.Empleados
                .Where(e => e.id == idEmpleado)
                .Include(e => e.Usuario)
                .Include(e => e.Puesto)
                .Include(e => e.Jefe)
                .FirstOrDefaultAsync();
        }
        // Trae empleados activos
        public async Task<IEnumerable<Empleado>> GetEmpleadosActivos()
        {
            return await _dbContextAutoGestion.Empleados
                .Where(e => e.activo)
                .Include(e => e.Usuario)
                .Include(e => e.Puesto)
                .Include(e => e.Jefe)
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


    }
}
