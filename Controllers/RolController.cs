using RestauranteBackend.interfaces.Rol;
using RestauranteBackend.Models.Rol;
using Microsoft.AspNetCore.Mvc;

namespace RestauranteBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        private readonly IRolesService _rolService;

        public RolController(IRolesService rolService)
        {
            _rolService = rolService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RolDto>>> GetRoles()
        {
            var roles = await _rolService.GetRoles();
            if (roles == null || !roles.Any())
                return NotFound("No se encontraron roles.");
            return Ok(roles);
        }         
        [HttpGet("{id}")]
        public async Task<ActionResult<RolDto>> GetRolById(string id)
        {
            var roles = await _rolService.GetRolById(id);
            if (roles == null)
                return NotFound("No se encontraron el rol.");
            return Ok(roles);
        }        
        [HttpGet("activos")]
        public async Task<ActionResult<IEnumerable<RolDto>>> GetRolesActivos()
        {
            var roles = await _rolService.GetRolesActivos();
            if (roles == null || !roles.Any())
                return NotFound("No se encontraron roles.");
            return Ok(roles);
        }

        [HttpPost("{idRol}/asignar-permisos")]
        public async Task<ActionResult> AsignarPermisos(string idRol, List<string> idsPermisos)
        {
            try
            {
                bool success = await _rolService.AssignPermissionsToRole(idRol, idsPermisos);
                if (success)
                {
                    return Ok("Permisos asignados correctamente.");
                }
                else
                {
                    return BadRequest("No se pudieron asignar todos los permisos.");
                }
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno del servidor.", details = ex.Message });
            }
        }


        [HttpPost]
        public async Task<ActionResult<RolDto>> CreateRoles(Role rol)
        {
            var rolCreate = await _rolService.PostRol(rol);
            return Ok(rolCreate);
        }        
        [HttpPut("{id}")]
        public async Task<ActionResult>UpdateRoles( string id, Role rol)
        {
            var rolUpdate = await _rolService.PutRol(rol, id);
            return Ok(rolUpdate);
        }
    }
}
