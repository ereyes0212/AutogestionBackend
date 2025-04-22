using AutoGestion.interfaces;
using AutoGestion.interfaces.ILogin;
using AutoGestion.repositories;

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

            var empresas = user.Empleado?.EmpleadoEmpresas
                .Select(ee => new
                {
                    id = ee.Empresa.Id,
                    nombre = ee.Empresa.nombre
                }).ToList();

            var data = new
            {
                IdUser = user.id,
                User = user.usuario,
                Rol = user.Role!.Nombre,
                IdRol = user.Role.Id,
                IdEmpleado = user.empleado_id,
                Empresas = empresas,
                Permiso = userPermissions,
                Puesto = user.Empleado!.Puesto!.Nombre,
                PuestoId = user.Empleado.Puesto.Id
            };

            var token = _asignaciones.GenerateJwtToken(data);

            return token;
        }
    }
}
