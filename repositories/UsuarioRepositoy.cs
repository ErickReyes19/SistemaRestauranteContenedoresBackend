using RestauranteBackend.interfaces.IUsuario;
using RestauranteBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace RestauranteBackend.repositories
{
    public class UsuarioRepositoy: IUsuarioRepository
    {
        private readonly DbContextInventario _dbContextInventario;

        public UsuarioRepositoy(DbContextInventario dbContextInventario)
        {
            _dbContextInventario = dbContextInventario;
        }

        public async Task<IEnumerable<Usuario>> GetUsuarios()
        {
            return await _dbContextInventario.Usuarios.Include("Empleado").Include("Role").ToListAsync();
        }

        public async Task<IEnumerable<Usuario>> GetUsuariosActivos()
        {
            return await _dbContextInventario.Usuarios.Include("Empleado").Include("Role").Where(u => u.activo == true).ToListAsync();
        }

        public async Task<Usuario> GetUsuarioById(string id)
        {
            var usuario = await _dbContextInventario.Usuarios
                .Include("Empleado")
                .Include("Role")
                .Where(u => u.id == id)
                .FirstOrDefaultAsync(); // Usa FirstOrDefaultAsync en lugar de FirstAsync

            if (usuario == null)
            {
                throw new KeyNotFoundException("Usuario no encontrado."); // O manejar de otra forma
            }

            return usuario;
        }

        public async Task<Usuario>PostUsuario(Usuario usuario)
        {
            var usuariocreate = await _dbContextInventario.Usuarios.AddAsync(usuario);
            await _dbContextInventario.SaveChangesAsync();
            return usuariocreate.Entity;
        }

        public async Task<Usuario> PutUsuario(Usuario usuario, string id)
        {
            var existingUsuario = await _dbContextInventario.Usuarios.FindAsync(id);

            if (existingUsuario == null)
            {
                throw new KeyNotFoundException("Usuario no encontrado.");
            }

            _dbContextInventario.Entry(existingUsuario).CurrentValues.SetValues(usuario);

            var result = await _dbContextInventario.SaveChangesAsync();

            if (result == 0)
            {
                throw new InvalidOperationException("No se pudo actualizar el usuario.");
            }

            return existingUsuario;
        }



    }
}
