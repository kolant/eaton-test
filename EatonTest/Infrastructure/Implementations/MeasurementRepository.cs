using EatonTest.Domain.Models;
using EatonTest.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EatonTest.Infrastructure.Implementations
{
    public class MeasurementRepository : CrudRepository<Measurement>, IMeasurementRepository
    {
        public MeasurementRepository(ApiContext context) : base(context)
        {
        }

        public async Task<Dictionary<string, int>> GetDeviceMeasurementsSummaryAsync()
        {
            return await Context.Devices
                .GroupJoin(
                    Context.Measurements,
                    d => d.Id,
                    m => m.DeviceId,
                    (d, m) => new {
                        deviceName = d.Name,
                        measurementsCount = Context.Measurements.Where(x => x.DeviceId == d.Id).Count(),
                    }
                ).ToDictionaryAsync(o => o.deviceName, o => o.measurementsCount);
        }
    }
}
