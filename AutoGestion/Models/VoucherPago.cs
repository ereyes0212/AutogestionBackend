using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoGestion.Models
{
    public class VoucherPago
    {
        [Key]
        [StringLength(36)]
        public string? Id { get; set; }

        [StringLength(36)]
        public string EmpleadoId { get; set; }

        public DateTime FechaPago { get; set; }

        // Ya no usamos SalarioBruto/SalarioNeto
        public int DiasTrabajados { get; set; }
        public decimal SalarioDiario { get; set; }
        public decimal SalarioMensual { get; set; }
        public decimal NetoPagar { get; set; }

        public string Observaciones { get; set; }

        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public string? adicionado_por { get; set; }
        public string? modificado_por { get; set; }

        [ForeignKey("EmpleadoId")]
        public Empleado? Empleado { get; set; }

        public virtual ICollection<DetalleVoucherPago> DetallesVoucherPago { get; set; }
            = new List<DetalleVoucherPago>();
    }
}
