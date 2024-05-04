using System.ComponentModel.DataAnnotations;

namespace TransportAPI.Models
{
    public class CreateDriverDto
    {
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
        [Required]
        [MaxLength(40)]
        public string SecondName { get; set; }
        [Required]
        [MaxLength(4)]
        public string DrivingCategories { get; set; }
        public List<RunDto> Runs { get; set; }
    }
}
