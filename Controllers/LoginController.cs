using RestauranteBackend.interfaces.ILogin;
using RestauranteBackend.Models;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace Inventario.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDTo loginRequest)
        {
            if (loginRequest == null)
            {
                return BadRequest("Datos inválidos.");
            }

            try
            {
                // Realizar login y obtener el JWT
                var token = await _loginService.Login(loginRequest.username, loginRequest.password);
                return Ok(new { Token = token });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error en el servidor", details = ex.Message });
            }
        }
    }
}
