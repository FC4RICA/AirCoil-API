using AirCoil_API.Data;
using AirCoil_API.Dto;
using AirCoil_API.Helpers;
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

        public async Task<ICollection<Car>> GetCarsAsync(CarQueryObject query)
        {
            var cars = _context.Cars
                .Include(c => c.Province)
                .Include(c => c.Model)
                .ThenInclude(m => m.Brand)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.LicensePlate))
            {
                cars = cars.Where(c => c.LicensePlate.Contains(query.LicensePlate));
            }

            if (!string.IsNullOrWhiteSpace(query.Province))
            {
                cars = cars.Where(c => c.Province.Name.Contains(query.Province));
            }

            return await _context.Cars.OrderBy(c => c.Id).ToListAsync();
        }

        public async Task<Car> GetCarAsync(int id)
        {
            return await _context.Cars
                .Include(c => c.Province)
                .Include(c => c.Model)
                .ThenInclude(m => m.Brand)
                .Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Car> GetCarAsync(CreateCarDto car)
        {
            return await _context.Cars
                .Include(c => c.Province)
                .Include(c => c.Model)
                .ThenInclude(m => m.Brand)
                .Where(c => c.LicensePlate == car.LicensePlate)
                .Where(c => c.Province.Name == car.Province)
                .Where(c => c.Model.Name == car.Model)
                .FirstOrDefaultAsync();
        }

        public async Task<ICollection<Job>> GetJobsByCarAsync(int id, JobQueryObject? query)
        {
            var jobs = _context.Jobs.Where(j => j.Car.Id == id)
                .Include(j => j.User)
                .Include(j => j.Car).ThenInclude(c => c.Province)
                .Include(j => j.Car).ThenInclude(c => c.Model)
                .Include(j => j.Images)
                .Include(j => j.Result)
                .AsQueryable();

            if (query.StartDate.HasValue)
            {
                jobs = jobs.Where(j => j.CreatedAt >= query.StartDate);
            }

            if (query.EndDate.HasValue)
            {
                jobs = jobs.Where(j => j.CreatedAt <= query.EndDate);
            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await jobs.Skip(skipNumber).Take(query.PageSize).OrderBy(j => j.Id).ToListAsync();
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
