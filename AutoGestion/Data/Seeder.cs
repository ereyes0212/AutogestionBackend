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
                _context.Empresa.RemoveRange(_context.Empresa);
                _context.Puesto.RemoveRange(_context.Puesto);
                _context.SaveChanges();
            }

            // Verificar si ya existe la empresa "Diario Tiempo"
            var empresaExistente = _context.Empresa.FirstOrDefault(e => e.nombre == "Diario Tiempo");
            if (empresaExistente != null)
            {
                // Si la empresa ya existe, no se ejecuta el resto del seeding
                return;
            }

            // Crear Empresa
            var empresa = new Empresa
            {
                Id = Guid.NewGuid().ToString(),
                nombre = "Diario Tiempo",
                activo = true,
                adicionado_por = "Sistema",
                modificado_por = "Sistema",
                created_at = DateTime.UtcNow,
                updated_at = DateTime.UtcNow
            };
            _context.Empresa.Add(empresa);
            _context.SaveChanges();



            // Sembrar Permisos (solo si no existen)
            if (!_context.Permisos.Any())
            {
                _context.Permisos.AddRange(
                    new Permiso
                    {
                        Id = Guid.NewGuid().ToString(),
                        Nombre = "ver_empleado",
                        Descripcion = "Permiso para ver empleados",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        Activo = true
                    },
                    new Permiso
                    {
                        Id = Guid.NewGuid().ToString(),
                        Nombre = "crear_empleado",
                        Descripcion = "Permiso para crear empleados",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        Activo = true
                    },
                    new Permiso
                    {
                        Id = Guid.NewGuid().ToString(),
                        Nombre = "editar_empleado",
                        Descripcion = "Permiso para editar empleados",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        Activo = true
                    }
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

            if(puesto == null)
            {
                puesto = new Puesto
                {
                    Id = Guid.NewGuid().ToString(),
                    Nombre = "Gerente Recursos Humanos",
                    Activo = true,
                    Descripcion = "Gerente de recursos humanos de diario tiempo",
                    Empresa_id = empresa.Id,
                    Adicionado_por = "Sistema",
                    Modificado_por = "Sistema",
                    Created_at = DateTime.UtcNow,
                    Updated_at = DateTime.UtcNow
                };
                _context.Puesto.Add(puesto);
                _context.SaveChanges();
            }
            // Crear Empleado si no existe
            var empleado = _context.Empleados.FirstOrDefault(e => e.correo == "erick.reyes@tiempo.hn");
            if (empleado == null)
            {
                empleado = new Empleado
                {
                    id = Guid.NewGuid().ToString(),
                    nombre = "Erick Jose",
                    apellido = "Reyes Pineda",
                    puesto_id = puesto.Id,
                    correo = "erick.reyes@tiempo.hn",
                    empresa_id = empresa.Id,
                    edad = 25,
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
            if (!_context.Usuarios.Any(u => u.usuario == "erick.reyes"))
            {
                var usuario = new Usuario
                {
                    id = Guid.NewGuid().ToString(),
                    usuario = "erick.reyes",
                    contrasena = BCrypt.Net.BCrypt.HashPassword("erick.reyes"),
                    empresa_id = empresa.Id,
                    empleado_id = empleado.id,
                    role_id = adminRole.Id,
                    activo = true,
                    created_at = DateTime.UtcNow,
                    updated_at = DateTime.UtcNow
                };
                _context.Usuarios.Add(usuario);
                _context.SaveChanges();
            }
        }
    }
}
