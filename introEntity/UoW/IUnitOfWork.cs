using introEntity.Repositorios;

namespace introEntity.UoW
{
    public interface IUnitOfWork : IDisposable
    {
        IPeliculaRepository peliculaRepository { get; }
        IGeneroRepository generoRepository { get; }
        IComentarioRepository comentarioRepository { get; }
        IActoresRepository actorRepository { get; }
        IUsuarioRepository usuarioRepository { get; }
        Task saveChanges();
    }
}
