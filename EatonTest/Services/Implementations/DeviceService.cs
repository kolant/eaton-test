using EatonTest.Domain.Models;
using EatonTest.Infrastructure.Interfaces;
using EatonTest.Services.Exceptions;
using EatonTest.Services.Interfaces;

namespace EatonTest.Services.Implementations
{
    public class DeviceService : GenericService<Device, IDeviceRepository>, IDeviceService
    {
        public DeviceService(IDeviceRepository repository) : base(repository)
        {
        }

        public async override Task<Device> CreateAsync(Device device)
        {
            var existedDevice = await Repository.GetByNameAsync(device.Name);

            if (existedDevice != null)
            {
                throw new DuplicateDeviceException(device.Name);
            }

            return await base.CreateAsync(device);
        }
    }
}
