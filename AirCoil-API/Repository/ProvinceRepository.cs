using AirCoil_API.Data;
using AirCoil_API.Interface;
using AirCoil_API.Models;
using Microsoft.AspNetCore.Components.Web;

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

        public Province GetProvince(int id)
        {
            return _context.Provinces.Where(p => p.Id == id).FirstOrDefault();
        }

        public bool CreateProvince(Province province)
        {
            _context.Add(province);
            return Save();
        }

        public bool DeleteProvince(Province province)
        {
            _context.Provinces.Remove(province);
            return Save();
        }

        public ICollection<Car> GetCarsByProvince(int id)
        {
            return _context.Cars.Where(c => c.Province.Id == id).ToList();
        }

        public bool ProvinceExists(string name)
        {
            return _context.Provinces.Any(p => p.Name.Equals(name));
        }
        public bool ProvinceExists(int id)
        {
            return _context.Provinces.Any(p => p.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }
    }
}
