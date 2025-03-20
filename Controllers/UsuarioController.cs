using RestauranteBackend.interfaces.IUsuario;
using RestauranteBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Inventario.interfaces.IUsuario;

namespace RestauranteBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
                var usuarios = await _usuarioService.GetUsuarios();

                if (usuarios == null || !usuarios.Any())
                    return NotFound("No se encontraron usuarios.");

                return Ok(usuarios);


        }
        [HttpGet("activos")]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuariosActivos()
        {
                var usuarios = await _usuarioService.GetUsuariosActivos();

                if (usuarios == null || !usuarios.Any())
                    return NotFound("No se encontraron usuarios.");

                return Ok(usuarios);


        }
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarioById(string id)
        {

                var usuarios = await _usuarioService.GetUsuarioById(id);

                return Ok(usuarios);

        }
        [HttpPost]
        public async Task<ActionResult<Usuario>> CreateUsuarios(Usuario usuario)
        {
            var usuarios = await _usuarioService.PostUsuario(usuario);
            return CreatedAtAction(nameof(CreateUsuarios), new { id = usuarios.id }, usuarios);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Usuario>> UpdateUsuarios(string id, Usuario usuario)
        {

                var usuarios = await _usuarioService.PutUsuario(id, usuario);

                return Ok(usuarios);

        }
    }
}
