using AutoGestion.interfaces;
using AutoGestion.interfaces.IConfiguracion;
using AutoGestion.Models;
using AutoGestion.Models.ConfiguracionPuesto; // Asumiendo que el DTO está aquí
using AutoGestion.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoGestion.services
{
    public class ConfiguracionAprobacionService : IConfiguracionAprobacionService
    {
        private readonly IConfiguracionAprobacionRepository _configuracionRepository;
        private readonly IAsignaciones _asignacionesService;

        public ConfiguracionAprobacionService(IConfiguracionAprobacionRepository configuracionRepository, IAsignaciones asignacionesService)
        {
            _configuracionRepository = configuracionRepository;
            _asignacionesService = asignacionesService;
        }

        public async Task<IEnumerable<ConfiguracionAprobacionDto>> GetAprobaciones()
        {
            var configs = await _configuracionRepository.GetAprobaciones();
            if (configs == null || !configs.Any())
            {
                throw new KeyNotFoundException("No hay configuraciones de aprobación registradas.");
            }

            var dtos = configs.Select(c => new ConfiguracionAprobacionDto
            {
                Id = c.Id!,
                Puesto = c.Puesto != null ? c.Puesto.Nombre : c.Descripcion,
                Descripcion = c.Descripcion!,
                Tipo = c.Tipo!,
                Nivel = c.nivel,
                Activo = c.Activo
            });

            return dtos;
        }

        public async Task<ConfiguracionAprobacionDto> GetAprobacionById(string id)
        {
            var config = await _configuracionRepository.GetAprobacionesById(id);
            if (config == null)
            {
                throw new KeyNotFoundException("Configuración de aprobación no encontrada.");
            }

            var dto = new ConfiguracionAprobacionDto
            {
                Id = config.Id!,
                Puesto = config.Puesto.Nombre,
                Descripcion = config.Descripcion!,
                Tipo = config.Tipo!,
                Nivel = config.nivel,
                Activo = config.Activo
            };

            return dto;
        }



        public async Task<IEnumerable<ConfiguracionAprobacionDto>> GetAprobacionesActivas()
        {
            var configs = await _configuracionRepository.GetAprobacionesActivos();
            if (configs == null || !configs.Any())
            {
                throw new KeyNotFoundException("No hay configuraciones de aprobación activas.");
            }

            var dtos = configs.Select(c => new ConfiguracionAprobacionDto
            {
                Id = c.Id!,
            return dtos;
        }
                Puesto = c.Puesto != null ? c.Puesto.Nombre : c.Descripcion,
                Descripcion = c.Descripcion!,
                Tipo = c.Tipo!,
                Nivel = c.nivel,
                Activo = c.Activo
            });

            return dtos;
        }

        // Inserta en lote (por ejemplo, luego de un drag-and-drop desde el frontend)
        public async Task<IEnumerable<ConfiguracionAprobacionDto>> PostAprobaciones(IEnumerable<ConfiguracionAprobacion> configuraciones)
        {


            await _configuracionRepository.DeleteAprobaciones();


            // Mapeo de entidad a ConfiguracionAprobacion
            foreach (var config in configuraciones)
            {
                config.Id = _asignacionesService.GenerateNewId();
                config.Created_at = _asignacionesService.GetCurrentDateTime();
                config.Activo = true;
                config.Updated_at = _asignacionesService.GetCurrentDateTime();
                config.Adicionado_por = _asignacionesService.GetClaimValue(_asignacionesService.GetTokenFromHeader()!, "User");
                config.Modificado_por = _asignacionesService.GetClaimValue(_asignacionesService.GetTokenFromHeader()!, "User");
            }

            // Guarda las configuraciones en la base de datos
            var savedConfigs = await _configuracionRepository.PostAprobaciones(configuraciones);

            // Mapeo de entidad a DTO para devolver los datos
            var dtos = savedConfigs.Select(c => new ConfiguracionAprobacionDto
            {
                Id = c.Id!,
                Puesto = c.Puesto != null ? c.Puesto.Nombre : c.Descripcion,
                Descripcion = c.Descripcion!,
                Tipo = c.Tipo!,
                Nivel = c.nivel,
                Activo = c.Activo
            });

            return dtos;
        }


        // Actualiza en lote (por ejemplo, tras reordenamiento)
        public async Task<IEnumerable<ConfiguracionAprobacionDto>> PutAprobaciones(IEnumerable<ConfiguracionAprobacionDto> configuraciones)
        {
            // Mapeo de DTO a Modelo para actualización
            var configsToUpdate = configuraciones.Select(dto => new ConfiguracionAprobacion
            {
                Id = dto.Id,
                puesto_id = dto.Puesto,
                Descripcion = dto.Descripcion,
                Tipo = dto.Tipo,
                nivel = dto.Nivel,
                Activo = dto.Activo,
                Updated_at = _asignacionesService.GetCurrentDateTime(),
                Modificado_por = _asignacionesService.GetClaimValue(_asignacionesService.GetTokenFromHeader()!, "User")
            }).ToList();

            var updatedConfigs = await _configuracionRepository.PutAprobaciones(configsToUpdate);

            var dtos = updatedConfigs.Select(c => new ConfiguracionAprobacionDto
            {
                Id = c.Id!,
                Puesto = c.Puesto != null ? c.Puesto.Nombre : c.Descripcion,
                Descripcion = c.Descripcion!,
                Tipo = c.Tipo!,
                Nivel = c.nivel,
                Activo = c.Activo
            });

            return dtos;
        }


    }
}
