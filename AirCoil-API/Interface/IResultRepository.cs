using AirCoil_API.Models;

namespace AirCoil_API.Interface
{
    public interface IResultRepository
    {
        Task<ICollection<Result>> GetResultsAsync();
        Task<Result?> GetResultAsync(int id);
        Task<ICollection<Job>> GetJobsByResultAsync(int id);
        Task<bool> CreateResultAsync(Result result);
        Task<bool> UpdateResultAsync(Result result);
        Task<bool> DeleteResultAsync(Result result);
        Task<bool> ResultExistsAsync(int id);
        Task<bool> SaveAsync();
    }
}
