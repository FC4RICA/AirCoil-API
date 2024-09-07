namespace AirCoil_API.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public DateTime UploadDate { get; set; } = DateTime.Now;
        public int? JobId { get; set; }

        public Job? Job { get; set; }
    }
}
