namespace AirCoil_API.Dto
{
    public class BrandDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class CreateBrandDto
    {
        public required string Name { get; set; }
    }
}
