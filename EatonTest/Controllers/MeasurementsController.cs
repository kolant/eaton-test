using AutoMapper;
using EatonTest.Domain.Models;
using EatonTest.DTO.Requests;
using EatonTest.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EatonTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MeasurementsController : Controller
    {
        private readonly ILogger<MeasurementsController> _logger;
        private readonly IMapper _mapper;
        private readonly IMeasurementService _measurementService;

        public MeasurementsController(
            ILogger<MeasurementsController> logger,
            IMapper mapper,
            IMeasurementService measurementService)
        {
            _logger = logger;
            _mapper = mapper;
            _measurementService = measurementService;
        }

        [HttpPost]
        public async Task<IActionResult> Index(MeasurementRequest request)
        {
            _logger.LogInformation($"Saving measurement from device id {request.DeviceId}...");
            await _measurementService.CreateAsync(_mapper.Map<Measurement>(request));
            _logger.LogInformation($"The measurement from device id {request.DeviceId} sucessfully saved.");

            return Ok();
        }

        [HttpGet("summary")]
        public async Task<IActionResult> GetMeasurments()
        {
            _logger.LogInformation($"Receiving measurements count summary grouped by device...");
            var summary = await _measurementService.GetDeviceMeasurementsSummaryAsync();
            _logger.LogInformation($"Summary received");

            return Ok(summary);
        }
    }
}
