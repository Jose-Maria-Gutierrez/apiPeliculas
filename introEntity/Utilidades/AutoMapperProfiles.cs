using AutoMapper;
using introEntity.DTOs;
using introEntity.Entidades;

namespace introEntity.Utilidades
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<GeneroDTO, Genero>();
            CreateMap<ActorDTO, Actor>();
            CreateMap<Actor,ActorRespuestaDTO>();
            CreateMap<ComentarioDTO, Comentario>();
            CreateMap<PeliculaActorDTO, PeliculaActor>();
            CreateMap<PeliculaDTO, Pelicula>().ForMember(ent => ent.Generos,
                dto => dto.MapFrom(campo => campo.Generos.Select(id => new Genero { Id = id })));
        }
    }
}
