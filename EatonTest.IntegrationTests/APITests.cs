
using EatonTest.DTO.Requests;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Text;
using Newtonsoft.Json;
using EatonTest.Domain.Models;

namespace EatonTest.IntegrationTests
{
    [TestClass]
    public class APITests
    {
        private readonly HttpClient _httpClient;

        public APITests()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            _httpClient = webAppFactory.CreateDefaultClient();
        }

        [TestMethod]
        public async Task POST_Devices_Responds_OK()
        {
            var createDeviceRequest = new DeviceRequest() { Name = "test" };
            var response = await PostDeviceAsync(createDeviceRequest);
            var responseJsonString = await response.Content.ReadAsStringAsync();
            var createdDevice = JsonConvert.DeserializeObject<Device>(responseJsonString);

            Assert.IsNotNull(response);
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);
            Assert.AreEqual(createdDevice.Name, createDeviceRequest.Name);
        }

        [TestMethod]
        public async Task POST_Devices_Responds_DuplicateDeviceException()
        {
            var createDeviceRequest = new DeviceRequest() { Name = "duplicateTest" };
            var createDeviceRequest2 = new DeviceRequest() { Name = "duplicateTest" };

            var response = await PostDeviceAsync(createDeviceRequest);
            var response2 = await PostDeviceAsync(createDeviceRequest2);

            Assert.IsNotNull(response);
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);

            Assert.IsNotNull(response2);
            Assert.AreEqual(response2.StatusCode, System.Net.HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public async Task POST_Measurements_Responds_OK()
        {
            // registering new device 
            var createDeviceRequest = new DeviceRequest() { Name = "test" };
            var createDeviceResponse = await PostDeviceAsync(createDeviceRequest);
            var responseJsonString = await createDeviceResponse.Content.ReadAsStringAsync();
            var createdDevice = JsonConvert.DeserializeObject<Device>(responseJsonString);

            // sending device measurement
            HttpResponseMessage measurementResponse = await SendDeviceMeasurement(createdDevice, "key", 1);

            Assert.IsNotNull(measurementResponse);
            Assert.AreEqual(measurementResponse.StatusCode, System.Net.HttpStatusCode.OK);
        }

        [TestMethod]
        public async Task GET_MeasurementsSummary_Responds_OK()
        {
            // registering new device 
            Device device = await RegisterDevice(new DeviceRequest() { Name = "device" });
            Device device2 = await RegisterDevice(new DeviceRequest() { Name = "device2" });
            Device device3 = await RegisterDevice(new DeviceRequest() { Name = "device3" });

            // sending devices measurements
            await SendDeviceMeasurement(device, "key", 1);
            await SendDeviceMeasurement(device, "key", 2);

            await SendDeviceMeasurement(device2, "key", 2);
            await SendDeviceMeasurement(device2, "key", 1);
            await SendDeviceMeasurement(device2, "key", 3);

            // receiving summary
            var response = await _httpClient.GetAsync("measurements/summary");
            var result = await response.Content.ReadAsStringAsync();
            Dictionary<string, int> summary = JsonConvert.DeserializeObject<Dictionary<string, int>>(result);

            Assert.IsNotNull(response);
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);
            Assert.IsNotNull(summary);
            Assert.AreEqual(summary[device.Name], 2);
            Assert.AreEqual(summary[device2.Name], 3);
            Assert.AreEqual(summary[device3.Name], 0);
        }

        private async Task<Device> RegisterDevice(DeviceRequest request)
        {
            var createDeviceResponse = await PostDeviceAsync(request);
            var responseJsonString = await createDeviceResponse.Content.ReadAsStringAsync();
            var createdDevice = JsonConvert.DeserializeObject<Device>(responseJsonString);

            return createdDevice;
        }

        private async Task<HttpResponseMessage> SendDeviceMeasurement(Device? createdDevice, string key, float value)
        {
            var createMeasurmentRequest = new MeasurementRequest() { DeviceId = createdDevice.Id, Key = key, Value = value };
            var data = new StringContent(JsonConvert.SerializeObject(createMeasurmentRequest), Encoding.UTF8, "application/json");
            var measurementResponse = await _httpClient.PostAsync("measurements", data);

            return measurementResponse;
        }

        private async Task<HttpResponseMessage> PostDeviceAsync(DeviceRequest request)
        {
            var data = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            return await _httpClient.PostAsync("devices", data);
        }
    }
}