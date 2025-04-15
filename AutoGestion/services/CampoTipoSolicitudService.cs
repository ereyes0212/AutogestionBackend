using AutoGestion.interfaces;
using AutoGestion.interfaces.ICampoTipoSolicitud;
using AutoGestion.Models;
using AutoGestion.Models.CampoTipoSolicitudDto;
using AutoGestion.repositories;
using AutoGestion.Utils;

namespace AutoGestion.services
{
    public class CampoTipoSolicitudService : ICampoTipoSolicitudService
    {
        private readonly ICampoTipoSolicitudRepository _tipoCampoSolicitudRepository;
        private readonly IAsignaciones _AsinacionesService;

        public CampoTipoSolicitudService(ICampoTipoSolicitudRepository campoTipoSolicitudRepository, IAsignaciones asignacionesService)
        {
            _tipoCampoSolicitudRepository = campoTipoSolicitudRepository;
            _AsinacionesService = asignacionesService;
        }
        public async Task<IEnumerable<CampoTipoSolicitudDto>> GetCampoTipoSolicitud()
        {

            var puestos = await _tipoCampoSolicitudRepository.GetCamposTipoSolicitud();
            if (puestos == null)
            {
                throw new KeyNotFoundException("Lista de puestos Vacia.");
            }
            var puestosDto = puestos.Select(p => new CampoTipoSolicitudDto
            {
                Id = p.Id!,
                Nombre = p.Nombre,
                Activo = p.Activo,
                EsRequerido = p.EsRequerido,
                nivel = p.nivel,
                TipoCampo = p.TipoCampo,
                TipoSolicitud = p.TipoSolicitud.Nombre,
                TipoSolicitudId = p.TipoSolicitudId
            });

            return puestosDto;
        }        
        public async Task<IEnumerable<CampoTipoSolicitudDto>> GetCampoTipoSolicitudByTipoSolicitud(string id)
        {

            var puestos = await _tipoCampoSolicitudRepository.GetCamposTipoSolicitudByTipoSolicitud(id);
            if (puestos == null)
            {
                throw new KeyNotFoundException("Campos Vacio.");
            }
            var puestosDto = puestos.Select(p => new CampoTipoSolicitudDto
            {
                Id = p.Id!,
                Nombre = p.Nombre,
                Activo = p.Activo,
                EsRequerido = p.EsRequerido,
                nivel = p.nivel,
                TipoCampo = p.TipoCampo,
                TipoSolicitud = p.TipoSolicitud.Nombre,
                TipoSolicitudId = p.TipoSolicitudId
            });

            return puestosDto;
        }       
        public async Task<IEnumerable<CampoTipoSolicitudDto>> GetCampoTipoSolicitudActivasByTipoSolicitud(string id)
        {

            var puestos = await _tipoCampoSolicitudRepository.GetCampoTipoSolicitudActivasByTipoSolicitud(id);
            if (puestos == null)
            {
                throw new KeyNotFoundException("Campos Vacio.");
            }
            var puestosDto = puestos.Select(p => new CampoTipoSolicitudDto
            {
                Id = p.Id!,
                Nombre = p.Nombre,
                Activo = p.Activo,
                EsRequerido = p.EsRequerido,
                nivel = p.nivel,
                TipoCampo = p.TipoCampo,
                TipoSolicitud = p.TipoSolicitud.Nombre,
                TipoSolicitudId = p.TipoSolicitudId
            });

            return puestosDto;
        }
        public async Task<IEnumerable<CampoTipoSolicitudDto>> GetCampoTipoSolicitudActivos()
        {

            var campos = await _tipoCampoSolicitudRepository.GetCampoTipoSolicitudActivas();
            if (campos == null)
            {
                throw new KeyNotFoundException("Lista de puestos Vacia.");
            }
            var campoDto = campos.Select(p => new CampoTipoSolicitudDto
            {
                Id = p.Id!,
                Nombre = p.Nombre,
                Activo = p.Activo,
                EsRequerido = p.EsRequerido,
                nivel = p.nivel,
                TipoCampo = p.TipoCampo,
                TipoSolicitud = p.TipoSolicitud.Nombre,
                TipoSolicitudId = p.TipoSolicitudId
            });

            return campoDto;
        }

