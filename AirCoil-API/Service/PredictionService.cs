using AirCoil_API.Dto;
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

        public PredictionService(IJobRepository jobRepository, IImageService imageService, ILogger<PredictionService> logger, HttpClient httpClient)
        {
            _jobRepository = jobRepository;
            _imageService = imageService;
            _logger = logger;
            _httpClient = httpClient;
        }

        public async Task HandlePredictionAsync(Job job)
        {
            try
            {
                job.Result = await PredictAsync(job.Images);
                await _jobRepository.UpdateJobAsync(job);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during prediction for job ID {JobId}", job.Id);
            }
        }

        public async Task<Result> PredictAsync(ICollection<Image> images)
        {
            var content = new MultipartFormDataContent();
            foreach (var image in images)
            {
                var fileContent = new ByteArrayContent(await File.ReadAllBytesAsync(image.FilePath));
                content.Add(fileContent, "files", image.FileName);
            }

            try
            {
                var response = await _httpClient.PostAsync("/predict", content);

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
