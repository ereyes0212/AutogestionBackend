using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoGestion.Models
{
    public class ConfiguracionAprobacion
    {
        [Key]
        [StringLength(36)]
        public string? Id { get; set; }

        [StringLength(36)]
        public string? Empresa_id { get; set; }

        [StringLength(36)]
        public string? puesto_id { get; set; }

        [Required]
        [StringLength(100)]
        public string Descripcion { get; set; }

        [Required]
        [StringLength(50)]
        public string Tipo { get; set; }

        public int nivel { get; set; }

        [Required]
        [Column(TypeName = "bit")]
        public bool Activo { get; set; }

        public DateTime? Created_at { get; set; }
        public DateTime? Updated_at { get; set; }

        [StringLength(100)]
        public string? Adicionado_por { get; set; }

        [StringLength(100)]
        public string? Modificado_por { get; set; }

        [ForeignKey("Empresa_id")]
        public Empresa? Empresa { get; set; }

        [ForeignKey("puesto_id")]
        public Puesto? Puesto { get; set; }
    }
}
