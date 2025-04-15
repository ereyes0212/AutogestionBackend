namespace AutoGestion.Models.CampoTipoSolicitudDto
{
    public class CampoTipoSolicitudDto
    {
        public string Id { get; set; }
        public string TipoSolicitudId { get; set; }
        public string Nombre { get; set; }
        public string TipoCampo { get; set; }
        public string TipoSolicitud { get; set; }
        public int nivel { get; set; }
        public bool EsRequerido { get; set; }
        public bool Activo { get; set; }
    }
}
