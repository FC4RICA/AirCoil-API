using AirCoil_API.Data;
using AirCoil_API.Interface;
using AirCoil_API.Models;

namespace AirCoil_API.Repository
{
    public class ProvinceRepository : IProvinceRepository
    {
        private readonly DataContext _context;
        public ProvinceRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Province> GetProvices()
        {
            return _context.Provinces.ToList();
       }

        public bool ProvinceExists(string name)
        {
            return _context.Provinces.Any(p =>  p.Id.Equals(name));
        }
    }
}
