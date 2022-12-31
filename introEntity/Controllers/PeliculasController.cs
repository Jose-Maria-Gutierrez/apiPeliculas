using AutoMapper;
using introEntity.DTOs;
using introEntity.Entidades;
using introEntity.UoW;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace introEntity.Controllers
{
    [ApiController]
    [Route("api/peliculas")]
    public class PeliculasController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public PeliculasController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<ActionResult> Post(PeliculaDTO peliculaDTO)
        {
            var pelicula = mapper.Map<Pelicula>(peliculaDTO);
            await this.unitOfWork.peliculaRepository.agregarPeliculaCascada(pelicula);
            await this.unitOfWork.saveChanges();
            return Ok();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Pelicula>> Get(int id)
        {
            var pelicula = this.unitOfWork.peliculaRepository.getPelicula(id);
            if(pelicula==null)                
                return NotFound();
            return Ok(pelicula.Result);
        }

        [HttpGet("select/{id:int}")]
        public async Task<ActionResult<PeliculaRespuestaDTO>> GetSelect(int id)
        {
            var pelicula = this.unitOfWork.peliculaRepository.getPelicula(id); //no hacia el include de todas las demas clases
            if (pelicula == null)
                return NotFound();
            PeliculaRespuestaDTO resp = this.mapper.Map<PeliculaRespuestaDTO>(pelicula.Result);
            return Ok(resp); //probar este
        }

        [HttpDelete("{id:int}/moderna")]
        public async Task<ActionResult> Delete(int id)
        {
            using (this.unitOfWork)
            {
                var filasAlteradas = this.unitOfWork.peliculaRepository.eliminarPorId(id); //borra en cascada
                if (filasAlteradas.Result == 0)
                    return NotFound();
                return Ok();
            }
        }
    }
}
