
using RestauranteBackend.Models.Empleado;

namespace RestauranteBackend.interfaces.IEmpleado
{
    public interface IEmpleadoService
    {
        Task<IEnumerable<EmpleadoDTO>> GetEmpleados();
        Task<IEnumerable<EmpleadoDTO>> GetEmpleadosActivos();
        Task<EmpleadoDTO> GetEmpleadoById(string id);
        Task<Empleado> PostEmpleados(Empleado empleado);
        Task<Empleado> PutEmpleados(string id, Empleado empleado);
    }
}
