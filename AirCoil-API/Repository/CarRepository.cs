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

        public ICollection<Car> GetCars()
        {
            return _context.Cars
                .Include(c => c.Province)
                .Include(c => c.Model)
                .ThenInclude(m => m.Brand)
                .OrderBy(c => c.Id).ToList();
        }

        public Car GetCar(int id)
        {
            return _context.Cars.Where(c => c.Id == id)
                .Include(c => c.Province)
                .Include(c => c.Model)
                .ThenInclude(m => m.Brand)
                .FirstOrDefault();
        }

        public bool CarExists(int id)
        {
            return _context.Cars.Any(c => c.Id == id); 
        }


    }
}
