using introEntity.DTOs;
using introEntity.Entidades;

namespace introEntity.Repositorios
{
    public interface IPeliculaRepository : IGenericRepository<Pelicula>
    {
        Task<Pelicula>? getPelicula(int id);
        Task<int> eliminarPorId(int id);
        Task agregarPeliculaCascada(Pelicula pel);
    }
}
