using Microsoft.Identity.Client;

namespace AirCoil_API.Dto
{
    public class CarDto
    {
        public int Id { get; set; }
        public string LicensePlate { get; set; }
        public ProvinceDto Province { get; set; }
        public ModelDto Model { get; set; }
        public DateTime DateTime { get; set; }
    }
}
