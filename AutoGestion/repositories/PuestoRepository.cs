

using AutoGestion.interfaces.IEmpleado;
using AutoGestion.interfaces.IPuesto;
using AutoGestion.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoGestion.Repositories
{
    public class PuestoRepository : IPuestoRepository
    {
        private readonly DbContextAutoGestion _dbContextAutoGestion;

        public PuestoRepository(DbContextAutoGestion dbContextAutoGestion)
        {
            _dbContextAutoGestion = dbContextAutoGestion;
        }
        public async Task<IEnumerable<Puesto>> GetPuestos()
        {
            return await _dbContextAutoGestion.Puesto.Include("Empresa").ToListAsync();
        }
        public async Task<Puesto?> GetPuestosById(string id)
        {
            return await _dbContextAutoGestion.Puesto.Where(e => e.Id == id).Include("Empresa").FirstOrDefaultAsync() ?? null;
        }
        public async Task<IEnumerable<Puesto?>> GetPuestosByEmpresaId(string id)
        {
            return await _dbContextAutoGestion.Puesto.Where(e => e.Empresa_id == id).Include("Empresa").ToListAsync() ?? null;
        }

        public async Task<IEnumerable<Puesto>> GetPuestosActivos()
        {
            return await _dbContextAutoGestion.Puesto.Where(e => e.Activo == true).Include("Empresa").ToListAsync();
        }
        public async Task<IEnumerable<Puesto>> GetPuestosActivosByEmpresaId(string id)
        {
            return await _dbContextAutoGestion.Puesto.Where(e => e.Activo == true && e.Empresa_id == id).Include("Empresa").ToListAsync();
        }

        public async Task<Puesto> PostPuestos(Puesto puesto)
        {
            var result = await _dbContextAutoGestion.Puesto.AddAsync(puesto);
            await _dbContextAutoGestion.SaveChangesAsync();
            return result.Entity;
        }
        public async Task<Puesto> PutPuestos(string id, Puesto puesto)
        {

            _dbContextAutoGestion.Entry(puesto).State = EntityState.Modified;

            await _dbContextAutoGestion.SaveChangesAsync();

            return puesto;
        }

    }
}
