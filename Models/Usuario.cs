using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;
using RestauranteBackend.Models;



    public partial class Usuario
    {
        [Key]
        [StringLength(36)]
        public string? id { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario { get; set; }

        [Required]
        public string contrasena { get; set; }

        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }

        [StringLength(36)]
        public string empleado_id { get; set; }

        [StringLength(36)]
        [Required]
        public string role_id { get; set; }

        [Required]
        [Column(TypeName = "bit")]
        public bool activo { get; set; } = true;

        [ForeignKey("role_id")]
        public Role? Role { get; set; }

        [ForeignKey("empleado_id")]
        public Empleado? Empleado { get; set; }
    }


