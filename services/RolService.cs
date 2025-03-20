
using RestauranteBackend.interfaces.Rol;
using RestauranteBackend.Utils;
using RestauranteBackend.Interfaces;
using RestauranteBackend.Models.Rol;

namespace Inventario.services
{
    public class RolService: IRolesService
    {
        private readonly IRolRepository _rolrepository;
        private readonly IAsignaciones _AsinacionesService;
        public RolService (IRolRepository rolrepository, IAsignaciones asignacionesService)
        {
            _rolrepository = rolrepository;
            _AsinacionesService = asignacionesService;
        }

        public async Task<IEnumerable<RolDto>> GetRoles()
        {
            var roles = await _rolrepository.GetRoles();
            if (roles == null)
            {
                return null;
            }
            var rolesDto = roles.Select(r => new RolDto
            {
                Nombre = r.Nombre,
                Descripcion = r.Descripcion,
                Activo = r.Activo,
                Id = r.Id,
                CreatedAt = r.CreatedAt,
                UpdatedAt = r.UpdatedAt  
            });
            return rolesDto;
        }
        public async Task<IEnumerable<RolDto>> GetRolesActivos()
        {
            var roles = await _rolrepository.GetRolesActivos();
            if (roles == null)
            {
                return null;
            }
            var rolesDto = roles.Select(r => new RolDto
            {
                Nombre = r.Nombre,
                Descripcion = r.Descripcion,
                Activo = r.Activo,
                Id = r.Id,
                CreatedAt = r.CreatedAt,
                UpdatedAt = r.UpdatedAt  
            });
            return rolesDto;
        }  
        
        public async Task<RolDto> GetRolById(string id)
        {
            var r = await _rolrepository.GetRolesById(id);
            if (r == null)
            {
                return null;
            }
            var rolesDto = new RolDto
            {
                Nombre = r.Nombre,
                Descripcion = r.Descripcion,
                Activo = r.Activo,
                Id = r.Id,
                CreatedAt = r.CreatedAt,
                UpdatedAt = r.UpdatedAt  
            };
            return rolesDto;
        }
                
        public async Task<RolDto> PostRol(Role rol)
        {
            
            rol.Id = _AsinacionesService.GenerateNewId();
            rol.Activo = true;
            rol.CreatedAt = _AsinacionesService.GetCurrentDateTime();
            rol.UpdatedAt = _AsinacionesService.GetCurrentDateTime();
            await _rolrepository.PostRol(rol);
            var rolDto = new RolDto
            {
                Nombre = rol.Nombre,
                Descripcion= rol.Descripcion,
                Activo = rol.Activo,
                Id = rol.Id,
                CreatedAt = rol.CreatedAt,
                UpdatedAt = rol.UpdatedAt
            };
            return rolDto;
        }        
        public async Task<RolDto> PutRol(Role rol, string id)
        {
            var rolFound = await _rolrepository.GetRolesById(id);
            if (rolFound == null)
            {
                return null;
            }
            rolFound.ActualizarPropiedades(rol);
            rolFound.UpdatedAt = _AsinacionesService.GetCurrentDateTime();
            await _rolrepository.PutRol(rolFound, id);
            return new RolDto
            {
                Nombre = rolFound.Nombre,
                Descripcion = rolFound.Descripcion,
                Activo = rolFound.Activo,
                Id = rolFound.Id,
                CreatedAt = rolFound.CreatedAt,
                UpdatedAt = rolFound.UpdatedAt
            };
        }        
        public async Task<bool> AssignPermissionsToRole(string id, List<string> ids)
        {
            return await _rolrepository.AssignPermissions(id, ids);
        }

    }
}
