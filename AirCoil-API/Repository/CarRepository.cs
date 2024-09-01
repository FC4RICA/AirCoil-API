using AirCoil_API.Data;
using AirCoil_API.Interface;
using AirCoil_API.Models;
using Microsoft.EntityFrameworkCore;

namespace AirCoil_API.Repository
{
    public class CarRepository : ICarRepository
    {
        private readonly DataContext _context;
        public CarRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Car>> GetCarsAsync()
        {
            return await _context.Cars.OrderBy(c => c.Id).ToListAsync();
        }

        public async Task<Car> GetCarAsync(int id)
        {
            return await _context.Cars.Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<ICollection<Job>> GetJobsByCarAsync(int id)
        {
            return await _context.Jobs.Where(j => j.Car.Id == id).OrderBy(j => j.Id).ToListAsync();
        }

        public async Task<bool> CreateCarAsync(Car car)
        {
            _context.Cars.Add(car);
            return await SaveAsync();
        }

        public async Task<bool> UpdateCarAsync(Car car)
        {
            _context.Cars.Update(car);
            return await SaveAsync();
        }

        public async Task<bool> DeleteCarAsync(Car car)
        {
            _context.Cars.Remove(car);
            return await SaveAsync();
        }

        public async Task<bool> CarExistsAsync(int id)
        {
            return await _context.Cars.AnyAsync(c => c.Id == id); 
        }
        public async Task<bool> CarExistsAsync(Car car)
        {
            return await _context.Cars
                .AnyAsync(c => c.LicensePlate == car.LicensePlate && c.Province == car.Province && c.Model == car.Model);
        }

        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }

    }
}
