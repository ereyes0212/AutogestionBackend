using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoGestion.Models
{
    public class CamposTipoSolicitud
    {
        [Key]
        [StringLength(36)]
        public string? Id { get; set; }
        [StringLength(36)]
        public string? TipoSolicitudId { get; set; }        
        [StringLength(36)]
        public string? Nombre { get; set; }        
        [StringLength(36)]
        public string? TipoCampo { get; set; }
        public int nivel { get; set; }
        [Required]
        [Column(TypeName = "bit")]
        public bool EsRequerido { get; set; } = true;        
        [Required]
        [Column(TypeName = "bit")]
        public bool Activo { get; set; } =true;
        public DateTime? Created_at { get; set; }
        public DateTime? Updated_at { get; set; }
        public string? Adicionado_por { get; set; }
        public string? Modificado_por { get; set; }
        [ForeignKey("TipoSolicitudId")]
        public TipoSolicitud? TipoSolicitud { get; set; }
    }
}
