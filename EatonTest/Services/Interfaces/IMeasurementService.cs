using EatonTest.Domain.Models;
using EatonTest.DTO.Responses;

namespace EatonTest.Services.Interfaces
{
    public interface IMeasurementService : IGenericService<Measurement>
    {
        Task<Dictionary<string, int>> GetDeviceMeasurementsSummaryAsync();
    }
}
