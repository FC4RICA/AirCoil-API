namespace AirCoil_API.Models
{
    public class Province
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Car> Cars { get; set; }
    }
}
