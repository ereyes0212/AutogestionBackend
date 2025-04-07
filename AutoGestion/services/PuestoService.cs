using AutoGestion.interfaces;
using AutoGestion.interfaces.IEmpleado;
using AutoGestion.interfaces.IPuesto;
using AutoGestion.models.Empresa;
using AutoGestion.Models;
using AutoGestion.Utils;

namespace AutoGestion.services
{
    public class PuestoService : IPuestoService
    {
        private readonly IPuestoRepository _puestoRepository;
        private readonly IAsignaciones _AsinacionesService;

        public PuestoService(IPuestoRepository puestoRepository, IAsignaciones asignacionesService)
        {
            _puestoRepository = puestoRepository;
            _AsinacionesService = asignacionesService;
        }

        public async Task<IEnumerable<PuestoDto>> GetPuestos()
        {

            var puestos = await _puestoRepository.GetPuestos();
            if (puestos == null)
            {
                throw new KeyNotFoundException("Lista de puestos Vacia.");
            }
            var puestosDto = puestos.Select(p => new PuestoDto
            {
                Id = p.Id!,
                Nombre = p.Nombre,
                Activo = p.Activo,                
                empresa = p.Empresa.nombre,
                Descripcion = p.Descripcion
            });

            return puestosDto;
        }
        public async Task<PuestoDto?> GetPuestosById(string id)
        {
            var puesto = await _puestoRepository.GetPuestosById(id);

            if (puesto == null)
            {
                throw new KeyNotFoundException("Puesto no encontrado.");
            }

            var PuestoDto = new PuestoDto
            {
                Id = puesto.Id!,
                Nombre = puesto.Nombre,
                Descripcion = puesto.Descripcion,
                Activo = puesto.Activo,
                empresa = puesto.Empresa.nombre,
            };

            return PuestoDto;
        }

        public async Task<IEnumerable<PuestoDto?>> GetPuestosByEmpresaId()
        {
            var token = _AsinacionesService.GetTokenFromHeader();
            var empresaId = _AsinacionesService.GetClaimValue(token!, "IdEmpresa");
            var puestos = await _puestoRepository.GetPuestosByEmpresaId(empresaId!);

            if (puestos == null)
            {
                throw new KeyNotFoundException("Puestos no encontrado.");
            }

            var puestosDto = puestos.Select(e => new PuestoDto
            {
                Id = e.Id!,
                Nombre = e.Nombre,
                empresa = e.Empresa.nombre,
                Activo = e.Activo,
                Descripcion = e.Descripcion,
            });
            return puestosDto;
        }
        public async Task<IEnumerable<PuestoDto?>> GetPuestosActivosByEmpresaId()
        {
            var token = _AsinacionesService.GetTokenFromHeader();
            var empresaId = _AsinacionesService.GetClaimValue(token!, "IdEmpresa");
            var puestos = await _puestoRepository.GetPuestosActivosByEmpresaId(empresaId!);

            if (puestos == null)
            {
                throw new KeyNotFoundException("Puesto no encontrado.");
            }

            var PuestosDto = puestos.Select(e => new PuestoDto
            {
                Id = e.Id!,
                Nombre = e.Nombre,
                empresa = e.Empresa.nombre,
                Activo = e.Activo,
                Descripcion = e.Descripcion,
            });
            return PuestosDto;
        }


        public async Task<IEnumerable<PuestoDto>> GetPuestosActivos()
        {
            var puestos = await _puestoRepository.GetPuestosActivos();
            if (puestos == null)
            {
                throw new KeyNotFoundException("Lista de puestos Vacia.");
            }
            var puestosDto = puestos.Select(e => new PuestoDto
            {
                Id = e.Id!,
                Nombre = e.Nombre,
                empresa = e.Empresa.nombre,
                Activo = e.Activo,
                Descripcion = e.Descripcion

            });
            return puestosDto;
        }

        public async Task<PuestoDto> PostPuestos(Puesto puesto)
        {
            var token = _AsinacionesService.GetTokenFromHeader();
            puesto.Id = _AsinacionesService.GenerateNewId();
            puesto.Created_at = _AsinacionesService.GetCurrentDateTime();
            puesto.Updated_at = _AsinacionesService.GetCurrentDateTime();
            if (string.IsNullOrEmpty(puesto.Empresa_id))
            {
                puesto.Empresa_id = _AsinacionesService.GetClaimValue(token!, "IdEmpresa") ?? "Sistema";
            }
            puesto.Adicionado_por = _AsinacionesService.GetClaimValue(token!, "User") ?? "Sistema";
            puesto.Modificado_por = _AsinacionesService.GetClaimValue(token!, "User") ?? "Sistema";
            await _puestoRepository.PostPuestos(puesto);
            return new PuestoDto
            {
                Id = puesto.Id,
                Nombre = puesto.Nombre,
                Descripcion = puesto.Descripcion,
                Activo = puesto.Activo,
            };

        }

        public async Task<PuestoDto> PutPuestos(string id, Puesto puesto)
        {
            var puestoFound = await _puestoRepository.GetPuestosById(id);

            if (puestoFound == null)
            {
                throw new KeyNotFoundException("Puesto no encontrado.");
            }
            var token = _AsinacionesService.GetTokenFromHeader();
            puestoFound.ActualizarPropiedades(puesto);
            puestoFound.Activo = puesto.Activo;
            puesto.Empresa_id = _AsinacionesService.GetClaimValue(token!, "IdEmpresa") ?? "Sistema";
            puestoFound.Updated_at = _AsinacionesService.GetCurrentDateTime();
            puestoFound.Modificado_por = _AsinacionesService.GetClaimValue(token!, "User") ?? "Sistema";

            await _puestoRepository.PutPuestos(id, puestoFound);
            return new PuestoDto
            {
                Id = puestoFound.Id!,
                Nombre = puestoFound.Nombre,
                Activo = puestoFound.Activo,
                empresa = puestoFound.Empresa.nombre,
                Descripcion = puestoFound.Descripcion

            };
        }

    }
}
