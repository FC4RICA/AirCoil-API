using AirCoil_API.Helpers;
using AirCoil_API.Models;

namespace AirCoil_API.Interface
{
    public interface IJobRepository
    {
        Task<ICollection<Job>> GetJobsAsync(JobQueryObject query);
        Task<Job> GetJobAsync(int id);
        Task<bool> CreateJobAsync(Job job);
        Task<bool> UpdateJobAsync(Job job);
        Task<bool> DeleteJobAsync(Job job);
        Task<bool> SaveAsync();
    }
}
