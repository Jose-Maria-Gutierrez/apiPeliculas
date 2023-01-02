using System.ComponentModel.DataAnnotations;

namespace introEntity.DTOs
{
    public class UsuarioRegistroDTO
    {
        public string nombreUsuario { get; set; }
        public string pwd { get; set; }
        public string rol { get; set; }
        public int cantidadVistas { get; set; }
    }
}
