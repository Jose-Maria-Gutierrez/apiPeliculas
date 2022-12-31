namespace introEntity.DTOs
{
    public class PeliculaRespuestaDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public IEnumerable<string> generos{ get; set; }
        public IEnumerable<ActorEnPeliculaDTO> actores { get; set; }
        public int cantComentarios { get; set; }
    }
}
