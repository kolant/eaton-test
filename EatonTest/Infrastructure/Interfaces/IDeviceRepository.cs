using EatonTest.Domain.Models;

namespace EatonTest.Infrastructure.Interfaces
{
    public interface IDeviceRepository : ICrudRepository<Device>
    {
        public Task<Device> GetByNameAsync(string name);
    }
}
