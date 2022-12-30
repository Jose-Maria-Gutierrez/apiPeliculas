using System.ComponentModel.DataAnnotations;

namespace introEntity.Entidades
{
    public class Genero
    {
        public int Id { get; set; }
        [StringLength(maximumLength:150)]
        public string Nombre { get; set; } = null!; //le perdona el null para que no salga el warning
        public HashSet<Pelicula> Peliculas { get; set; } = new HashSet<Pelicula>();
    }
}
