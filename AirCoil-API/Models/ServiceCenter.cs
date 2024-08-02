namespace AirCoil_API.Models
{
    public class ServiceCenter
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Branch> Branches { get; set; }
    }
}
