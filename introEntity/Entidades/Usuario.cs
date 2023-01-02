using System.ComponentModel.DataAnnotations;

namespace introEntity.Entidades
{
    public class Usuario
    {
        public int Id{ get; set; }
        [Required]
        [MaxLength(32)]
        public string nombreUsuario { get; set; }
        [Required]
        [MaxLength(32)]
        public string pwd { get; set; }
        [Required]
        [MaxLength(12)]
        public string rol { get; set; }
        public int cantidadVistas { get; set; }
    }
}
