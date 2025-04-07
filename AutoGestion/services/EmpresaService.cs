using AutoGestion.interfaces;
using AutoGestion.models.Empresa;
using AutoGestion.Models;
using AutoGestion.Utils;

namespace AutoGestion.services
{
    public class EmpresaService : IEmpresaService
    {
        private readonly IEmpresaRepository _empresaRepository;
        private readonly IAsignaciones _asinacionesService;
        public EmpresaService(IEmpresaRepository empresaRepository, IAsignaciones asignaciones)
        {
            _empresaRepository = empresaRepository;
            _asinacionesService = asignaciones;
        }

        public async Task<IEnumerable<EmpresaDto>> GetEmpresas()
        {
            var empresas = await _empresaRepository.GetEmpresas();
            if (empresas == null)
            {
                throw new KeyNotFoundException("Lista de empresas Vacia.");
            }
            var EmpresasDto = empresas.Select(e => new EmpresaDto
            {
                Id = e.Id!,
                Nombre = e.nombre,
                Activo = e.activo,
            });

            return EmpresasDto;
        }
        public async Task<IEnumerable<EmpresaDto>> GetEmpresasActivas()
        {
            var empresas = await _empresaRepository.GetEmpresasActivas();
            if (empresas == null)
            {
                throw new KeyNotFoundException("Lista de empresas Vacia.");
            }
            var EmpresasDto = empresas.Select(e => new EmpresaDto
            {
                Id = e.Id!,
                Nombre = e.nombre,
                Activo = e.activo,
            });

            return EmpresasDto;
        }
        public async Task<EmpresaDto>GetEmpresaById(string id)
        {
            var e = await _empresaRepository.GetEmpresaById(id);
            if (e == null)
            {
                throw new KeyNotFoundException("Empresa no encontrada.");
            }
            var EmpresasDto = new EmpresaDto
            {
                Id = e.Id!,
                Nombre = e.nombre,
                Activo = e.activo,
            };

            return EmpresasDto;
        }        
        public async Task<EmpresaDto>PostEmpresa(Empresa empresa)
        {
            var token = _asinacionesService.GetTokenFromHeader();
            empresa.Id = _asinacionesService.GenerateNewId();
            empresa.updated_at = _asinacionesService.GetCurrentDateTime();
            empresa.created_at = _asinacionesService.GetCurrentDateTime();
            empresa.activo = true;
            empresa.adicionado_por = _asinacionesService.GetClaimValue(token!, "User") ?? "Sistema";
            empresa.modificado_por = _asinacionesService.GetClaimValue(token!, "User") ?? "Sistema";
            await _empresaRepository.PostEmpresa(empresa);

            var empresaDto = new EmpresaDto
            {
                Nombre = empresa.nombre,
                Id = empresa.Id!,
                Activo = empresa.activo,
            };
            return empresaDto;
        }

        public async Task<EmpresaDto> PutEmpresa(string id, Empresa empresa)
        {
            var empresaFound = await _empresaRepository.GetEmpresaById(id);

            if (empresaFound == null)
            {
                throw new KeyNotFoundException("Empresa no encontrado.");
            }
            var token = _asinacionesService.GetTokenFromHeader();
            empresaFound.ActualizarPropiedades(empresa);
            empresaFound.activo = empresa.activo;
            empresaFound.updated_at = _asinacionesService.GetCurrentDateTime();
            empresaFound.modificado_por = _asinacionesService.GetClaimValue(token!, "User") ?? "Sistema";

            await _empresaRepository.PutEmpresa( empresaFound);
            return new EmpresaDto
            {
                Id = empresaFound.Id!,
                Nombre = empresaFound.nombre,
                Activo = empresaFound.activo,
            };
        }

    }
}
