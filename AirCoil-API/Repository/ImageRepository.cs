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
            return await _context.Images.FindAsync(id);
        }
        

        public async Task<ICollection<Image>> CreateImageAsync(ICollection<Image> images)
        {
            await _context.Images.AddRangeAsync(images);

            if (!await SaveAsync())
            {
                return null;
            }

            return images;
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
