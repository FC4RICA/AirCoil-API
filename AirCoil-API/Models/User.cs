using Microsoft.AspNetCore.Identity;

namespace AirCoil_API.Models
{
    public class User : IdentityUser
    {
        public int? BranchId { get; set; }
        public Branch? Branch { get; set; }
        public ICollection<Job> Jobs { get; set; } = new List<Job>();
    }
}
