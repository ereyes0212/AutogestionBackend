using System;
using System.Linq;
using AutoGestion.Models;
using Microsoft.EntityFrameworkCore;

namespace CarWashBackend.Data
{
    public class Seeder
    {
        private readonly DbContextAutoGestion _context;
        private readonly bool _resetData;

        public Seeder(DbContextAutoGestion context, bool resetData = false)
        {
            _context = context;
            _resetData = resetData;
        }

        public void Seed()
        {
            // Si se especifica resetData, se eliminan todos los registros
            if (_resetData)
            {
                _context.Permisos.RemoveRange(_context.Permisos);
                _context.Roles.RemoveRange(_context.Roles);
                _context.Usuarios.RemoveRange(_context.Usuarios);
                _context.Empleados.RemoveRange(_context.Empleados);
                _context.Puesto.RemoveRange(_context.Puesto);
                _context.SaveChanges();
            }








            // Sembrar Permisos (solo si no existen)
            if (!_context.Permisos.Any())
            {
                _context.Permisos.AddRange(

                    //empleados
                    new Permiso { Id = Guid.NewGuid().ToString(), Nombre = "ver_empleados", Descripcion = "Permiso para ver los empleados", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, Activo = true },
                    new Permiso { Id = Guid.NewGuid().ToString(), Nombre = "crear_empleados", Descripcion = "Permiso para crear los empleados", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, Activo = true },
                    new Permiso { Id = Guid.NewGuid().ToString(), Nombre = "editar_empleado", Descripcion = "Permiso para editar los empleados", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, Activo = true },


                    //Permisos
                    new Permiso { Id = Guid.NewGuid().ToString(), Nombre = "ver_permisos", Descripcion = "Permiso para ver los permisos", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, Activo = true },

                    //Roles
                    new Permiso { Id = Guid.NewGuid().ToString(), Nombre = "ver_roles", Descripcion = "Permiso para ver roles", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, Activo = true },
                    new Permiso { Id = Guid.NewGuid().ToString(), Nombre = "crear_roles", Descripcion = "Permiso para crear roles", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, Activo = true },
                    new Permiso { Id = Guid.NewGuid().ToString(), Nombre = "editar_roles", Descripcion = "Permiso para editar roles", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, Activo = true },

                    //Usuarios
                    new Permiso { Id = Guid.NewGuid().ToString(), Nombre = "ver_usuarios", Descripcion = "Permiso para ver usuarios", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, Activo = true },
                    new Permiso { Id = Guid.NewGuid().ToString(), Nombre = "crear_usuario", Descripcion = "Permiso para crear usuarios", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, Activo = true },
                    new Permiso { Id = Guid.NewGuid().ToString(), Nombre = "editar_usuario", Descripcion = "Permiso para editar usuarios", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, Activo = true },


                    //Tipo Deducciones
                    new Permiso { Id = Guid.NewGuid().ToString(), Nombre = "ver_tipodeducciones", Descripcion = "Permiso para ver tipo deducciones", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, Activo = true },
                    new Permiso { Id = Guid.NewGuid().ToString(), Nombre = "crear_tipodeducciones", Descripcion = "Permiso para crear tipo deducciones", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, Activo = true },
                    new Permiso { Id = Guid.NewGuid().ToString(), Nombre = "editar_tipodeducciones", Descripcion = "Permiso para editar tipo deducciones", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, Activo = true },

                    //Puestos
                    new Permiso { Id = Guid.NewGuid().ToString(), Nombre = "ver_puestos", Descripcion = "Permiso para ver puestos", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, Activo = true },
                    new Permiso { Id = Guid.NewGuid().ToString(), Nombre = "crear_puestos", Descripcion = "Permiso para crear puestos", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, Activo = true },
                    new Permiso { Id = Guid.NewGuid().ToString(), Nombre = "editar_puestos", Descripcion = "Permiso para editar puestos", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, Activo = true },
                    
                    //Contabilidad
                    new Permiso { Id = Guid.NewGuid().ToString(), Nombre = "ver_contabilidad", Descripcion = "Permiso para ver modulo de contabilidad", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, Activo = true },
                    new Permiso { Id = Guid.NewGuid().ToString(), Nombre = "ver_generar_planilla", Descripcion = "Permiso para ver la pantalla de generar planilla", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, Activo = true },
                    
                    //voucher
                    new Permiso { Id = Guid.NewGuid().ToString(), Nombre = "ver_voucher_pago", Descripcion = "Permiso para ver los voucher de pago", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, Activo = true },
                    new Permiso { Id = Guid.NewGuid().ToString(), Nombre = "ver_detalle_voucher_pago", Descripcion = "Permiso para ver la pantalla de detalle de voucher", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, Activo = true },
                    
                    //voucher
                    new Permiso { Id = Guid.NewGuid().ToString(), Nombre = "ver_profile", Descripcion = "Permiso para ver el perfil", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, Activo = true }

                );
                _context.SaveChanges();
            }

            // Crear el rol de Administrador si no existe
            var adminRole = _context.Roles.FirstOrDefault(r => r.Nombre == "Administrador");
            if (adminRole == null)
            {
                adminRole = new Role
                {
                    Id = Guid.NewGuid().ToString(),
                    Nombre = "Administrador",
                    Descripcion = "Rol con acceso total al sistema",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    Activo = true
                };
                _context.Roles.Add(adminRole);
                _context.SaveChanges();
            }

            // Asignar todos los permisos al rol de administrador
            var allPermisos = _context.Permisos.ToList();
            var rolPermisosActuales = _context.RolePermiso
                .Where(rp => rp.RolId == adminRole.Id)
                .Select(rp => rp.PermisoId)
                .ToList();

            var nuevosPermisos = allPermisos
                .Where(p => !rolPermisosActuales.Contains(p.Id))
                .Select(p => new RolePermiso { RolId = adminRole.Id, PermisoId = p.Id })
                .ToList();

            if (nuevosPermisos.Any())
            {
                _context.AddRange(nuevosPermisos);
                _context.SaveChanges();
            }

            var puesto = _context.Puesto.FirstOrDefault(e => e.Nombre == "Gerente Recursos Humanos");

            if (puesto == null)
            {
                puesto = new Puesto
                {
                    Id = Guid.NewGuid().ToString(),
                    Nombre = "Gerente Recursos Humanos",
                    Activo = true,
                    Descripcion = "Gerente de recursos humanos de diario tiempo",
                    Adicionado_por = "Sistema",
                    Modificado_por = "Sistema",
                    Created_at = DateTime.UtcNow,
                    Updated_at = DateTime.UtcNow
                };
                _context.Puesto.Add(puesto);
                _context.SaveChanges();
            }

            // Crear Empleado si no existe
            var empleado = _context.Empleados.FirstOrDefault(e => e.correo == "marta.rapalo@tiempo.hn");
            if (empleado == null)
            {
                empleado = new Empleado
                {
                    id = Guid.NewGuid().ToString(),
                    nombre = "Marta",
                    apellido = "Rapalo",
                    puesto_id = puesto.Id,
                    correo = "marta.rapalo@tiempo.hn",
                    FechaNacimiento = new DateTime(1999, 12, 2),
                    Vacaciones= 10,
                    genero = "Masculino",
                    activo = true,
                    adicionado_por = "Sistema",
                    modificado_por = "Sistema",
                    created_at = DateTime.UtcNow,
                    updated_at = DateTime.UtcNow
                };

                _context.Empleados.Add(empleado);
                _context.SaveChanges();
            }

            // Crear Usuario si no existe
            if (!_context.Usuarios.Any(u => u.usuario == "marta.rapalo"))
            {
                var usuario = new Usuario
                {
                    id = Guid.NewGuid().ToString(),
                    usuario = "marta.rapalo",
                    contrasena = BCrypt.Net.BCrypt.HashPassword("marta.rapalo"),
                    empleado_id = empleado.id,
                    rol_id = adminRole.Id,
                    activo = true,
                    DebeCambiarPassword= true,
                    adicionado_por= "Sistema",
                    modificado_por="Sistema",
                    created_at = DateTime.UtcNow,
                    updated_at = DateTime.UtcNow
                };
                _context.Usuarios.Add(usuario);
                _context.SaveChanges();
            }
        }


    }
}
