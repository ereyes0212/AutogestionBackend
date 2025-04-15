using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class TipoSolicitud
{
    [Key]
    [StringLength(36)]
    public string? Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Nombre { get; set; }

    [Required]
    [StringLength(100)]
    public string Descripcion { get; set; }

    [Required]
    [Column(TypeName = "bit")]
    public bool activo { get; set; } = true;

    public DateTime? created_at { get; set; }
    public DateTime? updated_at { get; set; }

    public string? adicionado_por { get; set; }
    public string? modificado_por { get; set; }
}


