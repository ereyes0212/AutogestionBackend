using AutoGestion.interfaces;
using AutoGestion.interfaces.ILogin;

namespace AutoGestion.services
{
    public class LoginService: ILoginService
    {
        private readonly ILoginRepository _loginRepository;
        private readonly IAsignaciones _asignaciones;
        public LoginService (ILoginRepository loginRepository, IAsignaciones asignacionesService)
        {
            _loginRepository = loginRepository;
            _asignaciones = asignacionesService;
        }

        public async Task<string> Login(string username, string password)
        {
            var user = await _loginRepository.GetUserByUsername(username);
            if (user == null)
            {
                throw new UnauthorizedAccessException("Usuario no encontrado.");
            }

            bool isPasswordValid = _asignaciones.VerifyPassword(password, user.contrasena);
            if (!isPasswordValid)
            {
                throw new UnauthorizedAccessException("Contraseña incorrecta.");
            }

            var userPermissions = await _loginRepository.GetUserPermissions(user.id!);


            var data = new
            {
                IdUser = user.id,
                User = user.usuario,
                Rol = user.Role!.Nombre,
                IdRol = user.Role.Id,
                DebeCambiar = user.DebeCambiarPassword,
                IdEmpleado = user.empleado_id,
                Permiso = userPermissions,
                Puesto = user.Empleado!.Puesto!.Nombre,
                PuestoId = user.Empleado.Puesto.Id
            };

            var token = _asignaciones.GenerateJwtToken(data);

            return token;
        }

        public async Task<string> ResetPassword(string username, string newPassword)
        {
            var usuario = await _loginRepository.GetUserByUsername(username)
                ?? throw new KeyNotFoundException($"Usuario '{username}' no encontrado.");

            usuario.contrasena = _asignaciones.EncriptPassword(newPassword);
            usuario.DebeCambiarPassword = false;
            usuario.updated_at = _asignaciones.GetCurrentDateTime();

            var updated = await _loginRepository.ResetPassword(usuario);


            var permisos = await _loginRepository.GetUserPermissions(updated.id!);
            var payload = new
            {
                IdUser = updated.id,
                User = updated.usuario,
                Rol = updated.Role!.Nombre,
                IdRol = updated.Role.Id,
                DebeCambiar = updated.DebeCambiarPassword,
                IdEmpleado = updated.empleado_id,
                Permiso = permisos,
                Puesto = updated.Empleado!.Puesto!.Nombre,
                PuestoId = updated.Empleado.Puesto.Id
            };

            var newToken = _asignaciones.GenerateJwtToken(payload);
            return newToken;
        }

    }
}
