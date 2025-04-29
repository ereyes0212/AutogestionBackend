
using AutoGestion.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoGestion.repositories
{
    public class TipoDeduccionRepository : ITipoDeduccionRepository
    {
        private readonly DbContextAutoGestion _dbContextAutoGestion;

        public TipoDeduccionRepository(DbContextAutoGestion dbContextAutoGestion)
        {
            _dbContextAutoGestion = dbContextAutoGestion;
        }

        public async Task<IEnumerable<TipoDeduccion>> GetTipoDeducciones()
        {
            return await _dbContextAutoGestion.TipoDeducciones.ToListAsync();
        }
        public async Task<TipoDeduccion?> GetTipoDeduccionById(string id)
        {
            return await _dbContextAutoGestion.TipoDeducciones.Where(e => e.Id == id).FirstOrDefaultAsync() ?? null;
        }


        public async Task<IEnumerable<TipoDeduccion>> GetTipoDeduccionesActivos()
        {
            return await _dbContextAutoGestion.TipoDeducciones.Where(e => e.Activo == true).ToListAsync();
        }


        public async Task<TipoDeduccion> PostTipoDeduccion(TipoDeduccion tipoDeduccion)
        {
            var result = await _dbContextAutoGestion.TipoDeducciones.AddAsync(tipoDeduccion);
            await _dbContextAutoGestion.SaveChangesAsync();
            return result.Entity;
        }
        public async Task<TipoDeduccion> PutTipoDeduccion(string id, TipoDeduccion tipoDeduccion)
        {

            _dbContextAutoGestion.Entry(tipoDeduccion).State = EntityState.Modified;

            await _dbContextAutoGestion.SaveChangesAsync();

            return tipoDeduccion;
        }
    }
}
