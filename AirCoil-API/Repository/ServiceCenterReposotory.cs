using AirCoil_API.Data;
using AirCoil_API.Interface;
using AirCoil_API.Models;
using Microsoft.EntityFrameworkCore;

namespace AirCoil_API.Repository
{
    public class ServiceCenterReposotory : IServiceCenterRepository
    { 
        private readonly DataContext _context;

        public ServiceCenterReposotory(DataContext context)
        {
            _context = context;
        }

        public async Task<ICollection<ServiceCenter>> GetServiceCentersAsync()
        {
            return await _context.ServiceCenters.OrderBy(s => s.Id).ToListAsync();
        }

        public async Task<ServiceCenter> GetServiceCenterAsync(int id)
        {
            return await _context.ServiceCenters.Where(s => s.Id == id).FirstOrDefaultAsync();
        }

        public async Task<ICollection<Branch>> GetBranchesByServiceCenterAsync(int id)
        {
            return await _context.Branches.Where(b => b.ServiceCenter.Id == id).ToListAsync();
        }

        public async Task<bool> CreateServiceCenterAsync(ServiceCenter serviceCenter)
        {
            _context.ServiceCenters.Add(serviceCenter);
            return await SaveAsync();
        }

        public async Task<bool> UpdateServiceCenterAsync(ServiceCenter serviceCenter)
        {
            _context.ServiceCenters.Update(serviceCenter);
            return await SaveAsync();
        }

        public async Task<bool> DeleteServiceCenterAsync(ServiceCenter serviceCenter)
        {
            _context.ServiceCenters.Remove(serviceCenter);
            return await SaveAsync();
        }

        public async Task<bool> ServiceCenterExistsAsync(int id)
        {
            return await _context.ServiceCenters.AnyAsync(s => s.Id == id);
        }

        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }
    }
}
