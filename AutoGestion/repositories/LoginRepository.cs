using AutoGestion.interfaces.ILogin;
using AutoGestion.Models;
using AutoGestion.services;
using Microsoft.EntityFrameworkCore;

namespace AutoGestion.repositories
{
    public class LoginRepository : ILoginRepository
    {
        private readonly DbContextAutoGestion _dbContextAutoGestion;
        public LoginRepository(DbContextAutoGestion dbContextAutoGestion)
        {
            _dbContextAutoGestion = dbContextAutoGestion;
        }

        public async Task<Usuario> GetUserByUsername(string username)
        {
            return await _dbContextAutoGestion.Usuarios
                .Include(u => u.Role)
                .Include(u => u.Empleado)
                    .ThenInclude(e => e.EmpleadoEmpresas)
                        .ThenInclude(ee => ee.Empresa)
                .FirstOrDefaultAsync(u => u.usuario == username);
        }

        public async Task<List<string>> GetUserPermissions(string userId)
        {
            // Obtener el usuario y su rol
            var user = await _dbContextAutoGestion.Usuarios
                .Where(u => u.id == userId)
                .Include(u => u.Role)  // Incluir el rol del usuario
                .FirstOrDefaultAsync();

            if (user == null || user.Role == null)
            {
                throw new InvalidOperationException("Usuario o rol no encontrado");
            }

            // Obtener los permisos del rol del usuario
            var userPermissions = await _dbContextAutoGestion.RolePermiso
                .Where(rp => rp.RolId == user.Role.Id)  // Filtrar por el rol del usuario
                .Select(rp => rp.Permiso.Nombre)  // Seleccionar los permisos
                .Distinct()  // Evitar duplicados
                .ToListAsync();

            return userPermissions;
        }




    }
}
