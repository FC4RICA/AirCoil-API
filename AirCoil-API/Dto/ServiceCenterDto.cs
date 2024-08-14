namespace AirCoil_API.Dto
{
    public class ServiceCenterDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class CreateServiceCenterDto
    {
        public string Name { get; set; }
    }
}
