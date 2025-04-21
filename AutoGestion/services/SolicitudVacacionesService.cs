// AutoGestion.Services/SolicitudVacaciones/SolicitudVacacionesService.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoGestion.interfaces.IConfiguracion;
using AutoGestion.interfaces.ISolicitudVacaciones;
using AutoGestion.interfaces;
using AutoGestion.Models;
using AutoGestion.Models.AutoGestion.Models;
using AutoGestion.Models.SolicitudVacacionesDto;
using AutoGestion.services;

namespace AutoGestion.Services.SolicitudVacaciones
{
    public class SolicitudVacacionesService : ISolicitudVacacionesService
    {
        private readonly ISolicitudVacacionesRepository _repo;
        private readonly IConfiguracionAprobacionRepository _cfgRepo;
        private readonly IAsignaciones _utils;

        public SolicitudVacacionesService(
            ISolicitudVacacionesRepository repo,
            IConfiguracionAprobacionRepository cfgRepo,
            IAsignaciones utils)
        {
            _repo = repo;
            _cfgRepo = cfgRepo;
            _utils = utils;
        }

        public async Task<IEnumerable<SolicitudVacacionDto>> GetSolicitudes()
        {
            var list = await _repo.GetSolicitudes();
            return list.Select(MapToDto);
        }

        public async Task<SolicitudVacacionDto> GetSolicitudById(string id)
        {
            var sol = await _repo.GetSolicitudById(id)
                      ?? throw new KeyNotFoundException("Solicitud no encontrada");
            return MapToDto(sol);
        }

        public async Task<IEnumerable<SolicitudVacacionDto>> GetSolicitudesPorEmpleado(string empleadoId)
        {
            var list = await _repo.GetSolicitudesPorEmpleado(empleadoId);
            return list.Select(MapToDto);
        }

        public async Task<SolicitudVacacionDto> CrearSolicitud(SolicitudVacacionCreateDto dto)
        {
            // Mapear DTO → Entidad básica
            var sol = new SolicitudVacacion
            {
                Id = _utils.GenerateNewId(),
                EmpleadoId = dto.EmpleadoId,
                FechaIngreso = dto.FechaIngreso,
                FechaSolicitud = DateTime.UtcNow,
                FechaGoce = dto.FechaInicio,
                FechaRegreso = dto.FechaFin,
                Descripcion = dto.Descripcion,
                PeriodoVacaciones = "prueba",
                PuestoId= dto.PuestoId,
                DiasPendientesFecha = dto.DiasPendientesFecha,
                TotalDiasAutorizados = dto.TotalDiasSolicitados,
                TotalDiasPendientes = dto.DiasPendientesFecha - dto.TotalDiasSolicitados,
                Aprobado = false,
                CreatedAt = DateTime.UtcNow
            };

            // Inicializar pasos según configuración
            var cfgs = await _cfgRepo.GetAprobacionesActivosByEmpresaId(dto.EmpresaId);
            sol.Aprobaciones = cfgs.Select(c => new SolicitudVacacionAprobacion
            {
                Id = _utils.GenerateNewId(),
                ConfiguracionAprobacionId = c.Id!,
                Nivel = c.nivel,
                Estado = "Pendiente",
                CreatedAt = DateTime.UtcNow
            }).ToList();

            var created = await _repo.AddSolicitud(sol);
            return MapToDto(created);
        }

        public async Task<SolicitudVacacionDto> ProcesarAprobacion(string solicitudId, int nivel, bool aprobado, string comentarios)
        {
            var token = _utils.GetTokenFromHeader();

            var sol = await _repo.GetSolicitudById(solicitudId)
                      ?? throw new KeyNotFoundException("Solicitud no encontrada");

            var paso = sol.Aprobaciones.FirstOrDefault(a => a.Nivel == nivel)
                       ?? throw new InvalidOperationException($"No existe el paso de nivel {nivel}");

            paso.Estado = aprobado ? "Aprobado" : "Rechazado";
            paso.Comentarios = comentarios;
            paso.EmpleadoAprobadorId = _utils.GetClaimValue(token!, "IdEmpleado") ?? "Sistema";
            paso.FechaDecision = DateTime.UtcNow;
            paso.UpdatedAt = DateTime.UtcNow;

            if (!aprobado)
            {
                sol.Aprobado = false;
            }
            else if (sol.Aprobaciones.All(a => a.Estado == "Aprobado"))
            {
                sol.Aprobado = true;
            }

            sol.UpdatedAt = DateTime.UtcNow;
            await _repo.UpdateSolicitud(sol);

            return MapToDto(sol);
        }

        private SolicitudVacacionDto MapToDto(SolicitudVacacion s) => new()
        {
            Id = s.Id,
            EmpleadoId = s.EmpleadoId,
            NombreEmpleado = s.Empleado?.nombre + " " + s.Empleado?.apellido,
            FechaIngreso = s.FechaIngreso,
            FechaSolicitud = s.FechaSolicitud,
            FechaInicio = s.FechaGoce,
            FechaFin = s.FechaRegreso,
            TotalDiasSolicitados = s.TotalDiasAutorizados,
            DiasPendientes = s.TotalDiasPendientes,
            Aprobado = s.Aprobado,
            Descripcion = s.Descripcion,
            Aprobaciones = s.Aprobaciones
                                   .OrderBy(a => a.Nivel)
                                   .Select(a => new AprobacionVacacionDto
                                   {
                                       Id = a.Id,
                                       Nivel = a.Nivel,
                                       Aprobado = a.Estado == "Aprobado"
                                                           ? (bool?)true
                                                           : a.Estado == "Rechazado"
                                                             ? (bool?)false
                                                             : null,
                                       Comentario = a.Comentarios,
                                       FechaAprobacion = a.FechaDecision
                                   }).ToList()
        };
    }
}
