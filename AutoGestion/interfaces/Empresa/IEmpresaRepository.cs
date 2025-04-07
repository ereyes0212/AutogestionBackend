

    public interface IEmpresaRepository
    {
        Task<IEnumerable<Empresa>> GetEmpresas();
        Task<Empresa> GetEmpresaById(string id);
        Task<IEnumerable<Empresa>> GetEmpresasActivas();
        Task<Empresa> PostEmpresa(Empresa empresa);
        Task<Empresa> PutEmpresa(Empresa empresa);
    }

