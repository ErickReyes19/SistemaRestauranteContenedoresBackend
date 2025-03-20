using RestauranteBackend.Models;

namespace RestauranteBackend.interfaces.IEmpleado
{
    public interface IEmpleadoRepository
    {
        Task<IEnumerable<Empleado>> GetEmpleados();
        Task<Empleado> GetEmpleadoById(string id);
        Task<IEnumerable<Empleado>> GetEmpleadosActivos();
        Task<Empleado> PostEmpleados(Empleado empleado);
        Task<Empleado> PutEmpleados(string id, Empleado empleado);
    }
}
