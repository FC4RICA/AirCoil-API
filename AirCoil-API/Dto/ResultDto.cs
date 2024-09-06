namespace AirCoil_API.Dto
{
    public class ResultDto
    {
        public int Id { get; set; }
        public int EDLLevel { get; set; }
        public string Description { get; set; }
    }

    public class CreateResultDto
    {
        public int EDLLevel { get; set; }
        public string Description { get; set; }
    }
}
