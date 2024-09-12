using AirCoil_API.Interface;
using AirCoil_API.Models;

namespace AirCoil_API.Service
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository _imageRepository;

        public ImageService(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        public async Task<string> GetImageUrlAsync(int id, HttpRequest request)
        {
            var image = await _imageRepository.GetImageAsync(id);
            if (image == null) 
            {
                return null;
            }

            var baseUrl = $"{request.Scheme}://{request.Host}";
            var imageUrl = $"{baseUrl}/images/{image.FileName}";

            return imageUrl;
        }

        public async Task<ICollection<Image>> CreateImageAsync(IFormFileCollection files)
        {
            var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            var images = new List<Image>();

            foreach (var file in files)
            {
                var fileName = Path.GetRandomFileName() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(directoryPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                images.Add(new Image
                {
                    FileName = fileName,
                    FilePath = filePath
                });
            }


            return await _imageRepository.CreateImageAsync(images);
        }

        public async Task<bool> DeleteImageAsync(Image image)
        {
            var filePath = image.FilePath;
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            return await _imageRepository.DeleteImageAsync(image);
        }

        public async Task<Image> GetImageAsync(int id)
        {
            return await _imageRepository.GetImageAsync(id);
        }

        public async Task<ICollection<Image>> GetImagesAsync()
        {
            return await _imageRepository.GetImagesAsync();
        }

    }
}
