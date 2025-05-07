
using AutoGestion.interfaces.iTipoSeccion;
using AutoGestion.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoGestion.repositories
{
    public class TipoSeccionRepository : ITipoSeccionRepository
    {
        private readonly DbContextAutoGestion _dbContextAutoGestion;

        public TipoSeccionRepository(DbContextAutoGestion dbContextAutoGestion)
        {
            _dbContextAutoGestion = dbContextAutoGestion;
        }

        public async Task<IEnumerable<TipoSeccion>> GetTipoSeccion()
        {
            return await _dbContextAutoGestion.TipoSeccion.ToListAsync();
        }
        public async Task<TipoSeccion?> GetTipoSeccionById(string id)
        {
            return await _dbContextAutoGestion.TipoSeccion.Where(e => e.Id == id).FirstOrDefaultAsync() ?? null;
        }


        public async Task<IEnumerable<TipoSeccion>> GetTipoSeccionActivos()
        {
            return await _dbContextAutoGestion.TipoSeccion.Where(e => e.Activo == true).ToListAsync();
        }


        public async Task<TipoSeccion> PostTipoSeccion(TipoSeccion tipoDeduccion)
        {
            var result = await _dbContextAutoGestion.TipoSeccion.AddAsync(tipoDeduccion);
            await _dbContextAutoGestion.SaveChangesAsync();
            return result.Entity;
        }
        public async Task<TipoSeccion> PutTipoSeccion( TipoSeccion tipoDeduccion)
        {

            _dbContextAutoGestion.Entry(tipoDeduccion).State = EntityState.Modified;

            await _dbContextAutoGestion.SaveChangesAsync();

            return tipoDeduccion;
        }
    }
}
