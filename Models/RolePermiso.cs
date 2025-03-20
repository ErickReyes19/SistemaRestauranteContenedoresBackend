using System.Data;

namespace RestauranteBackend.Models
{
    public class RolePermiso
    {
        public string RolId { get; set; }
        public Role Rol { get; set; }

        public string PermisoId { get; set; }
        public Permiso Permiso { get; set; }
    }

}
