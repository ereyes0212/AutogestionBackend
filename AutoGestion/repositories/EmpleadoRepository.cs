

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
        public async Task<IEnumerable<Empleado>> GetEmpleados()
        {
            return await _dbContextAutoGestion.Empleados.Include("Usuario").Include("Empresa").Include("Puesto").ToListAsync();
        }        
        public async Task<Empleado?> GetEmpleadoById(string id)
        {
            return await _dbContextAutoGestion.Empleados.Where(e => e.id == id).Include("Usuario").Include("Empresa").Include("Puesto").Include("Empleado").FirstOrDefaultAsync() ?? null;
        }        
        public async Task<IEnumerable<Empleado?>> GetEmpleadoByEmpresaId(string id)
        {
            return await _dbContextAutoGestion.Empleados.Where(e => e.empresa_id == id).Include("Usuario").Include("Empresa").Include("Puesto").ToListAsync() ?? null;
        }

        public async Task<IEnumerable<Empleado>> GetEmpleadosActivos()
        {
            return await _dbContextAutoGestion.Empleados.Where(e => e.activo == true).Include("Usuario").Include("Empresa").Include("Puesto").ToListAsync();
        }        
        public async Task<IEnumerable<Empleado>> GetEmpleadosActivosByEmpresaId(string id)
        {
            return await _dbContextAutoGestion.Empleados.Where(e => e.activo == true && e.empresa_id == id).Include("Usuario").Include("Empresa").Include("Puesto").ToListAsync();
        }

        public async Task<Empleado> PostEmpleados( Empleado empleado)
        {
            var result = await _dbContextAutoGestion.Empleados.AddAsync(empleado);
            await _dbContextAutoGestion.SaveChangesAsync(); 
            return result.Entity; 
        }
        public async Task<Empleado> PutEmpleados(string id, Empleado empleado)
        {

            _dbContextAutoGestion.Entry(empleado).State = EntityState.Modified;

            await _dbContextAutoGestion.SaveChangesAsync();

            return empleado;
        }

    }
}
