using AutoGestion.interfaces;
using AutoGestion.interfaces.IEmpleado;
using AutoGestion.models.Empresa;
using AutoGestion.Models;
using AutoGestion.Utils;

namespace AutoGestion.services
{
    public class EmpleadoService : IEmpleadoService
    {
        private readonly IEmpleadoRepository _empleadoRepository;
        private readonly IAsignaciones _AsinacionesService;

        public EmpleadoService(IEmpleadoRepository empleadoRepository, IAsignaciones asignacionesService)
        {
            _empleadoRepository = empleadoRepository;
            _AsinacionesService = asignacionesService;
        }

        public async Task<IEnumerable<EmpleadoDTO>> GetEmpleados()
        {

            var empleados = await _empleadoRepository.GetEmpleados();
            if (empleados == null)
            {
                throw new KeyNotFoundException("Lista de empleados Vacia.");
            }
            var empleadosDto = empleados.Select(e => new EmpleadoDTO
            {
                id = e.id!,
                nombre = e.nombre,
                apellido = e.apellido,
                correo = e.correo,
                Vacaciones = e.Vacaciones,
                FechaNacimiento = e.FechaNacimiento,
                genero = e.genero,
                puesto = e.Puesto != null ? e.Puesto.Nombre : "",
                puesto_id = e.puesto_id,
                jefe_id = e.jefe_id,
                usuario_id = e.Usuario != null ? e.Usuario.usuario : "Sin Usuario asignado",
                jefe = e.Jefe != null ? e.Jefe.nombre : "Sin jefe asignado",
                activo = e.activo,
                usuario = e.Usuario?.usuario ?? "",
                // Mapeo de la relación muchos a muchos: EmpleadoEmpresas a EmpresaSimpleDto
                Empresas = e.EmpleadoEmpresas.Select(ee => new EmpresaSimpleDto
                {
                    id = ee.Empresa.Id,
                    nombre = ee.Empresa.nombre
                }).ToList()
            });

            return empleadosDto;
        }
        public async Task<IEnumerable<EmpleadoDTO>> GetEmpleadosDisponibles()
        {

            var empleados = await _empleadoRepository.GetEmpleadosDisponibles();
            if (empleados == null)
            {
                throw new KeyNotFoundException("Lista de empleados Vacia.");
            }
            var empleadosDto = empleados.Select(e => new EmpleadoDTO
            {
                id = e.id!,
                nombre = e.nombre,
                apellido = e.apellido,
                correo = e.correo,
                FechaNacimiento = e.FechaNacimiento,
                genero = e.genero,
                Vacaciones = e.Vacaciones,
                puesto = e.Puesto != null ? e.Puesto.Nombre : "",
                puesto_id = e.puesto_id,
                jefe_id = e.jefe_id,
                usuario_id = e.Usuario != null ? e.Usuario.usuario : "Sin Usuario asignado",
                jefe = e.Jefe != null ? e.Jefe.nombre : "Sin jefe asignado",
                activo = e.activo,
                usuario = e.Usuario?.usuario ?? "",
                // Mapeo de la relación muchos a muchos: EmpleadoEmpresas a EmpresaSimpleDto
                Empresas = e.EmpleadoEmpresas.Select(ee => new EmpresaSimpleDto
                {
                    id = ee.Empresa.Id,
                    nombre = ee.Empresa.nombre
                }).ToList()
            });

            return empleadosDto;
        }
        public async Task<EmpleadoDTO?> GetEmpleadoById(string id)
        {
            var e = await _empleadoRepository.GetEmpleadoById(id);

            if (e == null)
            {
                throw new KeyNotFoundException("Empleado no encontrado.");
            }

            var empleadoDto = new EmpleadoDTO
            {
                id = e.id!,
                nombre = e.nombre,
                apellido = e.apellido,
                correo = e.correo,
                Vacaciones = e.Vacaciones,
                FechaNacimiento = e.FechaNacimiento,
                genero = e.genero,
                puesto = e.Puesto != null ? e.Puesto.Nombre : "",
                puesto_id = e.puesto_id,
                jefe_id = e.jefe_id,
                usuario_id = e.Usuario != null ? e.Usuario.usuario : "Sin Usuario asignado",
                jefe = e.Jefe != null ? e.Jefe.nombre : "Sin jefe asignado",
                activo = e.activo,
                usuario = e.Usuario?.usuario ?? "",
                // Mapeo de la relación muchos a muchos: EmpleadoEmpresas a EmpresaSimpleDto
                Empresas = e.EmpleadoEmpresas.Select(ee => new EmpresaSimpleDto
                {
                    id = ee.Empresa.Id,
                    nombre = ee.Empresa.nombre
                }).ToList()
            };

            return empleadoDto;
        }

