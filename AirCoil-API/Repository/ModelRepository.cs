using AirCoil_API.Data;
using AirCoil_API.Interface;
using AirCoil_API.Models;
using Microsoft.EntityFrameworkCore;

namespace AirCoil_API.Repository
{
    public class ModelRepository : IModelRepository
    {
        private readonly DataContext _context;

        public ModelRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Model>> GetModelsAsync()
        {
            return await _context.Models.Include(m => m.Brand).OrderBy(m => m.Id).ToListAsync();
        }

        public async Task<Model> GetModelAsync(int id)
        {
            return await _context.Models.Include(m => m.Brand).Where(m => m.Id == id).FirstOrDefaultAsync();
        }
        public async Task<Model> GetModelAsync(string name)
        {
            return await _context.Models.Include(m => m.Brand).Where(m => m.Name.Equals(name)).FirstOrDefaultAsync();
        }

        public async Task<ICollection<Car>> GetCarsByModelAsync(int id)
        {
            return await _context.Cars.Where(c => c.Model.Id == id).OrderBy(c => c.Id).ToListAsync();
        }

        public async Task<bool> CreateModelAsync(Model model)
        {
            _context.Models.Add(model);
            return await SaveAsync();
        }

        public async Task<bool> UpdateModelAsync(Model model)
        {
            _context.Models.Update(model);
            return await SaveAsync();
        }

        public async Task<bool> DeleteModelAsync(Model model)
        {
            _context.Models.Remove(model);
            return await SaveAsync();
        }

        public async Task<bool> ModelExistsAsync(int id)
        {
            return await _context.Models.AnyAsync(m => m.Id == id);
        }

        public async Task<bool> ModelExistsAsync(string name)
        {
            return await _context.Models.AnyAsync(m => m.Name.Equals(name));
        }

        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }
    }
}
