using AutoMapper;
using AutoMapper.QueryableExtensions;
using introEntity.DTOs;
using introEntity.Entidades;
using introEntity.UoW;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace introEntity.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/actores")]
    public class ActoresController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<ActoresController> logger;

        public ActoresController(IMapper mapper,IUnitOfWork unitOfWork,ILogger<ActoresController> logger)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }
        
        [HttpPost]
        [Authorize(Policy = "Administrador")]
        public async Task<ActionResult> Post(ActorDTO actorDTO)
        {
            var actor = mapper.Map<Actor>(actorDTO);
            await this.unitOfWork.actorRepository.add(actor);
            await this.unitOfWork.saveChanges();
            this.logger.LogInformation("se agrega nuevo actor");
            return Ok();
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Actor>>> Get()
        {
            var resp = await this.unitOfWork.actorRepository.getAll();
            this.logger.LogInformation("se pide listado de todos los actores");
            return Ok(resp);
        }
        
        [HttpGet("nombre")]
        public async Task<ActionResult<IEnumerable<Actor>>> Get(string nombre) //el nombre tal cual
        {
            var resp = await this.unitOfWork.actorRepository.getNombre(nombre);
            this.logger.LogInformation($"se pide actor/actores por nombre: {nombre}");
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
                this.logger.LogWarning($"se pide obtener un actor con id equivocado: {id}");
                return NotFound();
            }
            this.logger.LogInformation($"se pide un actor con id correcto: {id}");
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
