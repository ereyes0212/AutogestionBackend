

namespace AutoGestion.models.Usuario
{
    public class UsuarioDto
    {
        public string? id { get; set; }
        public string usuario { get; set; }
        public string contrasena { get; set; }
        public string rol { get; set; }
        public string rol_id { get; set; }
        public string empleado { get; set; }
        public string empleado_id { get; set; }
        public bool activo { get; set; }
        public bool debeCambiarContrasena { get; set; }
    }
}
