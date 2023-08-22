using EatonTest.Domain.Models;
using EatonTest.Infrastructure.Interfaces;
using EatonTest.Services.Interfaces;

namespace EatonTest.Services.Implementations
{
    public class MeasurementService : GenericService<Measurement, IMeasurementRepository>, IMeasurementService
    {
        public MeasurementService(IMeasurementRepository repository) : base(repository)
        {
        }

        public async Task<Dictionary<string, int>> GetDeviceMeasurementsSummaryAsync()
        {
            return await Repository.GetDeviceMeasurementsSummaryAsync();
        }
    }
}
