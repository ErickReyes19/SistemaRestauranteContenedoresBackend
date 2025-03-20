namespace RestauranteBackend.Models
{
    public partial class Permiso
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? Activo { get; set; }

        public ICollection<RolePermiso> RolePermisos { get; set; } = new List<RolePermiso>();
    }
}
