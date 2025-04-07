using AutoGestion.interfaces.Rol;
using AutoGestion.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoGestion.repositories
{
    public class RolRepository : IRolRepository
    {
        private readonly DbContextAutoGestion _dbContextAutoGestion;
        public RolRepository(DbContextAutoGestion dbContextAutoGestion)
        {
            _dbContextAutoGestion = dbContextAutoGestion;
        }

        public async Task<IEnumerable<Role>> GetRoles()
        {
            return await _dbContextAutoGestion.Roles.ToListAsync();
        }
        public async Task<IEnumerable<Role>> GetRolesActivos()
        {
            return await _dbContextAutoGestion.Roles.Where(r => r.Activo == true).ToArrayAsync();
        }
        public async Task<Role> GetRolesById(string id)
        {
            return await _dbContextAutoGestion.Roles.Where(r => r.Id == id).FirstOrDefaultAsync();
        }
        public async Task<Role> PostRol(Role rol)
        {
            var rolCreate = await _dbContextAutoGestion.Roles.AddAsync(rol);
            await _dbContextAutoGestion.SaveChangesAsync();
            return rolCreate.Entity;
        }
        public async Task<Role> PutRol(Role rol, string id)
        {
            _dbContextAutoGestion.Entry(rol).State = EntityState.Modified;
            await _dbContextAutoGestion.SaveChangesAsync();
            return rol;
        }

        public async Task<bool> AssignPermissions(string roleId, List<string> ids)
        {

            var existingPermissions = _dbContextAutoGestion.RolePermiso.Where(rp => rp.RolId == roleId);
            _dbContextAutoGestion.RolePermiso.RemoveRange(existingPermissions);

            var permisos = await _dbContextAutoGestion.Permisos.Where(p => ids.Contains(p.Id)).ToListAsync();

            var rolePermisos = permisos.Select(p => new RolePermiso
            {
                RolId = roleId,
                PermisoId = p.Id
            }).ToList();

            _dbContextAutoGestion.RolePermiso.AddRange(rolePermisos);

            var result = await _dbContextAutoGestion.SaveChangesAsync();

            return result > 0;
        }


    }
}
