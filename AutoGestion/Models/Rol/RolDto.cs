using AutoGestion.Models.Rol;

namespace AutoGestion.models.Rol
{
    public class RolDto
    {
        public required string Id { get; set; }
        public required string Nombre { get; set; }
        public required string Descripcion { get; set; }
        public bool Activo { get; set; }
    }

    public class RoleWithPermissionsDto
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }
        public List<PermisoRolDto> Permisos { get; set; }
    }


    public class PermisoRolDtoCreate
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
    }

    public class RolCreateDto
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool? Activo { get; set; }  // Lo dejé nullable por si no lo mandás siempre
        public List<PermisoRolDtoCreate> Permisos { get; set; }
    }


    public class RolUpdateDto
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool? Activo { get; set; }
        public List<PermisoRolPutDto> Permisos { get; set; }
    }

    public class PermisoRolPutDto
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
    }


}
