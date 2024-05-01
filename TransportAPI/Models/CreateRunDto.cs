using System.ComponentModel.DataAnnotations;

namespace TransportAPI.Models
{
    public class CreateRunDto
    {
        [Required]
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        [Required]
        [Range(0,99999)]
        public double Distance { get; set; }
        [Range(0, 100)]
        public double? AverageFuelConsumption { get; set; }
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
        [Required]
        [MaxLength(40)]
        public string SecondName { get; set; }
        [Required]
        [MaxLength(4)]
        public string DrivingCategories { get; set; }
        [Required]
        [MaxLength(20)]
        public string Model { get; set; }
        [Required]
        [MaxLength(9)]
        public string RegistrationNumber { get; set; }
        [Required]
        [MaxLength(20)]
        public string CarType { get; set; }
    }
}
