using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace introEntity.DTOs
{
    public class GeneroDTO
    {
        [StringLength(maximumLength: 150)]
        public string Nombre { get; set; } = null!;
    }
}
