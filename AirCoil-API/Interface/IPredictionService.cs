using AirCoil_API.Models;

namespace AirCoil_API.Interface
{
    public interface IPredictionService
    {
        Task<bool> HandlePredictionAsync(Job job, HttpRequest request);
        Task<Result> PredictAsync(ICollection<string> imagesUrl);
    }
}
