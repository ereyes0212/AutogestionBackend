
using Microsoft.EntityFrameworkCore;

namespace AutoGestion.Models
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
        public DbSet<Puesto> Puesto { get; set; }
        public DbSet<ConfiguracionAprobacion> ConfiguracionAprobacion { get; set; }
        public DbSet<TipoSolicitud> TipoSolicitud { get; set; }
        public DbSet<SolicitudVacacion> SolicitudVacacion { get; set; }
        public DbSet<SolicitudVacacionAprobacion> SolicitudVacacionAprobacion { get; set; }
        public DbSet<TipoDeduccion> TipoDeducciones { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de RolePermiso (relación Role - Permiso)
            modelBuilder.Entity<RolePermiso>(entity =>
            {
                entity.HasKey(rp => new { rp.RolId, rp.PermisoId });

                entity.HasOne(rp => rp.Rol)
                      .WithMany(r => r.RolePermisos)
                      .HasForeignKey(rp => rp.RolId);

                entity.HasOne(rp => rp.Permiso)
                      .WithMany(p => p.RolePermisos)
                      .HasForeignKey(rp => rp.PermisoId);
            });


            base.OnModelCreating(modelBuilder);
        }
    }
}
