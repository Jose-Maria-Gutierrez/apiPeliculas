using introEntity.DTOs;
using introEntity.Entidades;
using Microsoft.EntityFrameworkCore;

namespace introEntity.Repositorios
{
    public class UsuarioRepository : GenericRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Usuario>? getUsuario(UsuarioDTO usuario)
        {
            return await this.context.Usuarios.SingleOrDefaultAsync(u => 
            u.nombreUsuario==usuario.nombreUsuario && u.pwd==usuario.pwd);
        }
    }
}
