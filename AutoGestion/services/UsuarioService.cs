using AutoGestion.interfaces;
using AutoGestion.interfaces.IUsuario;
using AutoGestion.models.Usuario;
using AutoGestion.Models;
using AutoGestion.Utils;

namespace AutoGestion.services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repository;
        private readonly IAsignaciones _asignaciones;
        public UsuarioService(IUsuarioRepository usuarioService, IAsignaciones asignaciones)
        {
            _asignaciones = asignaciones;
            _repository = usuarioService;
        }

        public async Task<IEnumerable<UsuarioDto>> GetUsuarios()
        {
            var usuarios = await _repository.GetUsuarios();
            var usuariosDto = usuarios.Select(u => new UsuarioDto
            {
                id = u.id,
                usuario = u.usuario,
                contrasena = u.contrasena,
                empleado = u.Empleado!.nombre,
                rol = u.Role!.Nombre,
                rolId = u.role_id,
                empleadoId = u.role_id
            });
            return usuariosDto;
        }
        public async Task<IEnumerable<UsuarioDto>> GetUsuariosActivos()
        {
            var usuarios = await _repository.GetUsuariosActivos();
            var usuariosDto = usuarios.Select(u => new UsuarioDto
            {
                id = u.id,
                usuario = u.usuario,
                contrasena = u.contrasena,
                empleado = u.Empleado.nombre,
                rol = u.Role.Nombre,
                rolId = u.role_id,
                empleadoId = u.role_id
            });
            return usuariosDto;
        }


        public async Task<UsuarioDto> GetUsuarioById(string id)
        {
            var u = await _repository.GetUsuarioById(id);
            if (u == null)
            {
                throw new KeyNotFoundException("Usuario no encontrado.");
            }
            var usuarioDto = new UsuarioDto
            {
                id = u.id,
                usuario = u.usuario,
                contrasena = u.contrasena,
                empleado = u.Empleado!.nombre,
                rol = u.Role!.Nombre,
                rolId = u.role_id,
                empleadoId = u.role_id
            };
            return usuarioDto;
        }


        public async  Task<UsuarioDto> PostUsuario(Usuario usuario)
        {
            var token = _asignaciones.GetTokenFromHeader() ?? "";
            usuario.id = _asignaciones.GenerateNewId();
            usuario.created_at = _asignaciones.GetCurrentDateTime();
            usuario.updated_at = _asignaciones.GetCurrentDateTime();
            if (string.IsNullOrEmpty(usuario.empresa_id))
            {
                usuario.empresa_id = _asignaciones.GetClaimValue(token!, "IdEmpresa") ?? "Sistema";
            }
            usuario.contrasena = _asignaciones.EncriptPassword(usuario.contrasena);
            usuario.adicionado_por = _asignaciones.GetClaimValue(token, "User") ?? "Sistema";
            usuario.modificado_por = _asignaciones.GetClaimValue(token, "User") ?? "Sistema";
            await _repository.PostUsuario(usuario);
            var usuarioCreate = await _repository.GetUsuarioById(usuario.id);
            return new UsuarioDto
            {
                id = usuarioCreate.id,
                usuario = usuarioCreate.usuario,
                contrasena = usuarioCreate.contrasena,
                empleado = usuarioCreate.Empleado?.nombre ?? "",
                rol = usuarioCreate.Role?.Nombre ?? "",
                rolId = usuarioCreate.role_id,
                empleadoId = usuarioCreate.role_id
            };
        }
        public async Task<UsuarioDto> PutUsuario(string id, Usuario usuario)
        {
            var token = _asignaciones.GetTokenFromHeader();
            var usuarioFound = await _repository.GetUsuarioById(id);
            if (usuarioFound == null)
            {
                throw new KeyNotFoundException("Usuario no encontrado.");
            }

            usuarioFound.ActualizarPropiedades(usuario);
            usuarioFound.contrasena = _asignaciones.EncriptPassword(usuario.contrasena);
            usuarioFound.updated_at = _asignaciones.GetCurrentDateTime();
            usuarioFound.activo = usuario.activo;
            usuario.modificado_por = _asignaciones.GetClaimValue(token!, "User") ?? "Sistema";
            var u = await _repository.PutUsuario(usuarioFound);
            var usuarioDto = new UsuarioDto
            {
                id = u.id,
                usuario = u.usuario,
                contrasena = u.contrasena,
                empleado = u.Empleado!.nombre,
                rol = u.Role!.Nombre,
                rolId = u.role_id,
                empleadoId = u.role_id
            };
            return usuarioDto;

        }




    }
}
