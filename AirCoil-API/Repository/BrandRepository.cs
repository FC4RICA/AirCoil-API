using AirCoil_API.Data;
using AirCoil_API.Interface;
using AirCoil_API.Models;
using Microsoft.EntityFrameworkCore;

namespace AirCoil_API.Repository
{
    public class BrandRepository : IBrandRepository
    {
        private readonly DataContext _context;

        public BrandRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Brand>> GetBrandsAsync()
        {
            return await _context.Brands.OrderBy(b => b.Id).ToListAsync();
        }

        public async Task<Brand> GetBrandAsync(int id) 
        {
            return await _context.Brands.Where(b => b.Id == id).FirstOrDefaultAsync();
        }

        public async Task<ICollection<Model>> GetModelsByBrandAsync(int id)
        {
            return await _context.Models.Where(m => m.Brand.Id == id).OrderBy(m => m.Id).ToListAsync();
        }

        public async Task<bool> CreateBrandAsync(Brand brand)
        {
            _context.Brands.Add(brand);
            return await SaveAsync();
        }

        public async Task<bool> UpdateBrandAsync(Brand brand)
        {
            _context.Brands.Update(brand);
            return await SaveAsync();
        }

        public async Task<bool> DeleteBrandAsync(Brand brand)
        {
            _context.Brands.Remove(brand);
            return await SaveAsync();
        }

        public async Task<bool> BrandExistsAsync(int id)
        {
            return _context.Brands.Any(b => b.Id == id);
        }
        public async Task<bool> BrandExistsAsync(string name)
        {
            return _context.Brands.Any(b => b.Name.Equals(name));
        }

        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }
    }
}
