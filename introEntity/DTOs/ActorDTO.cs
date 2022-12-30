using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace introEntity.DTOs
{
    public class ActorDTO
    {
        [StringLength(150)]
        public string Nombre { get; set; } = null!;
        public decimal Fortuna { get; set; }
        public DateTime FechaNacimiento{ get; set; }
    }
}
