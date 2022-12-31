using introEntity.UoW;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace introEntity.Repositorios

{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ApplicationDbContext context;

        public GenericRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task add(T entity)
        {
            await this.context.Set<T>().AddAsync(entity);
        }

        public async Task agregarVarios(T[] entities)
        {
            await this.context.Set<T>().AddRangeAsync(entities);
        }

        public void delete(T entity)
        {
            this.context.Set<T>().Remove(entity);
        }

        public async Task<IEnumerable<T>> getAll()
        {
            return await this.context.Set<T>().ToListAsync();
        }

        public async Task<T>? getById(int id)
        {
            return await this.context.Set<T>().FindAsync(id);
        }

        public void update(T entity)
        {
            this.context.Update(entity);
        }
    }
}
