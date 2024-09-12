using AirCoil_API.Models;

namespace AirCoil_API.Interface
{
    public interface IImageRepository
    {
        Task<ICollection<Image>> GetImagesAsync();
        Task<Image> GetImageAsync(int id);
        Task<ICollection<Image>> CreateImageAsync(ICollection<Image> images);
        Task<bool> DeleteImageAsync(Image image);
        Task<bool> SaveAsync();
    }
}