        public async Task<CampoTipoSolicitudDto> GetCampoTipoSolicitudById(string id)
        {

            var p = await _tipoCampoSolicitudRepository.GetCampoTipoSolicitudById(id);
            if (p == null)
            {
                throw new KeyNotFoundException("Lista de puestos Vacia.");
            }
            var campoDto =  new CampoTipoSolicitudDto
            {
                Id = p.Id!,
                Nombre = p.Nombre,
                Activo = p.Activo,
                EsRequerido = p.EsRequerido,
                nivel = p.nivel,
                TipoCampo = p.TipoCampo,
                TipoSolicitud = p.TipoSolicitud.Nombre,
                TipoSolicitudId = p.TipoSolicitudId
            };

            return campoDto;
        }

        public async Task<CampoTipoSolicitudDto> PostCampoTipoSolicitud(CamposTipoSolicitud p)
        {
            var token = _AsinacionesService.GetTokenFromHeader();
            p.Id = _AsinacionesService.GenerateNewId();
            p.Created_at = _AsinacionesService.GetCurrentDateTime();
            p.Updated_at = _AsinacionesService.GetCurrentDateTime();
            p.Adicionado_por = _AsinacionesService.GetClaimValue(token!, "User") ?? "Sistema";
            p.Modificado_por = _AsinacionesService.GetClaimValue(token!, "User") ?? "Sistema";
            await _tipoCampoSolicitudRepository.PostCampoTipoSolicitud(p);
            var campoDto = new CampoTipoSolicitudDto
            {
                Id = p.Id!,
                Nombre = p.Nombre,
                Activo = p.Activo,
                EsRequerido = p.EsRequerido,
                nivel = p.nivel,
                TipoCampo = p.TipoCampo,
                TipoSolicitud = p.TipoSolicitud.Nombre,
                TipoSolicitudId = p.TipoSolicitudId
            };
            return campoDto;
        }

        public async Task<CampoTipoSolicitudDto> PutCampoTipoSolicitud(string id, CamposTipoSolicitud tipoSolicitud)
        {
            var CampotipoSolicitudFound = await _tipoCampoSolicitudRepository.GetCampoTipoSolicitudById(id);

            if (CampotipoSolicitudFound == null)
            {
                throw new KeyNotFoundException("Tipo soliciutd no encontrado.");
            }
            var token = _AsinacionesService.GetTokenFromHeader();
            CampotipoSolicitudFound.ActualizarPropiedades(tipoSolicitud);
            CampotipoSolicitudFound.Activo = tipoSolicitud.Activo;
            CampotipoSolicitudFound.Updated_at = _AsinacionesService.GetCurrentDateTime();
            CampotipoSolicitudFound.Modificado_por = _AsinacionesService.GetClaimValue(token!, "User") ?? "Sistema";

            await _tipoCampoSolicitudRepository.PutCampoTipoSolicitud(id, CampotipoSolicitudFound);
            var campoDto = new CampoTipoSolicitudDto
            {
                Id = CampotipoSolicitudFound.Id!,
                Nombre = CampotipoSolicitudFound.Nombre,
                Activo = CampotipoSolicitudFound.Activo,
                EsRequerido = CampotipoSolicitudFound.EsRequerido,
                nivel = CampotipoSolicitudFound.nivel,
                TipoCampo = CampotipoSolicitudFound.TipoCampo,
                TipoSolicitud = CampotipoSolicitudFound.TipoSolicitud.Nombre,
                TipoSolicitudId = CampotipoSolicitudFound.TipoSolicitudId
            };
            return campoDto;
        }
    }
}
