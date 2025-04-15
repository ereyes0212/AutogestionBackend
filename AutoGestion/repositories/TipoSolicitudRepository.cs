using AutoGestion.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoGestion.repositories
{
    public class TipoSolicitudRepository : ITipoSolicitudRepository
    {
        private readonly DbContextAutoGestion _dbContextAutoGestion;

        public TipoSolicitudRepository(DbContextAutoGestion dbContextAutoGestion)
        {
            _dbContextAutoGestion = dbContextAutoGestion;
        }

        public async Task<IEnumerable<TipoSolicitud>> GetTipoSolicitud() => await _dbContextAutoGestion.TipoSolicitud.ToListAsync();
        public async Task<IEnumerable<TipoSolicitud>> GetTipoSolicitudActivas() => await _dbContextAutoGestion.TipoSolicitud.Where(t => t.activo == true).ToListAsync();
        public async Task<TipoSolicitud> GetTipoSolicitudById(string id) => await _dbContextAutoGestion.TipoSolicitud.Where(t =>t.Id == id).FirstOrDefaultAsync();
        public async Task<TipoSolicitud> PostTipoSolicitud(TipoSolicitud tipoSolicitud)
        {
            var result = await _dbContextAutoGestion.TipoSolicitud.AddAsync(tipoSolicitud);
            await _dbContextAutoGestion.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<TipoSolicitud> PutTipoSolicitud(string id, TipoSolicitud tipoSolicitud)
        {
            _dbContextAutoGestion.Entry(tipoSolicitud).State = EntityState.Modified;
            await _dbContextAutoGestion.SaveChangesAsync();
            return tipoSolicitud;
        }

    }
}

