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
            CreateMap<Actor, ActorRespuestaDTO>();
            CreateMap<ComentarioDTO, Comentario>();
            CreateMap<UsuarioRegistroDTO, Usuario>();
            CreateMap<PeliculaActorDTO, PeliculaActor>();
            CreateMap<PeliculaDTO, Pelicula>().ForMember(ent => ent.Generos,
                dto => dto.MapFrom(campo => campo.Generos.Select(id => new Genero { Id = id })));
            CreateMap<Actor, ActorRespuestaDTO>();
            CreateMap<Pelicula, PeliculaRespuestaDTO>()
            .ForMember(ent => ent.generos, dto => dto.MapFrom(campo => campo.Generos.Select(d => d.Nombre)))
            .ForMember(ent => ent.actores, dto => dto.MapFrom(campo => campo.PeliculasActores.Select(p => 
            new ActorEnPeliculaDTO { Id = p.ActorId, Nombre = p.Actor.Nombre, Personaje = p.Personaje})))
            .ForMember(ent => ent.cantComentarios, dto => dto.MapFrom(campo => campo.Comentarios.Count));
        }
    }
}
