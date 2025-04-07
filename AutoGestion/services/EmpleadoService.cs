using AutoGestion.interfaces;
using AutoGestion.interfaces.IEmpleado;
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
                edad = e.edad,
                genero = e.genero,
                puesto = e.Puesto.Nombre,
                jefe = e.Jefe != null ? e.Jefe.nombre : "Sin Empleado asignado",
                activo = e.activo,
                empresa = e.Empresa.nombre,
                usuario = e.Usuario?.usuario ?? ""
            });

            return empleadosDto;
        }
        public async Task<EmpleadoDTO?> GetEmpleadoById(string id)
        {
            var empleado = await _empleadoRepository.GetEmpleadoById(id);

            if (empleado == null)
            {
                throw new KeyNotFoundException("Empleado no encontrado.");
            }

            var empleadoDto = new EmpleadoDTO
            {
                id = empleado.id!,
                nombre = empleado.nombre,
                apellido = empleado.apellido,
                correo = empleado.correo,
                edad = empleado.edad,
                puesto = empleado.Puesto.Nombre,
                jefe = empleado.Jefe != null ? empleado.Jefe.nombre : "Sin Empleado asignado",
                genero = empleado.genero,
                activo = empleado.activo,
                empresa = empleado.Empresa.nombre,
                usuario = empleado.Usuario?.usuario ?? ""
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
                edad = e.edad,
                puesto = e.Puesto.Nombre,
                jefe = e.Jefe != null ? e.Jefe.nombre : "Sin Empleado asignado",
                genero = e.genero,
                empresa = e.Empresa.nombre,
                activo = e.activo,
                usuario = e.Usuario?.usuario ?? ""
            });
            return empleadosDto;
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
                edad = e.edad,
                genero = e.genero,
                puesto = e.Puesto.Nombre,
                jefe = e.Jefe != null ? e.Jefe.nombre : "Sin Empleado asignado",
                empresa = e.Empresa.nombre,
                activo = e.activo,
                usuario = e.Usuario?.usuario ?? ""
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
                edad = e.edad,
                puesto = e.Puesto.Nombre,
                jefe = e.Jefe != null ? e.Jefe.nombre : "Sin Empleado asignado",
                genero = e.genero,
                activo = e.activo,
                empresa = e.Empresa.nombre,
                usuario = e.Usuario?.usuario ?? ""
            });
            return empleadosDto;
        }

        public async Task<EmpleadoDTO> PostEmpleados(Empleado empleado)
        {
            var token = _AsinacionesService.GetTokenFromHeader();
            empleado.id = _AsinacionesService.GenerateNewId();
            empleado.created_at = _AsinacionesService.GetCurrentDateTime();
            empleado.updated_at = _AsinacionesService.GetCurrentDateTime();
            if (string.IsNullOrEmpty(empleado.empresa_id))
            {
                empleado.empresa_id = _AsinacionesService.GetClaimValue(token!, "IdEmpresa") ?? "Sistema";
            }
            empleado.adicionado_por = _AsinacionesService.GetClaimValue(token!, "User") ?? "Sistema";
            empleado.modificado_por = _AsinacionesService.GetClaimValue(token!, "User") ?? "Sistema";
            await _empleadoRepository.PostEmpleados(empleado);
            return new EmpleadoDTO
            {
                id = empleado.id,
                nombre = empleado.nombre,
                apellido = empleado.apellido,
                correo = empleado.correo,
                genero = empleado.genero,
                edad = empleado.edad,
                activo = empleado.activo,
                usuario = empleado.Usuario?.usuario ?? ""

            };

        }

        public async Task<EmpleadoDTO> PutEmpleados(string id, Empleado empleado)
        {
            var empleadoFound = await _empleadoRepository.GetEmpleadoById(id);

            if (empleadoFound == null)
            {
                throw new KeyNotFoundException("Empleado no encontrado.");
            }
            var token = _AsinacionesService.GetTokenFromHeader();
            empleadoFound.ActualizarPropiedades(empleado);
            empleadoFound.activo = empleado.activo;
            empleado.empresa_id = _AsinacionesService.GetClaimValue(token!, "IdEmpresa") ?? "Sistema";
            empleadoFound.updated_at = _AsinacionesService.GetCurrentDateTime();
            empleadoFound.modificado_por = _AsinacionesService.GetClaimValue(token!, "User") ?? "Sistema";

            await _empleadoRepository.PutEmpleados(id, empleadoFound);
            return new EmpleadoDTO
            {
                id = empleadoFound.id!,
                nombre = empleadoFound.nombre,
                apellido = empleadoFound.apellido,
                correo = empleadoFound.correo,
                genero = empleadoFound.genero,
                edad = empleadoFound.edad,
                activo = empleadoFound.activo,
                usuario = empleadoFound.Usuario?.usuario ?? ""

            };
        }

    }
}
