﻿using System.ComponentModel.DataAnnotations;

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
        public int CarId { get; set; }
        [Required]
        public int DriverId { get; set; }
    }
}
