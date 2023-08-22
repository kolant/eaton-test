using EatonTest.Domain.Interfaces;

namespace EatonTest.Infrastructure.Interfaces
{
    public interface ICrudRepository<T> where T : class, IBaseEntity
    {
        IEnumerable<T> GetAll();

        Task<List<T>> GetAllAsync();

        T GetById(Guid id);

        Task<T> GetByIdAsync(Guid id);

        Task<List<T>> GetByIdsAsync(List<Guid> ids);

        T Create(T entity);

        Task<T> CreateAsync(T entity);

        Task<IEnumerable<T>> CreateBatchAsync(IEnumerable<T> entitiesToCreate);

        T Update(T entity);

        Task<T> UpdateAsync(T entity);

        void Delete(Guid id);

        Task DeleteAsync(Guid id);
    }
}