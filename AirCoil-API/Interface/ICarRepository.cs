using AirCoil_API.Dto;
using AirCoil_API.Helpers;
using AirCoil_API.Models;

namespace AirCoil_API.Interface
{
    public interface ICarRepository
    {
        Task<ICollection<Car>> GetCarsAsync(CarQueryObject query);
        Task<Car> GetCarAsync(int id);
        Task<Car> GetCarAsync(CreateCarDto car);
        Task<ICollection<Job>> GetJobsByCarAsync(int id, JobQueryObject? query);
        Task<bool> CreateCarAsync(Car car);
        Task<bool> UpdateCarAsync(Car car);
        Task<bool> DeleteCarAsync(Car car);
        Task<bool> CarExistsAsync(int id);
        Task<bool> CarExistsAsync(Car car);
        Task<bool> SaveAsync();
    }
}
