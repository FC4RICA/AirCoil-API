using AirCoil_API.Data;
using AirCoil_API.Interface;
using AirCoil_API.Models;
using Microsoft.EntityFrameworkCore;

namespace AirCoil_API.Repository
{
    public class ImageRepository : IImageRepository
    {
        private readonly DataContext _context;

        public ImageRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Image>> GetImagesAsync()
        {
            return await _context.Images.OrderBy(i => i.Id).ToListAsync();
        }


        public async Task<Image> GetImageAsync(int id)
        {
            return await _context.Images.Where(i => i.Id == id).FirstOrDefaultAsync();
        }
        

        public async Task<bool> CreateImageAsync(Image image)
        {
            await _context.Images.AddAsync(image);
            return await SaveAsync();
        }

        public async Task<bool> DeleteImageAsync(Image image)
        {
            _context.Images.Remove(image);
            return await SaveAsync();
        }

        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }
    }
}
