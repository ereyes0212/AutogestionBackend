
using AutoGestion.interfaces.ISolicitudVacaciones;
using AutoGestion.Models;
using AutoGestion.Models.AutoGestion.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoGestion.Repositories.SolicitudVacaciones
{
    public class SolicitudVacacionesRepository : ISolicitudVacacionesRepository
    {
        private readonly DbContextAutoGestion _ctx;
        public SolicitudVacacionesRepository(DbContextAutoGestion ctx)
            => _ctx = ctx;

        public async Task<IEnumerable<SolicitudVacacion>> GetSolicitudes()
            => await _ctx.SolicitudVacacion
                         .Include(s => s.Aprobaciones)
                         .ThenInclude(a => a.EmpleadoAprobador)
                         .ToListAsync();

        public async Task<SolicitudVacacion?> GetSolicitudById(string id)
            => await _ctx.SolicitudVacacion
                         .Include(s => s.Aprobaciones)
                         .ThenInclude(a => a.EmpleadoAprobador)
                         .FirstOrDefaultAsync(s => s.Id == id);

        public async Task<IEnumerable<SolicitudVacacion>> GetSolicitudesPorEmpleado(string empleadoId)
            => await _ctx.SolicitudVacacion
                         .Where(s => s.EmpleadoId == empleadoId)
                         .Include(s => s.Aprobaciones)
                         .ToListAsync();

        public async Task<SolicitudVacacion> AddSolicitud(SolicitudVacacion solicitud)
        {
            var entry = await _ctx.SolicitudVacacion.AddAsync(solicitud);
            await _ctx.SaveChangesAsync();
            return entry.Entity;
        }

        public async Task<SolicitudVacacion> UpdateSolicitud(SolicitudVacacion solicitud)
        {
            _ctx.Entry(solicitud).State = EntityState.Modified;
            await _ctx.SaveChangesAsync();
            return solicitud;
        }
    }
}