        public async Task<IEnumerable<EmpleadoDTO?>> GetEmpleadosByEmpresaId()
        {
            var token = _AsinacionesService.GetTokenFromHeader();
            var empresaId = _AsinacionesService.GetClaimValue(token!, "IdEmpresa");
            var empleados = await _empleadoRepository.GetEmpleadoByEmpresaId(empresaId!);

            if (empleados == null)
            {
                throw new KeyNotFoundException("Empleados no encontrado.");
            }

            var empleadosDto = empleados.Select(e => new EmpleadoDTO
            {
                id = e.id!,
                nombre = e.nombre,
                apellido = e.apellido,
                correo = e.correo,
                Vacaciones = e.Vacaciones,
                FechaNacimiento = e.FechaNacimiento,
                genero = e.genero,
                puesto = e.Puesto != null ? e.Puesto.Nombre : "",
                puesto_id = e.puesto_id,
                jefe_id = e.jefe_id,
                usuario_id = e.Usuario != null ? e.Usuario.usuario : "Sin Usuario asignado",
                jefe = e.Jefe != null ? e.Jefe.nombre : "Sin jefe asignado",
                activo = e.activo,
                usuario = e.Usuario?.usuario ?? "",
                Empresas = e.EmpleadoEmpresas.Select(ee => new EmpresaSimpleDto
                {
                    id = ee.Empresa.Id,
                    nombre = ee.Empresa.nombre
                }).ToList()
            });
            return empleadosDto;
        }

        public async Task<EmpleadoDTO?> GetProfile()
        {
            var token = _AsinacionesService.GetTokenFromHeader();
            var idEmpleado = _AsinacionesService.GetClaimValue(token!, "IdEmpleado");

            var e = await _empleadoRepository.GetProfile(idEmpleado!);

            if (e == null)
            {
                throw new KeyNotFoundException("Empleado no encontrado.");
            }

            var empleadoDto = new EmpleadoDTO
            {
                id = e.id!,
                nombre = e.nombre,
                apellido = e.apellido,
                correo = e.correo,
                FechaNacimiento = e.FechaNacimiento,
                genero = e.genero,
                puesto = e.Puesto != null ? e.Puesto.Nombre : "",
                puesto_id = e.puesto_id,
                Vacaciones = e.Vacaciones,
                jefe_id = e.jefe_id,
                usuario_id = e.Usuario != null ? e.Usuario.usuario : "Sin Usuario asignado",
                jefe = e.Jefe != null ? e.Jefe.nombre : "Sin jefe asignado",
                activo = e.activo,
                usuario = e.Usuario?.usuario ?? "",
                Empresas = e.EmpleadoEmpresas.Select(ee => new EmpresaSimpleDto
                {
                    id = ee.Empresa.Id,
                    nombre = ee.Empresa.nombre
                }).ToList()
            };

            return empleadoDto;
        }

