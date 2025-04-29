using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AutoGestion.Models
{
    public class TipoDeduccion
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
        public bool Activo { get; set; } = true;
        public DateTime? Created_at { get; set; }
        public DateTime? Updated_at { get; set; }

        public string? Adicionado_por { get; set; }
        public string? Modificado_por { get; set; }
    }
}
