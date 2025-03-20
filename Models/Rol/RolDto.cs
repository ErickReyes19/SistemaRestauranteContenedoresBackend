namespace RestauranteBackend.Models.Rol
{
    public class RolDto
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool Activo { get; set; }
    }
}
