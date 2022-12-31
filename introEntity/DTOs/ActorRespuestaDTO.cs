namespace introEntity.DTOs
{
    public class ActorRespuestaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
    }

    public class ActorEnPeliculaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set;}
        public string Personaje { get; set; }
    }

}
