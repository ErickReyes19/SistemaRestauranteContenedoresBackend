using RestauranteBackend.interfaces.ILogin;
using RestauranteBackend.Models;
using RestauranteBackend.services;
using Microsoft.EntityFrameworkCore;

namespace RestauranteBackend.repositories
{
    public class LoginRepository : ILoginRepository
    {
        private readonly DbContextInventario _dbContextInventario;
        public LoginRepository(DbContextInventario dbContextInventario)
        {
            _dbContextInventario = dbContextInventario;
        }

        public async Task<Usuario> GetUserByUsername(string username) {
            return await _dbContextInventario.Usuarios
                .Include(u => u.Role) 
                .FirstOrDefaultAsync(u => u.usuario == username);
        }

        public async Task<List<string>> GetUserPermissions(string userId)
        {
            // Obtener el usuario y su rol
            var user = await _dbContextInventario.Usuarios
                .Where(u => u.id == userId)
                .Include(u => u.Role)  // Incluir el rol del usuario
                .FirstOrDefaultAsync();

            if (user == null || user.Role == null)
            {
                throw new InvalidOperationException("Usuario o rol no encontrado");
            }

            // Obtener los permisos del rol del usuario
            var userPermissions = await _dbContextInventario.RolePermiso
                .Where(rp => rp.RolId == user.Role.Id)  // Filtrar por el rol del usuario
                .Select(rp => rp.Permiso.Nombre)  // Seleccionar los permisos
                .Distinct()  // Evitar duplicados
                .ToListAsync();

            return userPermissions;
        }




    }
}
