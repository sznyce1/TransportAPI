namespace TransportAPI.Models
{
    public class RunDto
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public int DriverId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public double Distance { get; set; }
        public double? AverageFuelConsumption { get; set; }
        public string Name { get; set; }
        public string SecondName { get; set; }
        public string Model { get; set; }
        public string RegistrationNumber { get; set; }
        
    }
}
