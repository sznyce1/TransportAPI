using System.ComponentModel.DataAnnotations;
using TransportAPI.Entities;

namespace TransportAPI.Models
{
    public class UpdateDriverDto
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
    }
}
