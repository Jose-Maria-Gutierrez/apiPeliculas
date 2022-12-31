using AutoMapper;
using introEntity.DTOs;
using introEntity.Entidades;
using introEntity.UoW;
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

        public GenerosController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<ActionResult> Post(GeneroDTO generoDTO)
        {
            var genero = mapper.Map<Genero>(generoDTO); //de DTO a clase
                                                        //context.Add(genero);
            await this.unitOfWork.generoRepository.add(genero);
            await this.unitOfWork.saveChanges();
            return Ok();
        }

        [HttpPost("varios")]
        public async Task<ActionResult> Post(GeneroDTO[] generosDTO)
        {
            var generos = mapper.Map<Genero[]>(generosDTO); //de DTO a clase
            await this.unitOfWork.generoRepository.agregarVarios(generos);
            await this.unitOfWork.saveChanges();
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Genero>>> Get()
        {
            var res = this.unitOfWork.generoRepository.getAll();
            return Ok(res.Result);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, GeneroDTO generoDTO) //actualizar
        {
            using (this.unitOfWork)
            {
                Genero nuevo = mapper.Map<Genero>(generoDTO);
                nuevo.Id = id;
                this.unitOfWork.generoRepository.update(nuevo);
                await this.unitOfWork.saveChanges();
                return Ok();
            }
        }

        [HttpDelete("{id:int}/moderna")]
        public async Task<ActionResult> Delete(int id)
        {
            //await this.unitOfWork.context.Generos.Where(g => g.Id == id).ExecuteDeleteAsync
            var item = this.unitOfWork.generoRepository.getById(id);
            if (item == null)
                return NotFound();
            this.unitOfWork.generoRepository.delete(item.Result);
            await this.unitOfWork.saveChanges();
            return Ok();
        }
    }
}
