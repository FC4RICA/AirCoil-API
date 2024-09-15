namespace AirCoil_API.Dto.Account
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public BranchDto Branch { get; set; }
    }
}
