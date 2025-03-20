
using RestauranteBackend.interfaces.IEmpleado;
using RestauranteBackend.Interfaces;
using RestauranteBackend.Models.Empleado;
using RestauranteBackend.Utils;

namespace RestauranteBackend.services
{
    public class EmpleadoService : IEmpleadoService
    {
        private readonly IEmpleadoRepository _empleadoRepository;
        private readonly IAsignaciones _AsinacionesService;

        public EmpleadoService(IEmpleadoRepository empleadoRepository, IAsignaciones asignacionesService)
        {
            _empleadoRepository = empleadoRepository;
            _AsinacionesService = asignacionesService;
        }

        public async Task<IEnumerable<EmpleadoDTO>> GetEmpleados()
        {

            var empleados = await _empleadoRepository.GetEmpleados();

            var empleadosDto = empleados.Select(e => new EmpleadoDTO
            {
                id = e.id,
                nombre = e.nombre,
                apellido = e.apellido,
                correo = e.correo,
                edad = e.edad,
                genero = e.genero,
                activo = e.activo,
                created_at = e.created_at,
                updated_at = e.updated_at,
                usuario = e.Usuario != null ? e.Usuario.usuario : null // Extrae solo el nombre de usuario
            });

            return empleadosDto;
        }
        public async Task<EmpleadoDTO?> GetEmpleadoById(string id)
        {
            var empleado = await _empleadoRepository.GetEmpleadoById(id);

            if (empleado == null) return null;

            var empleadoDto = new EmpleadoDTO
            {
                id = empleado.id,
                nombre = empleado.nombre,
                apellido = empleado.apellido,
                correo = empleado.correo,
                edad = empleado.edad,
                genero = empleado.genero,
                activo = empleado.activo,
                created_at = empleado.created_at,
                updated_at = empleado.updated_at,
                usuario = empleado.Usuario?.usuario // Manejo seguro de null
            };

            return empleadoDto;
        }


        public async Task<IEnumerable<EmpleadoDTO>> GetEmpleadosActivos()
        {
            var empleados = await _empleadoRepository.GetEmpleadosActivos();
            var empleadosDto = empleados.Select(e => new EmpleadoDTO
            {
                id = e.id,
                nombre = e.nombre,
                apellido = e.apellido,
                correo = e.correo,
                edad = e.edad,
                genero = e.genero,
                activo = e.activo,
                created_at = e.created_at,
                updated_at = e.updated_at,
                usuario = e.Usuario != null ? e.Usuario.usuario : null 
            });
            return empleadosDto;
        }

        public async Task<Empleado> PostEmpleados(Empleado empleado)
        {
            empleado.id = _AsinacionesService.GenerateNewId();
            empleado.created_at = _AsinacionesService.GetCurrentDateTime();
            empleado.updated_at = _AsinacionesService.GetCurrentDateTime();
            return await _empleadoRepository.PostEmpleados(empleado);
        }

        public async Task<Empleado> PutEmpleados(string id, Empleado empleado)
        {
            var empleadoFound = await _empleadoRepository.GetEmpleadoById(id);

            if (empleadoFound == null)
            {
                return null;
            }

            empleadoFound.ActualizarPropiedades(empleado);
            empleadoFound.activo = empleado.activo;
            empleadoFound.updated_at = _AsinacionesService.GetCurrentDateTime();

            return await _empleadoRepository.PutEmpleados(id, empleadoFound);
        }

    }
}
