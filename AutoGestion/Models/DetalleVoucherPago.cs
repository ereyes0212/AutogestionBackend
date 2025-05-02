using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoGestion.Models
{
    public class DetalleVoucherPago
    {
        [Key]
        [StringLength(36)]
        public string? Id { get; set; }
        [StringLength(36)]
        public string VoucherPagoId { get; set; } 
        [StringLength(36)]
        public string TipoDeduccionId { get; set; }
        public decimal Monto { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }

        public string? adicionado_por { get; set; }
        public string? modificado_por { get; set; }

        [ForeignKey("VoucherPagoId")]
        public VoucherPago? VoucherPago { get; set; }
        [ForeignKey("TipoDeduccionId")]
        public TipoDeduccion? TipoDeduccion { get; set; }

    }
}
