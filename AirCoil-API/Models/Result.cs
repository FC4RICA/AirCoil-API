namespace AirCoil_API.Models
{
    public class Result
    {
        public int Id { get; set; }
        public int EDLLevel { get; set; }
        public string description { get; set; } = string.Empty;
        public ICollection<Job> Jobs { get; set; } = new List<Job>();
    }
}
