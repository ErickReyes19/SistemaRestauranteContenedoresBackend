
using RestauranteBackend.interfaces.ILogin;
using RestauranteBackend.Interfaces;

namespace RestauranteBackend.services
{
    public class LoginService: ILoginService
    {
        private readonly ILoginRepository _loginRepository;
        private readonly IAsignaciones _asignaciones;
        public LoginService (ILoginRepository loginRepository, IAsignaciones asignacionesService)
        {
            _loginRepository = loginRepository;
            _asignaciones = asignacionesService;
        }

        public async Task<string> Login(string username, string password)
        {
            var user = await _loginRepository.GetUserByUsername(username);
            if (user == null)
            {
                throw new UnauthorizedAccessException("Usuario no encontrado.");
            }

            bool isPasswordValid = _asignaciones.VerifyPassword(password, user.contrasena);
            if (!isPasswordValid)
            {
                throw new UnauthorizedAccessException("Contraseña incorrecta.");
            }

            var userPermissions = await _loginRepository.GetUserPermissions(user.id!);

            var data = new
            {
                IdUser = user.id,
                User = user.usuario,
                Rol = user.Role!.Nombre,
                IdRol = user.Role.Id,
                IdEmpleado = user.empleado_id,
                Permissions = string.Join(",", userPermissions)
            };
            
            var token = _asignaciones.GenerateJwtToken(data);

            return token;
        }
    }
}
