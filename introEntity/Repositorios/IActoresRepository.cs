using introEntity.Entidades;

namespace introEntity.Repositorios
{
    public interface IActoresRepository : IGenericRepository<Actor>
    {
        public Task<IEnumerable<Actor>> getNombre(string nombre);
    }
}
