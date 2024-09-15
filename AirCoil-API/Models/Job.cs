namespace AirCoil_API.Models
{
    public class Job
    {
        public int Id { get; set; }
        public int Mileage { get; set; }
        public string? UserId { get; set; }
        public User? User { get; set; }
        public int? CarId { get; set; }
        public Car? Car { get; set; }
        public ICollection<Image> Images { get; set; } = new List<Image>();
        public int? ResultId { get; set; }
        public Result? Result { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
