namespace AirCoil_API.Dto
{
    public class ProvinceDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class CreateProvinceDto
    {
        public required string Name { get; set; }
    }
}
