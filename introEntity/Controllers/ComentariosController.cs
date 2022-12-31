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

        public ComentariosController(IMapper mapper,IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        
        [HttpPost]
        public async Task<ActionResult> Post(int peliculaId,ComentarioDTO comentarioDTO)
        {
            var comentario = mapper.Map<Comentario>(comentarioDTO);
            comentario.PeliculaId = peliculaId;
            await this.unitOfWork.comentarioRepository.add(comentario);
            await this.unitOfWork.saveChanges();
            return Ok();
        }
    }
}
