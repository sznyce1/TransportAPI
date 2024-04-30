namespace TransportAPI.Models
{
    public class CarDto
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string RegistrationNumber { get; set; }
        public string CarType { get; set; }
        public List<RunDto> Runs { get; set; }
    }
}
