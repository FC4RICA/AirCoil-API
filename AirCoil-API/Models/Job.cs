namespace AirCoil_API.Models
{
    public class Job
    {
        public int Id { get; set; }
        public User User { get; set; }
        public Car Car { get; set; }
        public int Mileage { get; set; }
        public ICollection<Image> Images { get; set; }
        public Result Result { get; set; }
        public DateTime DateTime { get; set; }
    }
}
