using System.ComponentModel;

namespace TransportAPI.Entities
{
    public class Car
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string RegistrationNumber { get; set; }
        public string CarType { get; set; }
        public virtual List<Run>? Runs { get; set; }
    }
}
