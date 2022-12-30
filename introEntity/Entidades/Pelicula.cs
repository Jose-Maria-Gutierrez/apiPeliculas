using System.ComponentModel.DataAnnotations;

namespace introEntity.Entidades
{
    public class Pelicula
    {
        public int Id { get; set; }
        [StringLength(maximumLength: 150)]
        public string Titulo { get; set; } = null!;
        public bool EnCines { get; set; }
        public DateTime FechaEstreno { get; set; }
        public HashSet<Comentario> Comentarios { get; set; } = new HashSet<Comentario>(); //no ordena pero es mas rapido
        public HashSet<Genero> Generos { get; set; } = new HashSet<Genero>();
        public List<PeliculaActor> PeliculasActores { get; set; } = new List<PeliculaActor>();
    }
}
