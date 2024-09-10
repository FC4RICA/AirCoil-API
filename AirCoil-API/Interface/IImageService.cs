using AirCoil_API.Models;

namespace AirCoil_API.Interface
{
    public interface IImageService
    {
        Task<String> GetImageUrlAsync(int id, HttpRequest request);
        Task<Image> GetImageAsync(int id);
        Task<ICollection<Image>> GetImagesAsync();
        Task<Image> CreateImageAsync(IFormFile file);
        Task<bool> DeleteImageAsync(Image image);
    }
}
