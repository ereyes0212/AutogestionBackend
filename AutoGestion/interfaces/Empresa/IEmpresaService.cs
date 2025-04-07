
using AutoGestion.models.Empresa;



    public interface IEmpresaService
    {
        Task<IEnumerable<EmpresaDto>> GetEmpresas();
        Task<IEnumerable<EmpresaDto>> GetEmpresasActivas();
        Task<EmpresaDto> GetEmpresaById(string id);
        Task<EmpresaDto> PostEmpresa(Empresa empresa);
        Task<EmpresaDto> PutEmpresa(string id, Empresa empresa);
    }

