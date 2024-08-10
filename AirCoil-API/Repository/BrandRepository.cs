using AirCoil_API.Data;
using AirCoil_API.Interface;
using AirCoil_API.Models;

namespace AirCoil_API.Repository
{
    public class BrandRepository : IBrandRepository
    {
        private readonly DataContext _context;

        public BrandRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Brand> GetBrands()
        {
            return _context.Brands.OrderBy(b => b.Id).ToList();
        }

        public Brand GetBrand(int id) 
        {
            return _context.Brands.Where(b => b.Id == id).FirstOrDefault();
        }

        public ICollection<Model> GetModelsByBrand(int id)
        {
            return _context.Models.Where(m => m.Id == id).ToList();
        }

        public bool CreateBrand(Brand brand)
        {
            _context.Brands.Add(brand);
            return Save();
        }

        public bool UpdateBrand(Brand brand)
        {
            _context.Brands.Update(brand);
            return Save();
        }

        public bool DeleteBrand(Brand brand)
        {
            _context.Brands.Remove(brand);
            return Save();
        }

        public bool BrandExists(int id)
        {
            return _context.Brands.Any(b => b.Id == id);
        }
        public bool BrandExists(string name)
        {
            return _context.Brands.Any(b => b.Name.Equals(name));
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }
    }
}
