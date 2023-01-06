using AutoMapper;
using introEntity.DTOs;
using introEntity.Entidades;
using introEntity.UoW;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace introEntity.Controllers
{
    [ApiController]
    [Route("api/generos")]
    public class GenerosController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<GenerosController> logger;

        public GenerosController(IMapper mapper, IUnitOfWork unitOfWork,ILogger<GenerosController> logger)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        //[Authorize]
        [HttpPost]
        public async Task<ActionResult> Post(GeneroDTO generoDTO)
        {
            var genero = mapper.Map<Genero>(generoDTO); //de DTO a clase
                                                        //context.Add(genero);
            await this.unitOfWork.generoRepository.add(genero);
            await this.unitOfWork.saveChanges();
            this.logger.LogInformation("se agrega nuevo genero");
            return Ok();
        }

        [HttpPost("varios")]
        public async Task<ActionResult> Post(GeneroDTO[] generosDTO)
        {
            var generos = mapper.Map<Genero[]>(generosDTO); //de DTO a clase
            await this.unitOfWork.generoRepository.agregarVarios(generos);
            await this.unitOfWork.saveChanges();
            this.logger.LogInformation("se agregan varios generos");
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Genero>>> Get()
        {
            this.logger.LogInformation("se piden todos los generos");
            var res = await this.unitOfWork.generoRepository.getAll();
            return Ok(res);
        }

        //[Authorize(Policy = "Administrador")]
        [Authorize]
        [HttpGet("auto")]
        public async Task<ActionResult<IEnumerable<Genero>>> GetAutorizado()
        {
            this.logger.LogInformation("se piden todos los generos");
            var res = await this.unitOfWork.generoRepository.getAll();
            return Ok(res);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, GeneroDTO generoDTO) //actualizar
        {
            Genero nuevo = mapper.Map<Genero>(generoDTO);
            nuevo.Id = id;
            this.unitOfWork.generoRepository.update(nuevo);
            await this.unitOfWork.saveChanges();
            this.logger.LogInformation($"se actualiza el genero id: {id}");
            return Ok();
        }

        [HttpDelete("{id:int}/moderna")]
        public async Task<ActionResult> Delete(int id)
        {
            //await this.unitOfWork.context.Generos.Where(g => g.Id == id).ExecuteDeleteAsync
            var item = this.unitOfWork.generoRepository.getById(id);
            if (item.Result == null)
            {
                this.logger.LogWarning($"no se encuentra id de genero para borrar: {id}");
                return NotFound();
            }
            this.unitOfWork.generoRepository.delete(item.Result);
            await this.unitOfWork.saveChanges();
            this.logger.LogInformation($"se borra genero id: {id}");
            return Ok();
        }
    }
}
