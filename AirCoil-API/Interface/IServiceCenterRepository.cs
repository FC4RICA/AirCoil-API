using AirCoil_API.Models;

namespace AirCoil_API.Interface
{
    public interface IServiceCenterRepository
    {
        ICollection<ServiceCenter> GetServiceCenters();
        ServiceCenter GetServiceCenter(int id);
        bool CreateServiceCenter(ServiceCenter serviceCenter);
        bool UpdateServiceCenter(ServiceCenter serviceCenter);
        bool DeleteServiceCenter(ServiceCenter serviceCenter);
        bool ServiceCenterExists(int id);
        bool Save();
    }
}
