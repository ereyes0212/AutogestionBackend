using AutoGestion.Models;

public class EmpleadoDTO
{
    public string id { get; set; }
    public string nombre { get; set; }
    public string apellido { get; set; }
    public string correo { get; set; }
    public int? edad { get; set; }
    public string genero { get; set; }
    public bool activo { get; set; }

    public string empresa { get; set; }
    public string usuario { get; set; } 

    public string puesto { get; set; }  
    public string jefe { get; set; }     
}