        public async Task<IEnumerable<EmpleadoDTO?>> GetEmpleadosActivosByEmpresaId()
        {
            var token = _AsinacionesService.GetTokenFromHeader();
            var empresaId = _AsinacionesService.GetClaimValue(token!, "IdEmpresa");
            var empleados = await _empleadoRepository.GetEmpleadosActivosByEmpresaId(empresaId!);

            if (empleados == null)
            {
                throw new KeyNotFoundException("Empleados no encontrado.");
            }

            var empleadosDto = empleados.Select(e => new EmpleadoDTO
            {
                id = e.id!,
                nombre = e.nombre,
                apellido = e.apellido,
                correo = e.correo,
                Vacaciones = e.Vacaciones,
                FechaNacimiento = e.FechaNacimiento,
                genero = e.genero,
                puesto = e.Puesto != null ? e.Puesto.Nombre : "",
                puesto_id = e.puesto_id,
                jefe_id = e.jefe_id,
                usuario_id = e.Usuario != null ? e.Usuario.usuario : "Sin Usuario asignado",
                jefe = e.Jefe != null ? e.Jefe.nombre : "Sin jefe asignado",
                activo = e.activo,
                usuario = e.Usuario?.usuario ?? "",
                // Mapeo de la relación muchos a muchos: EmpleadoEmpresas a EmpresaSimpleDto
                Empresas = e.EmpleadoEmpresas.Select(ee => new EmpresaSimpleDto
                {
                    id = ee.Empresa.Id,
                    nombre = ee.Empresa.nombre
                }).ToList()
            });
            return empleadosDto;
        }


        public async Task<IEnumerable<EmpleadoDTO>> GetEmpleadosActivos()
        {
            var empleados = await _empleadoRepository.GetEmpleadosActivos();
            if (empleados == null)
            {
                throw new KeyNotFoundException("Lista de empleados Vacia.");
            }
            var empleadosDto = empleados.Select(e => new EmpleadoDTO
            {
                id = e.id!,
                nombre = e.nombre,
                apellido = e.apellido,
                correo = e.correo,
                FechaNacimiento = e.FechaNacimiento,
                genero = e.genero,
                puesto = e.Puesto != null ? e.Puesto.Nombre : "",
                puesto_id = e.puesto_id,
                Vacaciones = e.Vacaciones,
                jefe_id = e.jefe_id,
                usuario_id = e.Usuario != null ? e.Usuario.usuario : "Sin Usuario asignado",
                jefe = e.Jefe != null ? e.Jefe.nombre : "Sin jefe asignado",
                activo = e.activo,
                usuario = e.Usuario?.usuario ?? "",
                // Mapeo de la relación muchos a muchos: EmpleadoEmpresas a EmpresaSimpleDto
                Empresas = e.EmpleadoEmpresas.Select(ee => new EmpresaSimpleDto
                {
                    id = ee.Empresa.Id,
                    nombre = ee.Empresa.nombre
                }).ToList()
            });
            return empleadosDto;
        }

        public async Task<EmpleadoDTO> PostEmpleados(EmpleadoCreateDto empleadoCreateDto)
        {
            var token = _AsinacionesService.GetTokenFromHeader();

            // Crear el empleado
            var empleado = new Empleado
            {
                id = _AsinacionesService.GenerateNewId(),
                nombre = empleadoCreateDto.Nombre,
                apellido = empleadoCreateDto.Apellido,
                correo = empleadoCreateDto.Correo,
                Vacaciones = 10,
                FechaNacimiento = empleadoCreateDto.FechaNacimiento,
                genero = empleadoCreateDto.Genero,
                activo = empleadoCreateDto.Activo,
                puesto_id = empleadoCreateDto.puesto_id,
                jefe_id = empleadoCreateDto.jefe_id,
                adicionado_por = _AsinacionesService.GetClaimValue(token!, "User") ?? "Sistema",
                modificado_por = _AsinacionesService.GetClaimValue(token!, "User") ?? "Sistema",
                created_at = _AsinacionesService.GetCurrentDateTime(),
                updated_at = _AsinacionesService.GetCurrentDateTime(),
            };

            // Si jefe_id viene vacío, lo transformamos a null
            if (string.IsNullOrWhiteSpace(empleado.jefe_id))
            {
                empleado.jefe_id = null;
            }

            // Guardar el empleado en la base de datos
            await _empleadoRepository.PostEmpleados(empleado);

            // Asignar las empresas al empleado si existen los ids
            if (empleadoCreateDto.EmpresaIds != null && empleadoCreateDto.EmpresaIds.Any())
            {
                // Asignar empresas usando el repositorio
                await _empleadoRepository.AsignarEmpresaEmpleado(empleado.id, empleadoCreateDto.EmpresaIds);
            }

            // Devolver el DTO con la información básica del empleado
            return new EmpleadoDTO
            {
                id = empleado.id,
                nombre = empleado.nombre,
                apellido = empleado.apellido,
                correo = empleado.correo,
                Vacaciones = empleado.Vacaciones,
                genero = empleado.genero,
                FechaNacimiento = empleado.FechaNacimiento,
                activo = empleado.activo,
                usuario = empleado.Usuario?.usuario ?? ""
            };
        }


