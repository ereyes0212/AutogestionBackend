using AutoGestion.interfaces.IConfiguracion;
using AutoGestion.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoGestion.Repositories
{
    public class ConfiguracionAprobacionRepository : IConfiguracionAprobacionRepository
    {
        private readonly DbContextAutoGestion _context;

        public ConfiguracionAprobacionRepository(DbContextAutoGestion context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ConfiguracionAprobacion>> GetAprobaciones()
        {
            return await _context.ConfiguracionAprobacion
                .Include(c => c.Puesto)
                .ToListAsync();
        }

        public async Task<ConfiguracionAprobacion> GetAprobacionesById(string id)
        {
            return await _context.ConfiguracionAprobacion
                .Include(c => c.Puesto)
                .FirstOrDefaultAsync(c => c.Id == id);
        }



        public async Task<IEnumerable<ConfiguracionAprobacion>> GetAprobacionesActivos()
        {
            return await _context.ConfiguracionAprobacion
                .Where(c => c.Activo)
                .Include(c => c.Puesto)
                .ToListAsync();
        }

 

        public async Task<IEnumerable<ConfiguracionAprobacion>> PostAprobaciones(IEnumerable<ConfiguracionAprobacion> configuraciones)
        {
            await _context.ConfiguracionAprobacion.AddRangeAsync(configuraciones);
            await _context.SaveChangesAsync();
            return configuraciones;
        }

        public async Task<IEnumerable<ConfiguracionAprobacion>> PutAprobaciones(IEnumerable<ConfiguracionAprobacion> configuraciones)
        {
            _context.ConfiguracionAprobacion.UpdateRange(configuraciones);
            await _context.SaveChangesAsync();
            return configuraciones;
        }

        public async Task DeleteAprobaciones()
        {
            var aprobaciones = await _context.ConfiguracionAprobacion.ToListAsync();

            if (aprobaciones.Any())
            {
                _context.ConfiguracionAprobacion.RemoveRange(aprobaciones);
                await _context.SaveChangesAsync();
            }
        }
    }
}
