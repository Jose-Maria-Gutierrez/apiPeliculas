using introEntity.Entidades;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace introEntity
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); //aplica todas las configs
        }

        public DbSet<Genero> Generos => Set<Genero>(); //con set creo el dbset de la clase genero
        public DbSet<Actor> Actores => Set<Actor>(); //con set creo el dbset de la clase genero
        public DbSet<Pelicula> Peliculas => Set<Pelicula>(); //con set creo el dbset de la clase genero
        public DbSet<Comentario> Comentarios => Set<Comentario>(); //con set creo el dbset de la clase genero
        public DbSet<PeliculaActor> PeliculasActores => Set<PeliculaActor>(); //con set creo el dbset de la clase 

    }
}
