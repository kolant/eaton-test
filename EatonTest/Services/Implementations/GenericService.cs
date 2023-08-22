using EatonTest.Domain.Interfaces.Models;
using EatonTest.Infrastructure.Interfaces;
using EatonTest.Services.Interfaces;

namespace EatonTest.Services.Implementations
{
    public abstract class GenericService<TBusinessObject, TRepository> : IGenericService<TBusinessObject>
        where TBusinessObject : BaseEntity
        where TRepository : class, ICrudRepository<TBusinessObject>
    {
        protected GenericService(TRepository repository)
        {
            Repository = repository;
        }

        protected TRepository Repository { get; }

        public virtual async Task<TBusinessObject> CreateAsync(TBusinessObject businessObject)
        {
            return await Repository.CreateAsync(businessObject);
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            await Repository.DeleteAsync(id);
        }

        public virtual async Task<List<TBusinessObject>> GetAllAsync()
        {
            return await Repository.GetAllAsync();
        }

        public virtual async Task<TBusinessObject> GetByIdAsync(Guid id)
        {
            return await Repository.GetByIdAsync(id);
        }

        public virtual async Task<List<TBusinessObject>> GetByIdsAsync(List<Guid> ids)
        {
            return await Repository.GetByIdsAsync(ids);
        }

        public virtual async Task<TBusinessObject> UpdateAsync(TBusinessObject businessObject)
        {
            return await Repository.UpdateAsync(businessObject);
        }
    }
}
