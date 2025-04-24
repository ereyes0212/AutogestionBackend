namespace AutoGestion.Models.ConfiguracionPuesto
{
    public class ConfiguracionAprobacionDto
    {
        public string Id { get; set; }
        public string Puesto { get; set; }
        public string Descripcion { get; set; }
        public string Tipo { get; set; }
        public int Nivel { get; set; }
        public bool Activo { get; set; }
    }
}
