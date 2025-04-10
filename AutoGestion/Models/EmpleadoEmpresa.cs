using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoGestion.Models
{
    public class EmpleadoEmpresa
    {
        [Required]
        [StringLength(36)]
        public string EmpleadoId { get; set; }

        [ForeignKey(nameof(EmpleadoId))]
        public Empleado Empleado { get; set; }

        [Required]
        [StringLength(36)]
        public string EmpresaId { get; set; }

        [ForeignKey(nameof(EmpresaId))]
        public Empresa Empresa { get; set; }
    }
}
