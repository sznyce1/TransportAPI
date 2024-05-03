namespace TransportAPI.Models
{
    public class UpdateRunDto
    {
        public int CarId { get; set; }
        public int DriverId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public double Distance { get; set; }
        public double? AverageFuelConsumption { get; set; }
    }
}
