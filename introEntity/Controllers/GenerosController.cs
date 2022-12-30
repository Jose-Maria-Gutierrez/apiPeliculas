using AutoMapper;
using introEntity.DTOs;
using introEntity.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace introEntity.Controllers
{
    [ApiController]
    [Route("api/generos")]
    public class GenerosController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public GenerosController(ApplicationDbContext context,IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> Post(GeneroDTO generoDTO)
        {
            var genero = mapper.Map<Genero>(generoDTO); //de DTO a clase
            context.Add(genero);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("varios")]
        public async Task<ActionResult> Post(GeneroDTO[] generosDTO)
        {
            var genero = mapper.Map<Genero[]>(generosDTO); //de DTO a clase
            context.AddRange(genero);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Genero>>> Get()
        {
            return await context.Generos.ToListAsync();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id,GeneroDTO generoDTO) //actualizar
        {
            var genero = mapper.Map<Genero>(generoDTO);
            genero.Id = id;
            context.Update(genero);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}/moderna")]
        public async Task<ActionResult> Delete(int id)
        {
            var filasAlteradas = await context.Generos.Where(g => g.Id==id).ExecuteDeleteAsync();
            if(filasAlteradas==0)
                return NotFound();
            return Ok();
        }
    }
}
