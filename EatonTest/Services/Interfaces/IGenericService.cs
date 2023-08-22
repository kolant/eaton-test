using EatonTest.Domain.Interfaces.Models;

namespace EatonTest.Services.Interfaces
{
    public interface IGenericService<TBusinessObject> where TBusinessObject : BaseEntity
    {
        Task<TBusinessObject> GetByIdAsync(Guid id);

        Task<List<TBusinessObject>> GetByIdsAsync(List<Guid> ids);

        Task<List<TBusinessObject>> GetAllAsync();

        Task DeleteAsync(Guid id);

        Task<TBusinessObject> CreateAsync(TBusinessObject businessObject);

        Task<TBusinessObject> UpdateAsync(TBusinessObject businessObject);
    }
}
