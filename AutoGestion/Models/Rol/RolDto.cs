namespace AutoGestion.models.Rol
{
    public class RolDto
    {
        public required string Id { get; set; }
        public required string Nombre { get; set; }
        public required string Descripcion { get; set; }
        public bool Activo { get; set; }
    }
}
