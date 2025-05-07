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
using AutoGestion.Services.SolicitudVacaciones;
using AutoGestion.Repositories.SolicitudVacaciones;
using AutoGestion.Interfaces.ISolicitudVacaciones;
using AutoGestion.interfaces.IEmailService;
using AutoGestion.interfaces.iTipoDeduccion;
using AutoGestion.interfaces.IVoucherPago;
using AutoGestion.Services;
using AutoGestion.interfaces.iTipoSeccion;
using AutoGestion.interfaces.IReporteDiseño;

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
        
        //SolicitudVacaciones
        services.AddScoped<ITipoDeduccionRepository, TipoDeduccionRepository>();
        services.AddScoped<ITipoDeduccionService, TipoDeduccionService>();   
        
        //Voucher
        services.AddScoped<IVoucherPagoRepository, VoucherPagoRepository>();
        services.AddScoped<IVoucherPagoService, VoucherPagoService>();   
        
        //TipoSecciones
        services.AddScoped<ITipoSeccionRepository, TipoSeccionRepository>();
        services.AddScoped<ITipoSeccionService, TipoSeccionService>();   
        
        //ReporteDiseño
        services.AddScoped<IReporteDiseñoReporsitory, ReporteDiseñoRepository>();
        services.AddScoped<IReporteDiseñoService, ReporteDiseñoService>();   

        //Asignaciones
        services.AddScoped<IAsignaciones, AsingacionesService>(); 
    }
}
