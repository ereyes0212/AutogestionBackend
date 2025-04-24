
using AutoGestion.interfaces;
using AutoGestion.interfaces.IConfiguracion;
using AutoGestion.Interfaces.ISolicitudVacaciones;
using AutoGestion.Models;
using AutoGestion.Models.SolicitudVacacionesDto;

namespace AutoGestion.Services.SolicitudVacaciones
{
    public class SolicitudVacacionesService : ISolicitudVacacionesService
    {
        private readonly ISolicitudVacacionesRepository _repo;
        private readonly IConfiguracionAprobacionRepository _cfgRepo;
        private readonly IEmpleadoRepository _empleadoRepo;
        private readonly IAsignaciones _utils;

        public SolicitudVacacionesService(
            ISolicitudVacacionesRepository repo,
            IConfiguracionAprobacionRepository cfgRepo,
            IEmpleadoRepository empleadoRepo,
            IAsignaciones utils)
        {
            _repo = repo;
            _cfgRepo = cfgRepo;
            _utils = utils;
            _empleadoRepo = empleadoRepo;
        }

        public async Task<IEnumerable<SolicitudVacacionDto>> GetSolicitudesAsync()
        {
            var list = await _repo.GetSolicitudesAsync();
            return list.Select(MapToDto);
        }

        public async Task<SolicitudVacacionDto> GetSolicitudByIdAsync(string id)
        {
            var sol = await _repo.GetSolicitudByIdAsync(id)
                      ?? throw new KeyNotFoundException("Solicitud no encontrada");
            return MapToDto(sol);
        }

        public async Task<IEnumerable<SolicitudVacacionDto>> GetSolicitudesPorEmpleadoAsync()
        {
            var token = _utils.GetTokenFromHeader();
            var IdEmpleado = _utils.GetClaimValue(token!, "IdEmpleado");
            var list = await _repo.GetSolicitudesPorEmpleadoAsync(IdEmpleado);
            return list.Select(MapToDto);
        }

        public async Task<SolicitudVacacionDto> CrearSolicitudAsync(SolicitudVacacionCreateDto dto)
        {
            var token = _utils.GetTokenFromHeader();
            var IdEmpleado = _utils.GetClaimValue(token!, "IdEmpleado");
            var PuestoId = _utils.GetClaimValue(token!, "PuestoId");

            var cfgs = await _cfgRepo.GetAprobacionesActivos();

            // Validación de fechas
            if (dto.FechaFin < dto.FechaInicio)
                throw new ArgumentException("FechaFin debe ser igual o posterior a FechaInicio");

            // Crear la solicitud de vacaciones
            var sol = new SolicitudVacacion
            {
                Id = _utils.GenerateNewId(),
                EmpleadoId = IdEmpleado,
                PuestoId = PuestoId,
                FechaSolicitud = DateTime.UtcNow,
                FechaInicio = dto.FechaInicio,
                FechaFin = dto.FechaFin,
                Descripcion = dto.Descripcion,
                Aprobado = null,
                CreatedAt = DateTime.UtcNow
            };

            // Obtener las aprobaciones asociadas con la solicitud
            sol.Aprobaciones = new List<SolicitudVacacionAprobacion>();

            foreach (var cfg in cfgs)
            {
                var aprobacion = new SolicitudVacacionAprobacion
                {
                    Id = _utils.GenerateNewId(),
                    Descripcion = cfg.Descripcion,
                    ConfiguracionAprobacionId = cfg.Id!,
                    Nivel = cfg.nivel,
                    Estado = "Pendiente",
                    CreatedAt = DateTime.UtcNow
                };

                // Verificar si el puesto es fijo o dinámico
                if (cfg.Tipo == "Fijo")
                {
                    // Asignar el empleado correspondiente para aprobar la solicitud (fijo)
                    var empleado = await _empleadoRepo.GetEmpleadoByPuesto(cfg.puesto_id);

                    aprobacion.EmpleadoAprobadorId = empleado.id; // empleado asignado al puesto fijo
                }
                else if (cfg.Tipo == "Dinamico")
                {
                    // Si el puesto es dinámico, obtener el jefe del empleado que hace la solicitud
                    var jefeEmpleado = await _empleadoRepo.GetEmpleadoById(IdEmpleado);
                    if (jefeEmpleado != null)
                    {
                        aprobacion.EmpleadoAprobadorId = jefeEmpleado.jefe_id; // Asignar al jefe para aprobar
                    }
                    else
                    {
                        throw new ArgumentException("No se pudo encontrar el jefe del empleado para la aprobación dinámica");
                    }
                }

                sol.Aprobaciones.Add(aprobacion);
            }

            // Guardar la solicitud en el repositorio
            var created = await _repo.AddSolicitudAsync(sol);

            return MapToDto(created);
        }

        public async Task<SolicitudVacacionDto> ProcesarAprobacionAsync(
            string solicitudId,
            int nivel,
            bool aprobado,
            string comentarios)
        {
            var token = _utils.GetTokenFromHeader();

            var sol = await _repo.GetSolicitudByIdAsync(solicitudId)
                      ?? throw new KeyNotFoundException("Solicitud no encontrada");

            var paso = sol.Aprobaciones
                          .FirstOrDefault(a => a.Nivel == nivel)
                       ?? throw new InvalidOperationException($"No existe el paso de nivel {nivel}");

            // 5) actualizar paso
            paso.Estado = aprobado ? "Aprobado" : "Rechazado";
            paso.Comentarios = comentarios;
            paso.EmpleadoAprobadorId = _utils.GetClaimValue(token!, "IdEmpleado") ?? "Sistema";
            paso.FechaDecision = DateTime.UtcNow;
            paso.UpdatedAt = DateTime.UtcNow;

            // 6) actualizar estado global
            if (!aprobado)
                sol.Aprobado = false;
            else if (sol.Aprobaciones.All(a => a.Estado == "Aprobado"))
                sol.Aprobado = true;

            sol.UpdatedAt = DateTime.UtcNow;

            await _repo.UpdateSolicitudAsync(sol);
            return MapToDto(sol);
        }

