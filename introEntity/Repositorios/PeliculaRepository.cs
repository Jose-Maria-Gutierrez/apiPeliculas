using AutoMapper;
using introEntity.DTOs;
using introEntity.Entidades;
using introEntity.UoW;
using Microsoft.EntityFrameworkCore;

namespace introEntity.Repositorios
{
    public class PeliculaRepository : GenericRepository<Pelicula>, IPeliculaRepository
    {
        public PeliculaRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task agregarPeliculaCascada(Pelicula pelicula)
        {
            if (pelicula.Generos is not null)
            {
                foreach (var genero in pelicula.Generos)
                {
                    this.context.Entry(genero).State = EntityState.Unchanged; //que no cree nuevos generos pq no se usa tabla intermedia
                }
            }
            if (pelicula.PeliculasActores is not null)
            {
                for (int i = 0; i < pelicula.PeliculasActores.Count; i++)
                {
                    pelicula.PeliculasActores[i].Orden = i + 1;
                }
            }
            await this.context.Peliculas.AddAsync(pelicula);
        }

        public async Task<int> eliminarPorId(int id)
        {
            return await context.Peliculas.Where(g => g.Id == id).ExecuteDeleteAsync();
        }

        public async Task<Pelicula>? getPelicula(int id)
        {
            return await base.context.Peliculas.Include(p => p.Comentarios).Include(p => p.Generos)
                .Include(p => p.PeliculasActores.OrderBy(pa => pa.Orden))
                .ThenInclude(pa => pa.Actor) //desde PeliculasActores a Actor
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
