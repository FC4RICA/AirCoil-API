using AirCoil_API.Data;
using AirCoil_API.Interface;
using AirCoil_API.Models;

namespace AirCoil_API.Repository
{
    public class ServiceCenterReposotory : IServiceCenterRepository
    { 
        private readonly DataContext _context;

        public ServiceCenterReposotory(DataContext context)
        {
            _context = context;
        }

        public ICollection<ServiceCenter> GetServiceCenters()
        {
            return _context.ServiceCenters.OrderBy(s => s.Id).ToList();
        }

        public ServiceCenter GetServiceCenter(int id)
        {
            return _context.ServiceCenters.Where(s => s.Id == id).FirstOrDefault();
        }

        public bool CreateServiceCenter(ServiceCenter serviceCenter)
        {
            _context.ServiceCenters.Add(serviceCenter);
            return Save();
        }

        public bool UpdateServiceCenter(ServiceCenter serviceCenter)
        {
            _context.ServiceCenters.Update(serviceCenter);
            return Save();
        }

        public bool DeleteServiceCenter(ServiceCenter serviceCenter)
        {
            _context.ServiceCenters.Remove(serviceCenter);
            return Save();
        }

        public bool ServiceCenterExists(int id)
        {
            return _context.ServiceCenters.Any(s => s.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }
    }
}
