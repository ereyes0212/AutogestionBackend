using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoGestion.Models;

public partial class Usuario
{
    [Key]
    [StringLength(36)]
    public string? id { get; set; }

    [StringLength(36)]
    public string empleado_id { get; set; }


    [Required]
    [StringLength(50)]
    public string usuario { get; set; }

    [Required]
    public string contrasena { get; set; }

    public DateTime? created_at { get; set; }
    public DateTime? updated_at { get; set; }

    [StringLength(36)]
    [Required]
    public string rol_id { get; set; }

    [Required]
    [Column(TypeName = "bit")]
    public bool activo { get; set; } = true;

    [ForeignKey("rol_id")]
    public Role? Role { get; set; }

    [ForeignKey("empleado_id")]
    public Empleado? Empleado { get; set; }   
    

    public string? adicionado_por { get; set; }
    public string? modificado_por { get; set; }
}
