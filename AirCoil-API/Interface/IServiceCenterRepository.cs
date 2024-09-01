using AirCoil_API.Models;

namespace AirCoil_API.Interface
{
    public interface IServiceCenterRepository
    {
        Task<ICollection<ServiceCenter>> GetServiceCentersAsync();
        Task<ServiceCenter> GetServiceCenterAsync(int id);
        Task<ICollection<Branch>> GetBranchesByServiceCenterAsync(int id);
        Task<bool> CreateServiceCenterAsync(ServiceCenter serviceCenter);
        Task<bool> UpdateServiceCenterAsync(ServiceCenter serviceCenter);
        Task<bool> DeleteServiceCenterAsync(ServiceCenter serviceCenter);
        Task<bool> ServiceCenterExistsAsync(int id);
        Task<bool> SaveAsync();
    }
}
