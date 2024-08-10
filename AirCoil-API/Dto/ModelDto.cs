namespace AirCoil_API.Dto
{
    public class ModelDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class CreateModelDto
    {
        public required string Name { get; set; }
        public required int BrandId { get; set; }
    }
}
