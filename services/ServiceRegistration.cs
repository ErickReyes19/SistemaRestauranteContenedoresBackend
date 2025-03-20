using RestauranteBackend.interfaces.IEmpleado;
using RestauranteBackend.Repositories;
using RestauranteBackend.services;
using RestauranteBackend.interfaces.IUsuario;
using RestauranteBackend.repositories;
using RestauranteBackend.interfaces.Rol;
using RestauranteBackend.interfaces.ILogin;
using Inventario.interfaces.IUsuario;
using Inventario.services;
using RestauranteBackend.Interfaces;

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

        //Asignaciones
        services.AddScoped<IAsignaciones, AsingacionesService>(); 
    }
}
