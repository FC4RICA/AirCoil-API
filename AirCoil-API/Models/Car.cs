namespace AirCoil_API.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string LicensePlate { get; set; } = string.Empty;
        public int? ProvinceId { get; set; }
        public Province? Province { get; set; }
        public int? ModelId { get; set; }
        public Model? Model { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public ICollection<Job> Jobs { get; set; } = new List<Job>();
    }
}
