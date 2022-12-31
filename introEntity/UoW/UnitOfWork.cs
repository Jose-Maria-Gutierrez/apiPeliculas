using introEntity.Repositorios;

namespace introEntity.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        public ApplicationDbContext context { get; }
        public IPeliculaRepository peliculaRepository { get; }
        public IGeneroRepository generoRepository { get; }
        public IComentarioRepository comentarioRepository { get; }
        public IActoresRepository actorRepository { get; }
       
        public UnitOfWork(ApplicationDbContext context, IPeliculaRepository peliculaRepo, 
            IGeneroRepository generoRepository,IComentarioRepository comentarioRepository,IActoresRepository actoresRepository)
        {
            this.context = context;
            this.peliculaRepository = peliculaRepo;
            this.generoRepository = generoRepository;
            this.comentarioRepository = comentarioRepository;
            this.actorRepository = actoresRepository;
        }

        public void Dispose()
        {
            this.context.Dispose();
        }

        public async Task saveChanges()
        {
            await this.context.SaveChangesAsync();
        }
    }
}
