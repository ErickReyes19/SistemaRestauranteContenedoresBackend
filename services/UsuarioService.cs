using Inventario.interfaces.IUsuario;

using RestauranteBackend.interfaces.IUsuario;
using RestauranteBackend.Interfaces;
using RestauranteBackend.Models.Usuario;
using RestauranteBackend.Utils;

namespace RestauranteBackend.services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _service;
        private readonly IAsignaciones _asignaciones;
        public UsuarioService(IUsuarioRepository usuarioService, IAsignaciones asignaciones)
        {
            _asignaciones = asignaciones;
            _service = usuarioService;
        }

        public async Task<IEnumerable<UsuarioDto>> GetUsuarios()
        {
            var usuarios = await _service.GetUsuarios();
            var usuariosDto = usuarios.Select(u => new UsuarioDto
            {
                id = u.id,
                usuario = u.usuario,
                contrasena = u.contrasena,
                created_at = u.created_at,
                updated_at = u.updated_at,
                empleado = u.Empleado.nombre,
                rol = u.Role.Nombre,
                rolId = u.role_id,
                empleadoId = u.role_id
            });
            return usuariosDto;
        }
        public async Task<IEnumerable<UsuarioDto>> GetUsuariosActivos()
        {
            var usuarios = await _service.GetUsuariosActivos();
            var usuariosDto = usuarios.Select(u => new UsuarioDto
            {
                id = u.id,
                usuario = u.usuario,
                contrasena = u.contrasena,
                created_at = u.created_at,
                updated_at = u.updated_at,
                empleado = u.Empleado.nombre,
                rol = u.Role.Nombre,
                rolId = u.role_id,
                empleadoId = u.role_id
            });
            return usuariosDto;
        }
        public async Task<UsuarioDto> GetUsuarioById(string id)
        {
            var u = await _service.GetUsuarioById(id);
            var usuarioDto = new UsuarioDto
            {
                id = u.id,
                usuario = u.usuario,
                contrasena = u.contrasena,
                created_at = u.created_at,
                updated_at = u.updated_at,
                empleado = u.Empleado.nombre,
                rol = u.Role.Nombre,
                rolId = u.role_id,
                empleadoId = u.role_id
            };
            return usuarioDto;
        }
        public Task<Usuario> PostUsuario(Usuario usuario)
        {
            usuario.id = _asignaciones.GenerateNewId();
            usuario.created_at = _asignaciones.GetCurrentDateTime();
            usuario.updated_at = _asignaciones.GetCurrentDateTime();
            usuario.contrasena = _asignaciones.EncriptPassword(usuario.contrasena);
            return _service.PostUsuario(usuario);
        }
        public async Task<UsuarioDto> PutUsuario(string id, Usuario usuario)
        {
            var usuarioFound = await _service.GetUsuarioById(id);
            if (usuarioFound == null)
            {
                return null;
            }

            usuarioFound.ActualizarPropiedades(usuario);
            usuarioFound.contrasena = _asignaciones.EncriptPassword(usuario.contrasena);
            usuarioFound.updated_at = _asignaciones.GetCurrentDateTime();
            var u = await _service.PutUsuario(usuarioFound, id);
            var usuarioDto = new UsuarioDto
            {
                id = u.id,
                usuario = u.usuario,
                contrasena = u.contrasena,
                created_at = u.created_at,
                updated_at = u.updated_at,
                empleado = u.Empleado.nombre,
                rol = u.Role.Nombre,
                rolId = u.role_id,
                empleadoId = u.role_id
            };
            return usuarioDto;

        }




    }
}
