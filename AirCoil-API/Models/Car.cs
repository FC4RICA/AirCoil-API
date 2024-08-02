namespace AirCoil_API.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string LicensePlate { get; set; }
        public Province Province { get; set; }
        public Model Model { get; set; }
        public DateTime DateTime { get; set; }
        public ICollection<Job> Jobs { get; set; }
    }
}
