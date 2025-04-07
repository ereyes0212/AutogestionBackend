
using Microsoft.EntityFrameworkCore;

namespace AutoGestion.Models // Aquí faltaban llaves para definir el espacio de nombres correctamente
{
    public class DbContextAutoGestion : DbContext
    {
        public DbContextAutoGestion() { }

        public DbContextAutoGestion(DbContextOptions<DbContextAutoGestion> options) : base(options) { }

        public DbSet<Permiso> Permisos { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<RolePermiso> RolePermiso { get; set; }
        public DbSet<Empresa> Empresa { get; set; }
        public DbSet<Puesto> Puesto { get; set; }
        public DbSet<ConfiguracionAprobacion> ConfiguracionAprobacion { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RolePermiso>(entity =>
            {
                entity
                    .HasKey(rp => new { rp.RolId, rp.PermisoId });

                entity
                    .HasOne(rp => rp.Rol)
                    .WithMany(r => r.RolePermisos)
                    .HasForeignKey(rp => rp.RolId);

                entity
                    .HasOne(rp => rp.Permiso)
                    .WithMany(p => p.RolePermisos)
                    .HasForeignKey(rp => rp.PermisoId);

                entity
                    .ToTable("RolePermiso");

            });


            base.OnModelCreating(modelBuilder);
        }


    }
}
