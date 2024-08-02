namespace AirCoil_API.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string URL { get; set; }
        public DateTime DateTime { get; set; }

        public Job Job { get; set; }
    }
}
