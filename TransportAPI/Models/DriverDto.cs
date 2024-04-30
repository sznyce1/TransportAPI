namespace TransportAPI.Models
{
    public class DriverDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SecondName { get; set; }
        public string DrivingCategories { get; set; }
        public List<RunDto> Runs { get; set; }
    }
}
