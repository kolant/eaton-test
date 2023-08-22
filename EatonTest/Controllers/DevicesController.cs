using AutoMapper;
using EatonTest.Domain.Models;
using EatonTest.DTO.Requests;
using EatonTest.DTO.Responses;
using EatonTest.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EatonTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DevicesController : Controller
    {
        private readonly ILogger<DevicesController> _logger;
        private readonly IMapper _mapper;
        private readonly IDeviceService _deviceService;

        public DevicesController(
            ILogger<DevicesController> logger,
            IMapper mapper,
            IDeviceService deviceService)
        {
            _logger = logger;
            _mapper = mapper;
            _deviceService = deviceService;
        }

        [HttpPost]
        public async Task<IActionResult> Register(DeviceRequest device)
        {
            _logger.LogInformation($"Registering the new device {device.Name}...");
            var createdDevice = await _deviceService.CreateAsync(_mapper.Map<Device>(device));
            _logger.LogInformation($"The new device {device.Name} sucessfully registered.");

            return Ok(_mapper.Map<DeviceResponse>(createdDevice));
        }
    }
}
