using AutoGestion.interfaces.IPuesto;
using AutoGestion.interfaces;
using AutoGestion.interfaces.iTipoDeduccion;
using AutoGestion.Models;
using AutoGestion.Repositories;
using AutoGestion.Utils;
using AutoGestion.interfaces.iTipoSeccion;

namespace AutoGestion.services
{
    public class TipoSeccionService : ITipoSeccionService
    {
        private readonly ITipoSeccionRepository _tipoSeccionRepository;
        private readonly IAsignaciones _AsinacionesService;

        public TipoSeccionService(ITipoSeccionRepository tipoSeccionRepository, IAsignaciones asignacionesService)
        {
            _tipoSeccionRepository = tipoSeccionRepository;
            _AsinacionesService = asignacionesService;
        }
        public async Task<IEnumerable<TipoSeccionDTO>> GetTipoSecciones()
        {

            var puestos = await _tipoSeccionRepository.GetTipoSeccion();
            if (puestos == null)
            {
                throw new KeyNotFoundException("Lista de tipo secciones Vacia.");
            }
            var SeccionDto = puestos.Select(p => new TipoSeccionDTO
            {
                Id = p.Id!,
                Nombre = p.Nombre,
                Activo = p.Activo,
                Descripcion = p.Descripcion
            });

            return SeccionDto;
        }
        public async Task<TipoSeccionDTO?> GetTipoSeccionById(string id)
        {
            var seccion = await _tipoSeccionRepository.GetTipoSeccionById(id);

            if (seccion == null)
            {
                throw new KeyNotFoundException("Tipo seccion no encontrado.");
            }

            var secciones = new TipoSeccionDTO
            {
                Id = seccion.Id!,
                Nombre = seccion.Nombre,
                Descripcion = seccion.Descripcion,
                Activo = seccion.Activo
            };

            return secciones;
        }




        public async Task<IEnumerable<TipoSeccionDTO>> GetTipoSeccionesActivos()
        {
            var puestos = await _tipoSeccionRepository.GetTipoSeccionActivos();
            if (puestos == null)
            {
                throw new KeyNotFoundException("Lista de tipo deducciones Vacia.");
            }
            var seccionesDto = puestos.Select(e => new TipoSeccionDTO
            {
                Id = e.Id!,
                Nombre = e.Nombre,
                Activo = e.Activo,
                Descripcion = e.Descripcion

            });
            return seccionesDto;
        }

        public async Task<TipoSeccionDTO> PostTipoSeccion(TipoSeccion tipoSeccion)
        {
            var token = _AsinacionesService.GetTokenFromHeader();
            tipoSeccion.Id = _AsinacionesService.GenerateNewId();
            tipoSeccion.Created_at = _AsinacionesService.GetCurrentDateTime();
            tipoSeccion.Updated_at = _AsinacionesService.GetCurrentDateTime();
            tipoSeccion.Adicionado_por = _AsinacionesService.GetClaimValue(token!, "User") ?? "Sistema";
            tipoSeccion.Modificado_por = _AsinacionesService.GetClaimValue(token!, "User") ?? "Sistema";
            await _tipoSeccionRepository.PostTipoSeccion(tipoSeccion);
            return new TipoSeccionDTO
            {
                Id = tipoSeccion.Id,
                Nombre = tipoSeccion.Nombre,
                Descripcion = tipoSeccion.Descripcion,
                Activo = tipoSeccion.Activo,
            };

        }

        public async Task<TipoSeccionDTO> PutTipoSeccion(string id, TipoSeccion seccion)
        {
            var seccionFound = await _tipoSeccionRepository.GetTipoSeccionById(id);

            if (seccionFound == null)
            {
                throw new KeyNotFoundException("Tipo sección no encontrado.");
            }
            var token = _AsinacionesService.GetTokenFromHeader();
            seccionFound.ActualizarPropiedades(seccion);
            seccionFound.Activo = seccion.Activo;
            seccionFound.Updated_at = _AsinacionesService.GetCurrentDateTime();
            seccionFound.Modificado_por = _AsinacionesService.GetClaimValue(token!, "User") ?? "Sistema";

            await _tipoSeccionRepository.PutTipoSeccion(seccionFound);
            return new TipoSeccionDTO
            {
                Id = seccionFound.Id!,
                Nombre = seccionFound.Nombre,
                Activo = seccionFound.Activo,
                Descripcion = seccionFound.Descripcion

            };
        }


    }
}
