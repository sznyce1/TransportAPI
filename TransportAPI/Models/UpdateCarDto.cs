using System.ComponentModel.DataAnnotations;

namespace TransportAPI.Models
{
    public class UpdateCarDto
    {
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
