using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace AirCoil_API.Dto
{
    public class CarDto
    {
        public int Id { get; set; }
        public string LicensePlate { get; set; }
        public ProvinceDto? Province { get; set; }
        public ModelDto? Model { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class CreateCarDto
    {
        [Required]
        public string LicensePlate { get; set; }
        [Required]
        public string Province { get; set; }
        [Required]
        public string Model { get; set; }
    }
}
