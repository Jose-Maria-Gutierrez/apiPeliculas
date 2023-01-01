using AutoMapper;
using introEntity.DTOs;
using introEntity.Entidades;
using introEntity.UoW;
using Microsoft.AspNetCore.Mvc;

namespace introEntity.Controllers
{
    [ApiController]
    [Route("api/peliculas/{peliculaId:int}/comentarios")]
    public class ComentariosController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<ComentariosController> logger;

        public ComentariosController(IMapper mapper,IUnitOfWork unitOfWork,ILogger<ComentariosController> logger)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }
        
        [HttpPost]
        public async Task<ActionResult> Post(int peliculaId,ComentarioDTO comentarioDTO)
        {
            var comentario = mapper.Map<Comentario>(comentarioDTO);
            comentario.PeliculaId = peliculaId;
            await this.unitOfWork.comentarioRepository.add(comentario);
            await this.unitOfWork.saveChanges();
            this.logger.LogInformation($"se agrega comentario para la pelicula: {peliculaId}");
            return Ok();
        }
    }
}
