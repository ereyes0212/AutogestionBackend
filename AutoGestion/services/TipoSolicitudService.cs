using AutoGestion.interfaces;
using AutoGestion.models.Empresa;
using AutoGestion.Models;
using AutoGestion.repositories;
using AutoGestion.Utils;

namespace AutoGestion.services
{
    public class TipoSolicitudService : ITipoSolicitudService
    {
        private readonly ITipoSolicitudRepository _tipoSolicitudRepository;
        private readonly IAsignaciones _AsinacionesService;

        public TipoSolicitudService(ITipoSolicitudRepository tipoSolicitudRepository, IAsignaciones asignacionesService)
        {
            _tipoSolicitudRepository = tipoSolicitudRepository;
            _AsinacionesService = asignacionesService;
        }

        public async Task<IEnumerable<TipoSolicitudDto>> GetTipoSolicitud()
        {

            var puestos = await _tipoSolicitudRepository.GetTipoSolicitud();
            if (puestos == null)
            {
                throw new KeyNotFoundException("Lista de puestos Vacia.");
            }
            var puestosDto = puestos.Select(p => new TipoSolicitudDto
            {
                Id = p.Id!,
                Nombre = p.Nombre,
                Activo = p.activo,
                Descripcion = p.Descripcion
            });

            return puestosDto;
        }
        public async Task<IEnumerable<TipoSolicitudDto>> GetTipoSolicitudActivos()
        {

            var puestos = await _tipoSolicitudRepository.GetTipoSolicitudActivas();
            if (puestos == null)
            {
                throw new KeyNotFoundException("Lista de puestos Vacia.");
            }
            var puestosDto = puestos.Select(p => new TipoSolicitudDto
            {
                Id = p.Id!,
                Nombre = p.Nombre,
                Activo = p.activo,
                Descripcion = p.Descripcion
            });

            return puestosDto;
        }
        public async Task<TipoSolicitudDto> GetTipoSolicitudById(string id)
        {

            var tipoSolicitud = await _tipoSolicitudRepository.GetTipoSolicitudById(id);
            if (tipoSolicitud == null)
            {
                throw new KeyNotFoundException("Lista de puestos Vacia.");
            }
            var puestoDto = new TipoSolicitudDto
            {
                Id = tipoSolicitud.Id!,
                Nombre = tipoSolicitud.Nombre,
                Descripcion = tipoSolicitud.Descripcion,
                Activo = tipoSolicitud.activo
            };

            return puestoDto;
        }

        public async Task<TipoSolicitudDto> PostTipoSolicitud(TipoSolicitud tipoSolicitud)
        {
            var token = _AsinacionesService.GetTokenFromHeader();
            tipoSolicitud.Id = _AsinacionesService.GenerateNewId();
            tipoSolicitud.created_at = _AsinacionesService.GetCurrentDateTime();
            tipoSolicitud.updated_at = _AsinacionesService.GetCurrentDateTime();
            tipoSolicitud.adicionado_por = _AsinacionesService.GetClaimValue(token!, "User") ?? "Sistema";
            tipoSolicitud.modificado_por = _AsinacionesService.GetClaimValue(token!, "User") ?? "Sistema";
            await _tipoSolicitudRepository.PostTipoSolicitud(tipoSolicitud);
            return new TipoSolicitudDto
            {
                Id = tipoSolicitud.Id,
                Nombre = tipoSolicitud.Nombre,
                Descripcion = tipoSolicitud.Descripcion,
                Activo = tipoSolicitud.activo,
            };

        }

        public async Task<TipoSolicitudDto> PutTipoSolicitud(string id, TipoSolicitud tipoSolicitud)
        {
            var tipoSolicitudFound = await _tipoSolicitudRepository.GetTipoSolicitudById(id);

            if (tipoSolicitudFound == null)
            {
                throw new KeyNotFoundException("Tipo soliciutd no encontrado.");
            }
            var token = _AsinacionesService.GetTokenFromHeader();
            tipoSolicitudFound.ActualizarPropiedades(tipoSolicitud);
            tipoSolicitudFound.activo = tipoSolicitud.activo;
            tipoSolicitudFound.updated_at = _AsinacionesService.GetCurrentDateTime();
            tipoSolicitudFound.modificado_por = _AsinacionesService.GetClaimValue(token!, "User") ?? "Sistema";

            await _tipoSolicitudRepository.PutTipoSolicitud(id, tipoSolicitudFound);
            return new TipoSolicitudDto
            {
                Id = tipoSolicitudFound.Id!,
                Nombre = tipoSolicitudFound.Nombre,
                Activo = tipoSolicitudFound.activo,
                Descripcion = tipoSolicitudFound.Descripcion

            };
        }


    }
}
