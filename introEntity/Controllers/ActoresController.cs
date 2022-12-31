using AutoMapper;
using AutoMapper.QueryableExtensions;
using introEntity.DTOs;
using introEntity.Entidades;
using introEntity.UoW;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace introEntity.Controllers
{
    [ApiController]
    [Route("api/actores")]
    public class ActoresController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public ActoresController(IMapper mapper,IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        
        [HttpPost]
        public async Task<ActionResult> Post(ActorDTO actorDTO)
        {
            using (this.unitOfWork)
            {
                var actor = mapper.Map<Actor>(actorDTO);
                await this.unitOfWork.actorRepository.add(actor);
                await this.unitOfWork.saveChanges();
                return Ok();
            }
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Actor>>> Get()
        {
            var resp = await this.unitOfWork.actorRepository.getAll();
            return Ok(resp);
            //ordenados por nombre y dsp por fecha nacimiento
        }
        
        [HttpGet("nombre")]
        public async Task<ActionResult<IEnumerable<Actor>>> Get(string nombre) //el nombre tal cual
        {
            var resp = await this.unitOfWork.actorRepository.getNombre(nombre);
            return Ok(resp);
        }
        /*
        [HttpGet("nombre/v2")]
        public async Task<ActionResult<IEnumerable<Actor>>> GetV2(string nombre) //cualquier actor que contenga ese nombre
        {
            return await this.unitOfWork.context.Actores.Where(a => a.Nombre.Contains(nombre)).ToListAsync();
        }*/
        
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Actor>> GetId(int id)
        {
            var actor = await this.unitOfWork.actorRepository.getById(id);
            if (actor == null)
            {
                return NotFound();
            }
            return actor;
        }
        /*
        [HttpGet("idNombre")]
        public async Task<ActionResult<IEnumerable<ActorRespuestaDTO>>> GetIdNombre()
        {
            return await this.unitOfWork.context.Actores.ProjectTo<ActorRespuestaDTO>(mapper.ConfigurationProvider).ToListAsync();
            //proyecta de actores a actorDTO automaticamente
            //await context.Actores.Select(a => new ActorRespuestaDTO{Id=a.Id,Nombre=a.Nombre }).ToListAsync();
            //el select usa el DTORespuesta
        }*/
    }
}
