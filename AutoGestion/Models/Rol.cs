
using System.ComponentModel.DataAnnotations;
using AutoGestion.Models;

public partial class Role
{
    [StringLength(36)]
    public string? Id { get; set; }
    [Required]
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? adicionado_por { get; set; }
    public string? modificado_por { get; set; }
    public bool Activo { get; set; }

    public ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
    public ICollection<RolePermiso> RolePermisos { get; set; } = new List<RolePermiso>();

}

