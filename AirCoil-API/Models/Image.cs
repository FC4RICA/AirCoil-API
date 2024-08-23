namespace AirCoil_API.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string URL { get; set; } = string.Empty;
        public DateTime DateTime { get; set; } = DateTime.Now;
        public int? JobId { get; set; }

        public Job Job { get; set; }
    }
}
