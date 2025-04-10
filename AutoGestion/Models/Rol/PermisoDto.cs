namespace AutoGestion.Models.Rol
{
    public class PermisoDto
    {
        public required string Id { get; set; }
        public required string Nombre { get; set; }
        public required string Descripcion { get; set; }
        public bool? Activo { get; set; }
    }    
    public class PermisoRolDto
    {
        public required string Id { get; set; }
        public required string Nombre { get; set; }
    }
}
