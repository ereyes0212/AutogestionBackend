using AutoGestion.interfaces.IEmpleado;
using AutoGestion.interfaces;
using AutoGestion.Repositories;
using AutoGestion.services;
using AutoGestion.interfaces.IUsuario;
using AutoGestion.repositories;
using AutoGestion.interfaces.Rol;
using AutoGestion.interfaces.ILogin;
using AutoGestion.interfaces.IPuesto;
using AutoGestion.interfaces.IConfiguracion;
using AutoGestion.interfaces.ISolicitudVacaciones;
using AutoGestion.Services.SolicitudVacaciones;
using AutoGestion.Repositories.SolicitudVacaciones;

public static class ServiceRegistration
{
    public static void AddServices(this IServiceCollection services)
    {
        // Empleado
        services.AddScoped<IEmpleadoRepository, EmpleadoRepository>();
        services.AddScoped<IEmpleadoService, EmpleadoService>();
        // Usuario
        services.AddScoped<IUsuarioRepository, UsuarioRepositoy>();
        services.AddScoped<IUsuarioService, UsuarioService>(); 
        //Rol
        services.AddScoped<IRolRepository, RolRepository>();
        services.AddScoped<IRolesService, RolService>();  
        //Login
        services.AddScoped<ILoginRepository, LoginRepository>();
        services.AddScoped<ILoginService, LoginService>();  

        //Empresa
        services.AddScoped<IEmpresaRepository, EmpresaRepository>();
        services.AddScoped<IEmpresaService, EmpresaService>();   
        
        //Puesto
        services.AddScoped<IPuestoRepository, PuestoRepository>();
        services.AddScoped<IPuestoService, PuestoService>();   
        
        //ConfiguracionAprobacion
        services.AddScoped<IConfiguracionAprobacionRepository, ConfiguracionAprobacionRepository>();
        services.AddScoped<IConfiguracionAprobacionService, ConfiguracionAprobacionService>();   
        
        //TipoSolicitud
        services.AddScoped<ITipoSolicitudRepository, TipoSolicitudRepository>();
        services.AddScoped<ITipoSolicitudService, TipoSolicitudService>();   
        
        //SolicitudVacaciones
        services.AddScoped<ISolicitudVacacionesRepository, SolicitudVacacionesRepository>();
        services.AddScoped<ISolicitudVacacionesService, SolicitudVacacionesService>();   

        //Asignaciones
        services.AddScoped<IAsignaciones, AsingacionesService>(); 
    }
}
