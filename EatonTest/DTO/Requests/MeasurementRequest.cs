namespace EatonTest.DTO.Requests
{
    public class MeasurementRequest
    {
        public string Key { get; set; }

        public float Value { get; set; }

        public Guid DeviceId { get; set; }
    }
}