        private SolicitudVacacionDto MapToDto(SolicitudVacacion s) => new()
        {
            Id = s.Id,
            EmpleadoId = s.EmpleadoId,
            NombreEmpleado = $"{s.Empleado?.nombre} {s.Empleado?.apellido}",
            PuestoId = s.PuestoId,
            Puesto = s.Puesto.Nombre,
            FechaSolicitud = s.FechaSolicitud,
            FechaInicio = s.FechaInicio,
            FechaFin = s.FechaFin,
            DiasSolicitados = s.DiasSolicitados,
            Aprobado = s.Aprobado,
            Descripcion = s.Descripcion,
            Aprobaciones = s.Aprobaciones
                                  .OrderBy(a => a.Nivel)
                                  .Select(a => new AprobacionVacacionDto
                                  {
                                      Id = a.Id,
                                      Nivel = a.Nivel,
                                      Descripcion = a.Descripcion,
                                      Aprobado = a.Estado == "Aprobado"
                                                          ? (bool?)true
                                                          : a.Estado == "Rechazado"
                                                            ? (bool?)false
                                                            : null,
                                      Comentario = a.Comentarios,
                                      FechaAprobacion = a.FechaDecision
                                  })
                                  .ToList()
        };

        // AutoGestion.Services.SolicitudVacaciones/SolicitudVacacionesService.cs
        public async Task<IEnumerable<AprobacionVacacionDto>> GetAprobacionesPorEmpleado()
        {
            // 1) Extraer el empleadoId del token
            var token = _utils.GetTokenFromHeader();
            var empleadoId = _utils.GetClaimValue(token!, "IdEmpleado")
                            ?? throw new UnauthorizedAccessException("No se pudo identificar el empleado.");

            // 2) Llamar al repositorio (que ya filtra por EmpleadoAprobadorId y Estado = "Pendiente")
            var aprobaciones = await _repo.GetAprobacionesPorEmpleado(empleadoId);

            // 3) Mapear a DTO
            return aprobaciones.Select(a => new AprobacionVacacionDto
            {
                Id = a.Id,
                IdSolicitud = a.SolicitudVacacionId,
                Nivel = a.Nivel,
                Aprobado = null,  // siempre null porque son "Pendiente"
                Comentario = a.Comentarios,
                FechaAprobacion = a.FechaDecision,

                // Datos de la solicitud asociada para mostrar contexto en el front
                EmpleadoId = a.SolicitudVacacion.EmpleadoId,
                NombreEmpleado = $"{a.SolicitudVacacion.Empleado.nombre} {a.SolicitudVacacion.Empleado.apellido}",
                PuestoId = a.SolicitudVacacion.PuestoId,
                Puesto = a.SolicitudVacacion.Puesto.Nombre,
                FechaSolicitud = a.SolicitudVacacion.FechaSolicitud,
                FechaInicio = a.SolicitudVacacion.FechaInicio,
                FechaFin = a.SolicitudVacacion.FechaFin,
                DiasSolicitados = a.SolicitudVacacion.DiasSolicitados,
                Descripcion = a.SolicitudVacacion.Descripcion
            })
            .ToList();
        }
        // AutoGestion.Services.SolicitudVacaciones/SolicitudVacacionesService.cs
        public async Task<IEnumerable<AprobacionVacacionDto>> GetAprobacionesPorEmpleadoHistorico()
        {
            // 1) Extraer el empleadoId del token
            var token = _utils.GetTokenFromHeader();
            var empleadoId = _utils.GetClaimValue(token!, "IdEmpleado")
                            ?? throw new UnauthorizedAccessException("No se pudo identificar el empleado.");

            // 2) Llamar al repositorio (que ya filtra por EmpleadoAprobadorId y Estado = "Pendiente")
            var aprobaciones = await _repo.GetAprobacionesPorEmpleadoHistorico(empleadoId);

            // 3) Mapear a DTO
            return aprobaciones.Select(a => new AprobacionVacacionDto
            {
                Id = a.Id,
                IdSolicitud = a.SolicitudVacacionId,
                Nivel = a.Nivel,
                Aprobado = a.Estado == "Aprobado"
                ? (bool?)true
    : a.Estado == "Rechazado"
        ? (bool?)false
        : null,
                Comentario = a.Comentarios,
                FechaAprobacion = a.FechaDecision,

                // Datos de la solicitud asociada para mostrar contexto en el front
                EmpleadoId = a.SolicitudVacacion.EmpleadoId,
                NombreEmpleado = $"{a.SolicitudVacacion.Empleado.nombre} {a.SolicitudVacacion.Empleado.apellido}",
                PuestoId = a.SolicitudVacacion.PuestoId,
                Puesto = a.SolicitudVacacion.Puesto.Nombre,
                FechaSolicitud = a.SolicitudVacacion.FechaSolicitud,
                FechaInicio = a.SolicitudVacacion.FechaInicio,
                FechaFin = a.SolicitudVacacion.FechaFin,
                DiasSolicitados = a.SolicitudVacacion.DiasSolicitados,
                Descripcion = a.SolicitudVacacion.Descripcion
            })
            .ToList();
        }



    }
}
