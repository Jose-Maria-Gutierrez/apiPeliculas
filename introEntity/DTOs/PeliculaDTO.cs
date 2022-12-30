namespace introEntity.DTOs
{
    public class PeliculaDTO
    {
        public string Titulo { get; set; }
        public bool EnCines { get; set; }
        public DateTime FechaEstreno { get; set; }
        public List<int> Generos { get; set; } = new List<int>();
        public List<PeliculaActorDTO> PeliculasActores { get; set; } = new List<PeliculaActorDTO>();
    }
}
