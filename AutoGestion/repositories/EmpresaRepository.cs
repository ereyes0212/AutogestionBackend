using AutoGestion.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoGestion.repositories
{
    public class EmpresaRepository : IEmpresaRepository
    {
        private readonly DbContextAutoGestion _dbContextAutoGestion;

        public EmpresaRepository (DbContextAutoGestion repositoryAutoGestion)
        {
            _dbContextAutoGestion = repositoryAutoGestion;
        }

        public async Task<IEnumerable<Empresa>> GetEmpresas()
        {
            return await _dbContextAutoGestion.Empresa.ToListAsync();
        }

        public async Task<IEnumerable<Empresa>> GetEmpresasActivas()
        {
            return await _dbContextAutoGestion.Empresa.Where(e => e.activo == true).ToListAsync();
        }

        public async Task<Empresa> GetEmpresaById(string id)
        {
            return await _dbContextAutoGestion.Empresa.Where(e=> e.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Empresa>PostEmpresa(Empresa empresa)
        {
            var result = await _dbContextAutoGestion.Empresa.AddAsync(empresa);
            await _dbContextAutoGestion.SaveChangesAsync();
            return result.Entity;
        }
        public async Task<Empresa> PutEmpresa(Empresa empresa)
        {

            _dbContextAutoGestion.Entry(empresa).State = EntityState.Modified;

            await _dbContextAutoGestion.SaveChangesAsync();

            return empresa;
        }


    }
}
