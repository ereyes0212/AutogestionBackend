using AutoGestion.Interfaces.ISolicitudVacaciones;
using AutoGestion.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoGestion.Repositories.SolicitudVacaciones
{
    public class SolicitudVacacionesRepository : ISolicitudVacacionesRepository
    {
        private readonly DbContextAutoGestion _ctx;
        public SolicitudVacacionesRepository(DbContextAutoGestion ctx)
        {
            _ctx = ctx;
        }

        public async Task<IEnumerable<SolicitudVacacion>> GetSolicitudesAsync()
            => await _ctx.SolicitudVacacion
                            .Include(e => e.Empleado)
                    .ThenInclude(p => p.Puesto)
                .Include(s => s.Aprobaciones)
                    .ThenInclude(a => a.EmpleadoAprobador)
                .ToListAsync();

        public async Task<SolicitudVacacion?> GetSolicitudByIdAsync(string id)
            => await _ctx.SolicitudVacacion
                            .Include(e => e.Empleado)
                    .ThenInclude(p => p.Puesto)
                .Include(s => s.Aprobaciones)
                    .ThenInclude(a => a.EmpleadoAprobador)
                .FirstOrDefaultAsync(s => s.Id == id);

        public async Task<IEnumerable<SolicitudVacacion>> GetSolicitudesPorEmpleadoAsync(string empleadoId)
            => await _ctx.SolicitudVacacion
                .Where(s => s.EmpleadoId == empleadoId)
                .Include(e => e.Empleado)
                    .ThenInclude(p => p.Puesto)
                .Include(s => s.Aprobaciones)
                    .ThenInclude(a => a.EmpleadoAprobador)
                .ToListAsync();

        public async Task<SolicitudVacacion> AddSolicitudAsync(SolicitudVacacion solicitud)
        {
            var entry = await _ctx.SolicitudVacacion.AddAsync(solicitud);
            await _ctx.SaveChangesAsync();
            return entry.Entity;
        }

        public async Task<SolicitudVacacion> UpdateSolicitudAsync(SolicitudVacacion solicitud)
        {
            _ctx.Entry(solicitud).State = EntityState.Modified;
            await _ctx.SaveChangesAsync();
            return solicitud;
        }

        public async Task<IEnumerable<SolicitudVacacionAprobacion>> GetAprobacionesPorEmpleado(string empleadoId)
        {
            return await _ctx.SolicitudVacacionAprobacion
                .Include(a => a.SolicitudVacacion)
                    .ThenInclude(sv => sv.Empleado)
                .Include(a => a.SolicitudVacacion)
                    .ThenInclude(sv => sv.Puesto)
                .Where(a => a.EmpleadoAprobadorId == empleadoId
                         && a.Estado == "Pendiente")
                .ToListAsync();
        }
        public async Task<IEnumerable<SolicitudVacacionAprobacion>> GetAprobacionesPorEmpleadoHistorico(string empleadoId)
        {
            return await _ctx.SolicitudVacacionAprobacion
                .Include(a => a.SolicitudVacacion)
                    .ThenInclude(sv => sv.Empleado)
                .Include(a => a.SolicitudVacacion)
                    .ThenInclude(sv => sv.Puesto)
                .Where(a => a.EmpleadoAprobadorId == empleadoId
                         && a.Estado != "Pendiente")
                .ToListAsync();
        }




    }
}
