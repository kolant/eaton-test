using EatonTest.Domain.Models;
using EatonTest.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EatonTest.Infrastructure.Implementations
{
    public class DeviceRepository : CrudRepository<Device>, IDeviceRepository
    {
        public DeviceRepository(ApiContext context) : base(context)
        {
        }

        public async Task<Device> GetByNameAsync(string name)
        {
            return await Context.Devices.FirstOrDefaultAsync(x => x.Name == name);
        }
    }
}
