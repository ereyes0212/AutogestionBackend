using AutoGestion.interfaces.IReporteDiseño;
using AutoGestion.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoGestion.repositories
{
    public class ReporteDiseñoRepository : IReporteDiseñoReporsitory
    {
        private readonly DbContextAutoGestion _dbContextAutoGestion;

        public ReporteDiseñoRepository(DbContextAutoGestion dbContextAutoGestion)
        {
            _dbContextAutoGestion = dbContextAutoGestion;
        }

        public async Task<IEnumerable<ReporteDiseño>> getReportesDiseño()
        {
            return await _dbContextAutoGestion.ReporteDiseño
                .Include(v => v.Empleado)
                .Include(v => v.Seccion)
                .ToListAsync();
        }
        public async Task<ReporteDiseño?> getReporteDiseñoById(string id)
        {
            return await _dbContextAutoGestion.ReporteDiseño
                .Where(e => e.Id == id)
                .Include(v => v.Empleado)
                .Include(v => v.Seccion)
                .FirstOrDefaultAsync() ?? null;
        }

        public async Task<ReporteDiseño> postReporteDiseño(ReporteDiseño reporteDiseño)
        {
            var result = await _dbContextAutoGestion.ReporteDiseño.AddAsync(reporteDiseño);
            await _dbContextAutoGestion.SaveChangesAsync();

            // Recargar la entidad incluyendo las propiedades de navegación
            await _dbContextAutoGestion.Entry(result.Entity).Reference(r => r.Empleado).LoadAsync();
            await _dbContextAutoGestion.Entry(result.Entity).Reference(r => r.Seccion).LoadAsync();

            return result.Entity;
        }
        public async Task<ReporteDiseño> putReporteDiseño(ReporteDiseño reporteDiseño)
        {
            _dbContextAutoGestion.Entry(reporteDiseño).State = EntityState.Modified;

            await _dbContextAutoGestion.SaveChangesAsync();

            // Cargar explícitamente las propiedades de navegación
            await _dbContextAutoGestion.Entry(reporteDiseño).Reference(r => r.Empleado).LoadAsync();
            await _dbContextAutoGestion.Entry(reporteDiseño).Reference(r => r.Seccion).LoadAsync();

            return reporteDiseño;
        }
    }
}
