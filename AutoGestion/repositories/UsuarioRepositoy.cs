using AutoGestion.interfaces.IUsuario;
using AutoGestion.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoGestion.repositories
{
    public class UsuarioRepositoy: IUsuarioRepository
    {
        private readonly DbContextAutoGestion _dbContextAutoGestion;

        public UsuarioRepositoy(DbContextAutoGestion dbContextAutoGestion)
        {
            _dbContextAutoGestion = dbContextAutoGestion;
        }

        public async Task<IEnumerable<Usuario>> GetUsuarios()
        {
            return await _dbContextAutoGestion.Usuarios.Include("Empleado").Include("Role").ToListAsync();
        }

        public async Task<IEnumerable<Usuario>> GetUsuariosActivos()
        {
            return await _dbContextAutoGestion.Usuarios.Include("Empleado").Include("Role").Where(u => u.activo == true).ToListAsync();
        }

        public async Task<Usuario> GetUsuarioById(string id)
        {
            var usuario = await _dbContextAutoGestion.Usuarios.Include("Empleado").Include("Role").Where(u => u.id == id).FirstOrDefaultAsync(); 
            return usuario;
        }

        public async Task<Usuario>PostUsuario(Usuario usuario)
        {
            var usuarioCreate = await _dbContextAutoGestion.Usuarios.AddAsync(usuario);
            await _dbContextAutoGestion.SaveChangesAsync();

            var usuarioWithIncludes = await _dbContextAutoGestion.Usuarios.Include(u => u.Role)
                .Include(u => u.Empleado).FirstOrDefaultAsync(u => u.id == usuario.id);
            return usuarioWithIncludes!;
        }

        public async Task<Usuario> PutUsuario(Usuario usuario)
        {
            _dbContextAutoGestion.Entry(usuario).State = EntityState.Modified;
            await _dbContextAutoGestion.SaveChangesAsync();

            return await _dbContextAutoGestion.Usuarios.Include(u => u.Role).Include(u => u.Empleado).FirstOrDefaultAsync(u => u.id == usuario.id);
        }





    }
}