        public async Task<EmpleadoDTO> PutEmpleados(string id, EmpleadoCreateDto empleadoDto)
        {
            var empleadoFound = await _empleadoRepository.GetEmpleadoById(id);
            if (empleadoFound == null)
            {
                throw new KeyNotFoundException("Empleado no encontrado.");
            }

            if (string.IsNullOrWhiteSpace(empleadoDto.jefe_id))
            {
                empleadoDto.jefe_id = null;
            }

            var token = _AsinacionesService.GetTokenFromHeader();

            // Actualizar propiFechaNacimientoes
            empleadoFound.nombre = empleadoDto.Nombre;
            empleadoFound.apellido = empleadoDto.Apellido;
            empleadoFound.correo = empleadoDto.Correo;
            empleadoFound.FechaNacimiento = empleadoDto.FechaNacimiento;
            empleadoFound.genero = empleadoDto.Genero;
            empleadoFound.activo = empleadoDto.Activo;
            empleadoFound.Vacaciones = empleadoDto.Vacaciones;
            empleadoFound.puesto_id = empleadoDto.puesto_id;
            empleadoFound.jefe_id = empleadoDto.jefe_id;
            empleadoFound.updated_at = _AsinacionesService.GetCurrentDateTime();
            empleadoFound.modificado_por = _AsinacionesService.GetClaimValue(token!, "User") ?? "Sistema";

            await _empleadoRepository.PutEmpleados(id, empleadoFound);

            // Actualizar empresas asignadas
            if (empleadoDto.EmpresaIds != null && empleadoDto.EmpresaIds.Any())
            {
                await _empleadoRepository.AsignarEmpresaEmpleado(id, empleadoDto.EmpresaIds);
            }

            // Obtener nuevamente con relaciones
            var empleadoUpdated = await _empleadoRepository.GetEmpleadoById(id);

            return new EmpleadoDTO
            {
                id = empleadoUpdated.id!,
                nombre = empleadoUpdated.nombre,
                apellido = empleadoUpdated.apellido,
                correo = empleadoUpdated.correo,
                genero = empleadoUpdated.genero,
                FechaNacimiento = empleadoUpdated.FechaNacimiento,
                activo = empleadoUpdated.activo,
                Vacaciones = empleadoUpdated.Vacaciones,
                puesto = empleadoUpdated.Puesto?.Nombre ?? "",
                puesto_id = empleadoUpdated.puesto_id,
                jefe_id = empleadoUpdated.jefe_id,
                jefe = empleadoUpdated.Jefe?.nombre ?? "Sin jefe asignado",
                usuario_id = empleadoUpdated.Usuario?.usuario ?? "Sin Usuario asignado",
                usuario = empleadoUpdated.Usuario?.usuario ?? "",
                Empresas = empleadoUpdated.EmpleadoEmpresas
                    .Select(ee => new EmpresaSimpleDto
                    {
                        id = ee.Empresa.Id,
                        nombre = ee.Empresa.nombre
                    }).ToList()
            };
        }




    }
}
