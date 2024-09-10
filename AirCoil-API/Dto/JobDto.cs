using AirCoil_API.Dto.Image;
using AirCoil_API.Models;
using System.ComponentModel.DataAnnotations;

namespace AirCoil_API.Dto
{
    public class JobDto
    {
        public int Id { get; set; }
        public int Mileage { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public ResultDto? Result { get; set; }
        public CarDto? Car { get; set; }
        public ICollection<ImageUrlDto> Images { get; set; } = new List<ImageUrlDto>();
    }

    public class CreateJobDto
    {
        [Required]
        public int Mileage { get; set; }
        [Required]
        public CreateCarDto Car { get; set; }
    }
}
