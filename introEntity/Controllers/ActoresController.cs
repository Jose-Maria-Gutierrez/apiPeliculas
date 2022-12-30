using AutoMapper;
using AutoMapper.QueryableExtensions;
using introEntity.DTOs;
using introEntity.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace introEntity.Controllers
{
    [ApiController]
    [Route("api/actores")]
    public class ActoresController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ActoresController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> Post(ActorDTO actorDTO)
        {
            var actor = mapper.Map<Actor>(actorDTO);
            context.Add(actor);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Actor>>> Get()
        {
            return await context.Actores.OrderBy(a => a.Nombre).ThenBy(a => a.FechaNacimiento).ToListAsync();
            //ordenados por nombre y dsp por fecha nacimiento
        }

        [HttpGet("nombre")]
        public async Task<ActionResult<IEnumerable<Actor>>> Get(string nombre) //el nombre tal cual
        {
            return await context.Actores.Where(a => a.Nombre == nombre).ToListAsync();
        }

        [HttpGet("nombre/v2")]
        public async Task<ActionResult<IEnumerable<Actor>>> GetV2(string nombre) //cualquier actor que contenga ese nombre
        {
            return await context.Actores.Where(a => a.Nombre.Contains(nombre)).ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Actor>> GetId(int id)
        {
            var actor = await context.Actores.FirstOrDefaultAsync(a => a.Id == id);
            if (actor == null)
            {
                return NotFound();
            }
            return actor;
        }

        [HttpGet("idNombre")]
        public async Task<ActionResult<IEnumerable<ActorRespuestaDTO>>> GetIdNombre()
        {
            return await context.Actores.ProjectTo<ActorRespuestaDTO>(mapper.ConfigurationProvider).ToListAsync();
            //proyecta de actores a actorDTO automaticamente
            //await context.Actores.Select(a => new ActorRespuestaDTO{Id=a.Id,Nombre=a.Nombre }).ToListAsync();
            //el select usa el DTORespuesta
        }
    }
}
