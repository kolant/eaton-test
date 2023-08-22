using EatonTest.Domain.Interfaces.Models;

namespace EatonTest.Domain.Models
{
    public class Device : BaseEntity
    {
        public string Name { get; set; }

        public IEnumerable<Measurement> Measurments { get; set; }
    }
}
