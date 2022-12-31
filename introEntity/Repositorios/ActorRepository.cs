using introEntity.Entidades;
using Microsoft.EntityFrameworkCore;

namespace introEntity.Repositorios
{
    public class ActorRepository : GenericRepository<Actor>, IActoresRepository
    {
        public ActorRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Actor>> getNombre(string nombre)
        {
            return await this.context.Actores.Where(a=>a.Nombre.Contains(nombre)).ToListAsync();
        }
    }
}
