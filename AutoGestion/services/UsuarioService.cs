using AutoGestion.interfaces;
using AutoGestion.interfaces.IEmailService;
using AutoGestion.interfaces.IUsuario;
using AutoGestion.models.Usuario;
using AutoGestion.Models;
using AutoGestion.Utils;

namespace AutoGestion.services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repository;
        private readonly IEmailService _emailService;
        private readonly IAsignaciones _asignaciones;
        public UsuarioService(IUsuarioRepository usuarioService, IAsignaciones asignaciones, IEmailService emailService)
        {
            _asignaciones = asignaciones;
            _repository = usuarioService;
            _emailService = emailService;
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
                rol_id = u.rol_id,
                empleado_id = u.empleado_id,
                activo = u.activo
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
                rol_id = u.rol_id,
                empleado_id = u.empleado_id,
                activo = u.activo

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
                rol_id = u.rol_id,
                empleado_id = u.empleado_id,
                activo = u.activo
            };
            return usuarioDto;
        }


        public async Task<UsuarioDto> PostUsuario(Usuario usuario)
        {
            // 1) Generar contraseña temporal
            var tempPassword = _asignaciones.GenerarPasswordAleatoria(15);

            var token = _asignaciones.GetTokenFromHeader() ?? "";
            usuario.id = _asignaciones.GenerateNewId();
            usuario.created_at = _asignaciones.GetCurrentDateTime();
            usuario.updated_at = _asignaciones.GetCurrentDateTime();
            usuario.activo = true;
            usuario.contrasena = _asignaciones.EncriptPassword(tempPassword);
            usuario.DebeCambiarPassword = true;
            usuario.adicionado_por = _asignaciones.GetClaimValue(token, "User") ?? "Sistema";
            usuario.modificado_por = _asignaciones.GetClaimValue(token, "User") ?? "Sistema";

            // 2) Guardar el usuario
            await _repository.PostUsuario(usuario);

            // 3) Recuperar el usuario guardado (con relaciones)
            var usuarioCreate = await _repository.GetUsuarioById(usuario.id);

            // 4) Enviar correo con la contraseña temporal
            if (!string.IsNullOrWhiteSpace(usuarioCreate.Empleado.correo))
            {
                var email = usuarioCreate.Empleado.correo;
                var nombre = usuarioCreate.Empleado?.nombre ?? usuarioCreate.usuario;
                var asunto = "Bienvenido: tu cuenta ha sido creada";
                var cuerpo = $@"
<div style=""font-family:Arial,sans-serif;font-size:14px;color:#333;"">
  <h2>Hola {nombre},</h2>
  <p>Se ha creado tu cuenta en el sistema:</p>
  <ul>
    <li><strong>Usuario:</strong> {usuarioCreate.usuario}</li>
    <li><strong>Contraseña temporal:</strong> {tempPassword}</li>
  </ul>
  <p>Por seguridad, se te pedirá cambiar la contraseña en tu primer inicio de sesión.</p>
  <p style=""font-size:12px;color:#666;"">Este es un correo automático. No responder.</p>
</div>
                ";

                await _emailService.SendEmailAsync(email, asunto, cuerpo);
            }

            // 5) Devolver DTO (sin exponer la contraseña hasheada)
            return new UsuarioDto
            {
                id = usuarioCreate.id,
                usuario = usuarioCreate.usuario,
                empleado = usuarioCreate.Empleado?.nombre ?? "",
                rol = usuarioCreate.Role?.Nombre ?? "",
                rol_id = usuarioCreate.rol_id,
                empleado_id = usuarioCreate.empleado_id,
                activo = usuarioCreate.activo,
                debeCambiarContrasena = usuario.DebeCambiarPassword ?? false
                // omitimos contrasena del DTO por seguridad
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
                rol_id = u.rol_id,
                empleado_id = u.rol_id,
                activo = u.activo
                

            };
            return usuarioDto;

        }




    }
}
