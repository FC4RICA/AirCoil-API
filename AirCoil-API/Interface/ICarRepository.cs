using AirCoil_API.Models;

namespace AirCoil_API.Interface
{
    public interface ICarRepository
    {
        Task<ICollection<Car>> GetCarsAsync();
        Task<Car> GetCarAsync(int id);
        Task<ICollection<Job>> GetJobsByCarAsync(int id);
        Task<bool> CreateCarAsync(Car car);
        Task<bool> UpdateCarAsync(Car car);
        Task<bool> DeleteCarAsync(Car car);
        Task<bool> CarExistsAsync(int id);
        Task<bool> CarExistsAsync(Car car);
        Task<bool> SaveAsync();
    }
}
