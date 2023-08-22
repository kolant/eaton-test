using EatonTest.Domain.Models;

namespace EatonTest.Infrastructure.Interfaces
{
    public interface IMeasurementRepository : ICrudRepository<Measurement>
    {
        Task<Dictionary<string, int>> GetDeviceMeasurementsSummaryAsync();

    }
}
