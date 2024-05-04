namespace TransportAPI.Entities
{
    public class Run
    {
        public int Id { get; set; }
        public int? CarId { get; set; }
        public int? DriverId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public double Distance { get; set; }
        public double? AverageFuelConsumption { get; set; }
        public virtual Driver Driver { get; set; }
        public virtual Car Car { get; set; }
    }
}
