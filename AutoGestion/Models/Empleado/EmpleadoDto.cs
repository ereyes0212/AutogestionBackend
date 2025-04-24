

public class EmpleadoDTO
{
    public string id { get; set; }
    public string nombre { get; set; }
    public string apellido { get; set; }
    public string correo { get; set; }
    public DateTime? FechaNacimiento { get; set; }
    public string genero { get; set; }
    public int  Vacaciones { get; set; }
    public bool activo { get; set; }

    public string usuario_id { get; set; }
    public string usuario { get; set; }

    public string puesto { get; set; }
    public string puesto_id { get; set; }

    public string jefe { get; set; }
    public string jefe_id { get; set; }
}

public class EmpleadoCreateDto
{
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public string Correo { get; set; }
    public DateTime? FechaNacimiento { get; set; }
    public string Genero { get; set; }
    public bool Activo { get; set; }
    public int Vacaciones { get; set; }


    // ID del usuario asociado al empleado
    public string? usuario_id { get; set; }

    // Datos del puesto
    public string puesto_id { get; set; }

    // ID del jefe
    public string? jefe_id { get; set; }

}
