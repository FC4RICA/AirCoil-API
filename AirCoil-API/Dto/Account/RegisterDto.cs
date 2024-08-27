using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace AirCoil_API.Dto.Account
{
    public class RegisterDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
