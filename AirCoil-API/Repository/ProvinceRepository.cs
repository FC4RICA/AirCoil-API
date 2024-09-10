using AirCoil_API.Data;
using AirCoil_API.Interface;
using AirCoil_API.Models;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;

namespace AirCoil_API.Repository
{
    public class ProvinceRepository : IProvinceRepository
    {
        private readonly DataContext _context;
        public ProvinceRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Province>> GetProvicesAsync()
        {
            return await _context.Provinces.OrderBy(p => p.Id).ToListAsync();
        }

        public async Task<Province> GetProvinceAsync(int id)
        {
            return await _context.Provinces.Where(p => p.Id == id).FirstOrDefaultAsync(); ;
        }

        public async Task<Province> GetProvinceAsync(string name)
        {
            return await _context.Provinces.Where(p => p.Name.Equals(name)).FirstOrDefaultAsync(); ;
        }

        public async Task<bool> CreateProvinceAsync(Province province)
        {
            _context.Provinces.Add(province);
            return await SaveAsync();
        }

        public async Task<bool> UpdateProvinceAsync(Province province)
        {
            _context.Provinces.Update(province);
            return await SaveAsync();
        }

        public async Task<bool> DeleteProvinceAsync(Province province)
        {
            _context.Provinces.Remove(province);
            return await SaveAsync();
        }

        public async Task<ICollection<Car>> GetCarsByProvinceAsync(int id)
        {
            return await _context.Cars.Where(c => c.Province.Id == id).ToListAsync();
        }

        public async Task<bool> ProvinceExistsAsync(string name)
        {
            return await _context.Provinces.AnyAsync(p => p.Name.Equals(name));
        }
        public async Task<bool> ProvinceExistsAsync(int id)
        {
            return await _context.Provinces.AnyAsync(p => p.Id == id);
        }

        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }
    }
}
