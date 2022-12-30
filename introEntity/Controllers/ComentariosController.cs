using AutoMapper;
using introEntity.DTOs;
using introEntity.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace introEntity.Controllers
{
    [ApiController]
    [Route("api/peliculas/{peliculaId:int}/comentarios")]
    public class ComentariosController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ComentariosController(ApplicationDbContext context,IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> Post(int peliculaId,ComentarioDTO comentarioDTO)
        {
            var comentario = mapper.Map<Comentario>(comentarioDTO);
            comentario.PeliculaId = peliculaId;
            context.Add(comentario);
            await context.SaveChangesAsync();
            return Ok();
        }

    }
}
