using AutoGestion.interfaces.IPuesto;
using AutoGestion.interfaces;
using AutoGestion.interfaces.iTipoDeduccion;
using AutoGestion.Models;
using AutoGestion.Repositories;
using AutoGestion.Utils;

namespace AutoGestion.services
{
    public class TipoDeduccionService : ITipoDeduccionService
    {
        private readonly ITipoDeduccionRepository _tipoDeduccionRepository;
        private readonly IAsignaciones _AsinacionesService;

        public TipoDeduccionService(ITipoDeduccionRepository tipoDeduccionService, IAsignaciones asignacionesService)
        {
            _tipoDeduccionRepository = tipoDeduccionService;
            _AsinacionesService = asignacionesService;
        }
        public async Task<IEnumerable<TipoDeduccionDto>> GetTipoDeducciones()
        {

            var puestos = await _tipoDeduccionRepository.GetTipoDeducciones();
            if (puestos == null)
            {
                throw new KeyNotFoundException("Lista de tipo deducciones Vacia.");
            }
            var puestosDto = puestos.Select(p => new TipoDeduccionDto
            {
                Id = p.Id!,
                Nombre = p.Nombre,
                Activo = p.Activo,
                Descripcion = p.Descripcion
            });

            return puestosDto;
        }
        public async Task<TipoDeduccionDto?> GetTipoDeduccionById(string id)
        {
            var puesto = await _tipoDeduccionRepository.GetTipoDeduccionById(id);

            if (puesto == null)
            {
                throw new KeyNotFoundException("Tipo deducción no encontrado.");
            }

            var PuestoDto = new TipoDeduccionDto
            {
                Id = puesto.Id!,
                Nombre = puesto.Nombre,
                Descripcion = puesto.Descripcion,
                Activo = puesto.Activo
            };

            return PuestoDto;
        }




        public async Task<IEnumerable<TipoDeduccionDto>> GetTipoDeduccionesActivos()
        {
            var puestos = await _tipoDeduccionRepository.GetTipoDeduccionesActivos();
            if (puestos == null)
            {
                throw new KeyNotFoundException("Lista de tipo deducciones Vacia.");
            }
            var puestosDto = puestos.Select(e => new TipoDeduccionDto
            {
                Id = e.Id!,
                Nombre = e.Nombre,
                Activo = e.Activo,
                Descripcion = e.Descripcion

            });
            return puestosDto;
        }

        public async Task<TipoDeduccionDto> PostTipoDeduccion(TipoDeduccion tipoDeduccion)
        {
            var token = _AsinacionesService.GetTokenFromHeader();
            tipoDeduccion.Id = _AsinacionesService.GenerateNewId();
            tipoDeduccion.Created_at = _AsinacionesService.GetCurrentDateTime();
            tipoDeduccion.Updated_at = _AsinacionesService.GetCurrentDateTime();
            tipoDeduccion.Adicionado_por = _AsinacionesService.GetClaimValue(token!, "User") ?? "Sistema";
            tipoDeduccion.Modificado_por = _AsinacionesService.GetClaimValue(token!, "User") ?? "Sistema";
            await _tipoDeduccionRepository.PostTipoDeduccion(tipoDeduccion);
            return new TipoDeduccionDto
            {
                Id = tipoDeduccion.Id,
                Nombre = tipoDeduccion.Nombre,
                Descripcion = tipoDeduccion.Descripcion,
                Activo = tipoDeduccion.Activo,
            };

        }

        public async Task<TipoDeduccionDto> PutTipoDeduccion(string id, TipoDeduccion puesto)
        {
            var puestoFound = await _tipoDeduccionRepository.GetTipoDeduccionById(id);

            if (puestoFound == null)
            {
                throw new KeyNotFoundException("Tipo Deduccion no encontrado.");
            }
            var token = _AsinacionesService.GetTokenFromHeader();
            puestoFound.ActualizarPropiedades(puesto);
            puestoFound.Activo = puesto.Activo;
            puestoFound.Updated_at = _AsinacionesService.GetCurrentDateTime();
            puestoFound.Modificado_por = _AsinacionesService.GetClaimValue(token!, "User") ?? "Sistema";

            await _tipoDeduccionRepository.PutTipoDeduccion(id, puestoFound);
            return new TipoDeduccionDto
            {
                Id = puestoFound.Id!,
                Nombre = puestoFound.Nombre,
                Activo = puestoFound.Activo,
                Descripcion = puestoFound.Descripcion

            };
        }


    }
}
