namespace AirCoil_API.Models
{
    public class Branch
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int? ServiceCenterId { get; set; }
        public ServiceCenter? ServiceCenter { get; set; }
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
