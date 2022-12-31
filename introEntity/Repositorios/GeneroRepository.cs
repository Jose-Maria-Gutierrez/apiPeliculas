using introEntity.Entidades;

namespace introEntity.Repositorios
{
    public class GeneroRepository : GenericRepository<Genero>, IGeneroRepository
    {
        public GeneroRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
