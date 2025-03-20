namespace RestauranteBackend.Models.Usuario
{
    public class UsuarioDto
    {
        public string? id { get; set; }
        public string usuario { get; set; }
        public string contrasena { get; set; }
        public string rol { get; set; }
        public string rolId { get; set; }
        public string empleado { get; set; }
        public string empleadoId { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
    }
}
