using RestauranteBackend.Models;

namespace RestauranteBackend.interfaces.ILogin
{
    public interface ILoginRepository
    {
        Task<Usuario> GetUserByUsername(string username);
        Task<List<string>> GetUserPermissions(string userId);
    }
}
