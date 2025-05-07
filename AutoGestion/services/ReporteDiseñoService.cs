using AutoGestion.interfaces;
using AutoGestion.interfaces.IReporteDiseño;
using AutoGestion.interfaces.iTipoSeccion;
using AutoGestion.Models;
using AutoGestion.Models.ReporteDisenioDto;
using AutoGestion.repositories;
using AutoGestion.Utils;

namespace AutoGestion.services
{
    public class ReporteDiseñoService : IReporteDiseñoService
    {
        private readonly IReporteDiseñoReporsitory _reporteDiseñoRepository;
        private readonly IAsignaciones _AsinacionesService;

        public ReporteDiseñoService(IReporteDiseñoReporsitory reporteDiseñoRepository, IAsignaciones asignacionesService)
        {
            _reporteDiseñoRepository = reporteDiseñoRepository;
            _AsinacionesService = asignacionesService;
        }
        public async Task<IEnumerable<ReporteDiseñoDto>> getReporteDiseños()
        {

            var puestos = await _reporteDiseñoRepository.getReportesDiseño();
            if (puestos == null)
            {
                throw new KeyNotFoundException("Lista de tipo secciones Vacia.");
            }
            var SeccionDto = puestos.Select(rd => new ReporteDiseñoDto
            {
                Id = rd.Id!,
                HoraFin = rd.HoraFin,
                HoraInicio = rd.HoraInicio,
                TipoSeccion = rd.Seccion.Nombre,
                PaginaFin = rd.PaginaFin,
                TipoSeccionId = rd.SeccionId,
                Observacion = rd.Observacion,
                PaginaInicio = rd.PaginaInicio,
                Empleado = rd.Empleado.nombre,
                FechaRegistro = rd.FechaRegistro ,

            });

            return SeccionDto;
        }
        public async Task<ReporteDiseñoDto?> getReporteDiseñoById(string id)
        {
            var p = await _reporteDiseñoRepository.getReporteDiseñoById(id);

            if (p == null)
            {
                throw new KeyNotFoundException("Reporte Diseño no encontrado.");
            }

            var reporteDiseño = new ReporteDiseñoDto
            {
                Id = p.Id!,
                HoraFin = p.HoraFin,
                HoraInicio = p.HoraInicio,
                TipoSeccion = p.Seccion.Nombre,
                PaginaFin = p.PaginaFin,
                TipoSeccionId = p.Seccion.Id,
                Observacion = p.Observacion,
                PaginaInicio = p.PaginaInicio,
                Empleado = p.Empleado.nombre,
                FechaRegistro = p.FechaRegistro,

            };

            return reporteDiseño;
        }


        public async Task<ReporteDiseñoDto> postReporteDiseño(ReporteDiseño rd)
        {
            var token = _AsinacionesService.GetTokenFromHeader();
            var IdEmpleado = _AsinacionesService.GetClaimValue(token!, "IdEmpleado");
            rd.Id = _AsinacionesService.GenerateNewId();
            rd.EmpleadoId = IdEmpleado ?? "";
            rd.FechaRegistro = _AsinacionesService.GetCurrentDateTime();
            rd.created_at = _AsinacionesService.GetCurrentDateTime();
            rd.updated_at = _AsinacionesService.GetCurrentDateTime();
            rd.adicionado_por = _AsinacionesService.GetClaimValue(token!, "User") ?? "Sistema";
            rd.modificado_por = _AsinacionesService.GetClaimValue(token!, "User") ?? "Sistema";
            await _reporteDiseñoRepository.postReporteDiseño(rd);
            return new ReporteDiseñoDto
            {
                Id = rd.Id!,
                HoraFin = rd.HoraFin,
                HoraInicio = rd.HoraInicio,
                TipoSeccion = rd.Seccion.Nombre,
                PaginaFin = rd.PaginaFin,
                Observacion = rd.Observacion,
                PaginaInicio = rd.PaginaInicio,
                Empleado = rd.Empleado.nombre,
                FechaRegistro = rd.FechaRegistro,

            };

        }

        public async Task<ReporteDiseñoDto> putReporteDiseño(string id, ReporteDiseño seccion)
        {
            var reporteFound = await _reporteDiseñoRepository.getReporteDiseñoById(id);

            if (reporteFound == null)
            {
                throw new KeyNotFoundException("Tipo sección no encontrado.");
            }
            var token = _AsinacionesService.GetTokenFromHeader();
            reporteFound.ActualizarPropiedades(seccion);
            reporteFound.updated_at = _AsinacionesService.GetCurrentDateTime();
            reporteFound.modificado_por = _AsinacionesService.GetClaimValue(token!, "User") ?? "Sistema";

            await _reporteDiseñoRepository.putReporteDiseño(reporteFound);
            return new ReporteDiseñoDto
            {
                Id = reporteFound.Id!,
                HoraFin = reporteFound.HoraFin,
                HoraInicio = reporteFound.HoraInicio,
                TipoSeccion = reporteFound.Seccion.Nombre,
                PaginaFin = reporteFound.PaginaFin,
                Observacion = reporteFound.Observacion,
                PaginaInicio = reporteFound.PaginaInicio,
                Empleado = reporteFound.Empleado.nombre,
                FechaRegistro = reporteFound.FechaRegistro

            };
        }
    }
}
