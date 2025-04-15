using AutoGestion.interfaces.ICampoTipoSolicitud;
using AutoGestion.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoGestion.repositories
{
    public class CampoTipoSolicitudRepository : ICampoTipoSolicitudRepository
    {
        private readonly DbContextAutoGestion _dbContextAutoGestion;

        public CampoTipoSolicitudRepository(DbContextAutoGestion dbContextAutoGestion)
        {
            _dbContextAutoGestion = dbContextAutoGestion;
        }
        public async Task<IEnumerable<CamposTipoSolicitud>> GetCamposTipoSolicitud() => await _dbContextAutoGestion.CampoTipoSolicitud.Include("TipoSolicitud").ToListAsync();
        public async Task<IEnumerable<CamposTipoSolicitud>> GetCamposTipoSolicitudByTipoSolicitud(string id) => await _dbContextAutoGestion.CampoTipoSolicitud.Include("TipoSolicitud").Where(c=> c.TipoSolicitudId == id).ToListAsync();
        public async Task<IEnumerable<CamposTipoSolicitud>> GetCampoTipoSolicitudActivas() => await _dbContextAutoGestion.CampoTipoSolicitud.Include("TipoSolicitud").Where(t => t.Activo == true).ToListAsync();
        public async Task<IEnumerable<CamposTipoSolicitud>> GetCampoTipoSolicitudActivasByTipoSolicitud(string id) => await _dbContextAutoGestion.CampoTipoSolicitud.Include("TipoSolicitud").Where(t => t.Activo == true && t.TipoSolicitudId == id).ToListAsync();
        public async Task<CamposTipoSolicitud> GetCampoTipoSolicitudById(string id) => await _dbContextAutoGestion.CampoTipoSolicitud.Include("TipoSolicitud").Where(t => t.Id == id).FirstOrDefaultAsync();
        public async Task<CamposTipoSolicitud> PostCampoTipoSolicitud(CamposTipoSolicitud campoTipoSolicitud)
        {
            var result = await _dbContextAutoGestion.CampoTipoSolicitud.AddAsync(campoTipoSolicitud);
            await _dbContextAutoGestion.SaveChangesAsync();
            return result.Entity;
        }
        public async Task<CamposTipoSolicitud> PutCampoTipoSolicitud(string id, CamposTipoSolicitud campoTipoSolicitud)
        {
            _dbContextAutoGestion.Entry(campoTipoSolicitud).State = EntityState.Modified;
            await _dbContextAutoGestion.SaveChangesAsync();
            return campoTipoSolicitud;
        }
    }
}
