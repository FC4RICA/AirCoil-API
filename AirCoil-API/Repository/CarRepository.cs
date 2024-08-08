using AirCoil_API.Data;
using AirCoil_API.Interface;
using AirCoil_API.Models;

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
            return _context.Cars.OrderBy(c => c.Id).ToList();
        }

        Car ICarRepository.GetCar(int id)
        {
            return _context.Cars.Where(c => c.Id == id).FirstOrDefault();
        }

        bool ICarRepository.CarExists(int id)
        {
            return _context.Cars.Any(c => c.Id == id); 
        }


    }
}
