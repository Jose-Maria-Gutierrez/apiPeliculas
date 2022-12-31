using introEntity.Entidades;

namespace introEntity.Repositorios
{
    public class ComentarioRepository : GenericRepository<Comentario>, IComentarioRepository
    {
        public ComentarioRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
