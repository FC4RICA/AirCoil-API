namespace AirCoil_API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public DateTime DateTime { get; set; }
        public bool isDeleted { get; set; }

        public ICollection<Job> Jobs { get; set; }
    }
}
