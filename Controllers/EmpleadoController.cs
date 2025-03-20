using RestauranteBackend.interfaces.IEmpleado;
using RestauranteBackend.Models;
using RestauranteBackend.services;
using Microsoft.AspNetCore.Mvc;

namespace RestauranteBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        private readonly IEmpleadoService _empleadoService;

        public EmpleadoController(IEmpleadoService empleadoService)
        {
            _empleadoService = empleadoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Empleado>>> GetEmpleados()
        {

                var empleados = await _empleadoService.GetEmpleados();

                if (empleados == null || !empleados.Any())
                    return NotFound("No se encontraron empleados.");

                return Ok(empleados);

        }           
        
        [HttpPost]
        public async Task<ActionResult<Empleado>> CreateEmpleados(Empleado empleado)
        {

                var empleadoCreate = await _empleadoService.PostEmpleados(empleado);

                if (empleadoCreate == null)
                    return NotFound("No se encontraron empleados.");

                return Ok(empleadoCreate);
   
        }            
        
        [HttpPut("{id}")]
        public async Task<ActionResult<Empleado>> UpdateEmpleados(Empleado empleado, string id)
        {


                var empleadoCreate = await _empleadoService.PutEmpleados(id,empleado);

                if (empleadoCreate == null)
                    return NotFound("No se encontraron empleados.");

                return Ok(empleadoCreate);

        }     
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Empleado>> EmpleadosById(string id)
        {


                var empleadoCreate = await _empleadoService.GetEmpleadoById(id);

                if (empleadoCreate == null)
                    return NotFound("No se encontró el empleado.");

                return Ok(empleadoCreate);

        }        
        
        [HttpGet("activos")]
        public async Task<ActionResult<IEnumerable<Empleado>>> GetEmpleadosActivos()
        {

                var empleados = await _empleadoService.GetEmpleadosActivos();

                if (empleados == null || !empleados.Any())
                    return NotFound("No se encontraron empleados.");

                return Ok(empleados);

        }
    }
}
