using AirCoil_API.Dto;
using AirCoil_API.Dto.Image;
using AirCoil_API.Interface;
using AirCoil_API.Models;

namespace AirCoil_API.Service
{
    public class PredictionService : IPredictionService
    {
        private readonly IJobRepository _jobRepository;
        private readonly IImageService _imageService;
        private readonly IResultRepository _resultRepository;
        private readonly ILogger<PredictionService> _logger;
        private readonly HttpClient _httpClient;

        public PredictionService(IJobRepository jobRepository, IImageService imageService, ILogger<PredictionService> logger, HttpClient httpClient, IResultRepository resultRepository)
        {
            _jobRepository = jobRepository;
            _imageService = imageService;
            _logger = logger;
            _httpClient = httpClient;
            _resultRepository = resultRepository;
        }

        public async Task<bool> HandlePredictionAsync(Job job, HttpRequest request)
        {
            try
            {
                if (job.Images == null || !job.Images.Any())
                {
                    _logger.LogError("No images found for job ID {JobId}", job.Id);
                    throw new Exception($"No images found for job ID {job.Id}");
                }

                var imageUrl = await _imageService.GetImageUrlAsync(job.Images.First().Id, request);
                job.Result = await PredictAsync(new List<string> { imageUrl });
                return await _jobRepository.UpdateJobAsync(job);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during prediction for job ID {JobId}", job.Id);
                return false;
            }
        }

        public async Task<Result> PredictAsync(ICollection<string> imagesUrl)
        {
            var content = JsonContent.Create(new { urls = imagesUrl });

            try
            {
                var response = await _httpClient.PostAsync("predict", content);

                if (response.IsSuccessStatusCode)
                {
                    var predictResult = await response.Content.ReadFromJsonAsync<PredictResult>();
                    return await _resultRepository.GetResultAsync(predictResult);
                }
                else
                {
                    _logger.LogError("Prediction API returned a non-success status code: {StatusCode}", response.StatusCode);
                    throw new Exception("Prediction API failed.");
                }
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "Error occurred while calling the predict API");
                throw;
            }
        }
    }
}
