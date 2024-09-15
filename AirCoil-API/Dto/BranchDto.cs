namespace AirCoil_API.Dto
{
    public class BranchDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ServiceCenterDto? ServiceCenter { get; set; }
    }

    public class CreateBranchDto
    {
        public string Name { get; set; }
    }
}
