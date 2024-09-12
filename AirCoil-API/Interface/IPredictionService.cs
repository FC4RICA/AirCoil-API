using AirCoil_API.Models;

namespace AirCoil_API.Interface
{
    public interface IPredictionService
    {
        Task HandlePredictionAsync(Job job);
        Task<Result> PredictAsync(ICollection<Image> images);
    }
}
