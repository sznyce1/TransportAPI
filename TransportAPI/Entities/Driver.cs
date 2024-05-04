namespace TransportAPI.Entities
{
    public class Driver
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SecondName { get; set; }
        public string DrivingCategories { get; set; }
        public virtual List<Run>? Runs { get; set; }
    }
}
