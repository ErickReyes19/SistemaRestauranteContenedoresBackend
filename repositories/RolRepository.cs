using RestauranteBackend.interfaces.Rol;
using RestauranteBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace RestauranteBackend.repositories
{
    public class RolRepository : IRolRepository
    {
        private readonly DbContextInventario _dbContextInventario;
        public RolRepository(DbContextInventario dbContextInventario)
        {
            _dbContextInventario = dbContextInventario;
        }

        public async Task<IEnumerable<Role>> GetRoles()
        {
            return await _dbContextInventario.Roles.ToListAsync();
        }
        public async Task<IEnumerable<Role>> GetRolesActivos()
        {
            return await _dbContextInventario.Roles.Where(r => r.Activo == true).ToArrayAsync();
        }
        public async Task<Role> GetRolesById(string id)
        {
            return await _dbContextInventario.Roles.Where(r => r.Id == id).FirstOrDefaultAsync();
        }
        public async Task<Role> PostRol(Role rol)
        {
            var rolCreate = await _dbContextInventario.Roles.AddAsync(rol);
            await _dbContextInventario.SaveChangesAsync();
            return rolCreate.Entity;
        }
        public async Task<Role> PutRol(Role rol, string id)
        {
            var existingRol = await _dbContextInventario.Roles.FindAsync(id);

            if (existingRol == null)
            {
                throw new KeyNotFoundException("Rol no encontrado.");
            }
            _dbContextInventario.Entry(existingRol).CurrentValues.SetValues(rol);

            var result = await _dbContextInventario.SaveChangesAsync();

            if (result == 0)
            {
                throw new InvalidOperationException("No se pudo actualizar el usuario.");
            }

            return existingRol;
        }
        public async Task<bool> AssignPermissions(string roleId, List<string> ids)
        {
            // Obtener el rol, incluyendo los permisos asociados
            var role = await _dbContextInventario.Roles
                .Include(r => r.RolePermisos)
                .FirstOrDefaultAsync(r => r.Id == roleId);

            if (role == null)
                throw new InvalidOperationException("Role not found.");

            var existingPermissions = _dbContextInventario.RolePermiso
                .Where(rp => rp.RolId == roleId);
            _dbContextInventario.RolePermiso.RemoveRange(existingPermissions);

            var permisos = await _dbContextInventario.Permisos
                .Where(p => ids.Contains(p.Id))
                .ToListAsync();

            var rolePermisos = permisos.Select(p => new RolePermiso
            {
                RolId = roleId,
                PermisoId = p.Id
            }).ToList();

            _dbContextInventario.RolePermiso.AddRange(rolePermisos);

            var result = await _dbContextInventario.SaveChangesAsync();

            return result > 0;
        }


    }
}
