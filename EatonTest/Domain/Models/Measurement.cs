using EatonTest.Domain.Interfaces.Models;

namespace EatonTest.Domain.Models
{
    public class Measurement : BaseEntity
    {
        public string Key { get; set; }

        public float Value { get; set; }

        public DateTime DateTime { get; set; }

        public Guid DeviceId { get; set; }

        public Device Device { get; set; }
    }
}
