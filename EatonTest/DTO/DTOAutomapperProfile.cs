using AutoMapper;
using EatonTest.Domain.Models;
using EatonTest.DTO.Requests;
using EatonTest.DTO.Responses;

namespace EatonTest.DTO
{
    public class DTOAutomapperProfile : Profile
    {
        public DTOAutomapperProfile()
        {
            CreateMap<DeviceRequest, Device>();
            CreateMap<Device, DeviceResponse>();

            CreateMap<MeasurementRequest, Measurement>();
        }
    }
}
