using Microsoft.Identity.Client;

namespace AirCoil_API.Dto
{
    public class CarDto
    {
        public int Id { get; set; }
        public string LicensePlate { get; set; }
        public DateTime DateTime { get; set; }
    }

    public class CreateCarDto
    {
        public required string LicensePlate { get; set; }
    }
}
