using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AutoGestion.Models;



public partial class Empleado
{
    [Key]
    [StringLength(36)]
    public string? id { get; set; }

    [StringLength(36)]
    public string puesto_id { get; set; }

    [StringLength(36)]
    public string? jefe_id { get; set; }

    [Required]
    [StringLength(100)]
    public string nombre { get; set; }

    [Required]
    [StringLength(100)]
    public string apellido { get; set; }

    [Required]
    [EmailAddress]
    public string correo { get; set; }

    public DateTime? FechaNacimiento { get; set; }
    public int Vacaciones { get; set; }

    public string genero { get; set; }

    [Required]
    [Column(TypeName = "bit")]
    public bool activo { get; set; } = true;

    public DateTime? created_at { get; set; }
    public DateTime? updated_at { get; set; }

    public string? adicionado_por { get; set; }
    public string? modificado_por { get; set; }

    // Relaciones existentes
    [ForeignKey("puesto_id")]
    public Puesto? Puesto { get; set; }

    [ForeignKey("jefe_id")]
    public Empleado? Jefe { get; set; }

    public Usuario? Usuario { get; set; }

    // Nueva relación muchos a muchos con Empresa a través de EmpleadoEmpresa
    public ICollection<EmpleadoEmpresa> EmpleadoEmpresas { get; set; } = new List<EmpleadoEmpresa>();
}
