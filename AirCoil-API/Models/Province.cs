using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace AirCoil_API.Models
{
    public class Province
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<Car> Cars { get; set; } = new List<Car>();
    }
}
