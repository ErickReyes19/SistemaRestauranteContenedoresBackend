using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


    public partial class Empleado
    {
        [Key]
        [StringLength(36)]
        public string? id { get; set; }

        [Required]
        [StringLength(100)]
        public string nombre { get; set; }

        [Required]
        [StringLength(100)]
        public string apellido { get; set; }

        [Required]
        [EmailAddress]
        public string correo { get; set; }

        public int? edad { get; set; }

        public string genero { get; set; }

        [Required]
        [Column(TypeName = "bit")]
        public bool activo { get; set; } = true;

        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }


        public Usuario? Usuario { get; set; }

    }

