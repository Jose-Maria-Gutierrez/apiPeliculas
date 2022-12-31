using Microsoft.AspNetCore.Mvc;

namespace introEntity.Repositorios
{
    public interface IGenericRepository<T> where T : class
    {
        Task add(T entity);
        Task agregarVarios(T[] entities);
        void delete(T entity);
        void update(T entity);
        Task<T>? getById(int id);
        Task<IEnumerable<T>> getAll();
    }
}
