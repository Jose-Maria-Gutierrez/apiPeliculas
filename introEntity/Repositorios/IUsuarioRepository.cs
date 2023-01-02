using introEntity.DTOs;
using introEntity.Entidades;

namespace introEntity.Repositorios
{
    public interface IUsuarioRepository : IGenericRepository<Usuario>
    {
        public Task<Usuario>? getUsuario(UsuarioDTO usuario);

    }
}
