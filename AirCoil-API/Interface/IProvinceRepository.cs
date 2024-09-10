using AirCoil_API.Models;

namespace AirCoil_API.Interface
{
    public interface IProvinceRepository
    {
        Task<ICollection<Province>> GetProvicesAsync();
        Task<Province> GetProvinceAsync(int id);
        Task<Province> GetProvinceAsync(string name);
        Task<ICollection<Car>> GetCarsByProvinceAsync(int id);
        Task<bool> CreateProvinceAsync(Province province);
        Task<bool> UpdateProvinceAsync(Province province);
        Task<bool> DeleteProvinceAsync(Province province);
        Task<bool> ProvinceExistsAsync(string name);
        Task<bool> ProvinceExistsAsync(int id);
        Task<bool> SaveAsync();
    }
}
