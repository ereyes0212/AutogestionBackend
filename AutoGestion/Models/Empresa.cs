using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AutoGestion.Models;


public class Empresa
{
    [Key]
    [StringLength(36)]
    public string? Id { get; set; }

    [Required]
    [StringLength(100)]
    public string nombre { get; set; }

    [Required]
    [Column(TypeName = "bit")]
    public bool activo { get; set; } = true;
    public DateTime? created_at { get; set; }
    public DateTime? updated_at { get; set; }

    public string? adicionado_por { get; set; }
    public string? modificado_por { get; set; }

}

