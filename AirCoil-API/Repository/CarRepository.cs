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
            return _context.Cars.OrderBy(c => c.Id).ToList();
        }

        public Car GetCar(int id)
        {
            return _context.Cars.Where(c => c.Id == id).FirstOrDefault();
        }

        public ICollection<Job> GetJobsByCar(int id)
        {
            return _context.Jobs.Where(j => j.Car.Id == id).OrderBy(j => j.Id).ToList();
        }

        public bool CreateCar(Car car)
        {
            _context.Cars.Add(car);
            return Save();
        }

        public bool UpdateCar(Car car)
        {
            _context.Cars.Update(car);
            return Save();
        }

        public bool DeleteCar(Car car)
        {
            _context.Cars.Remove(car);
            return Save();
        }

        public bool CarExists(int id)
        {
            return _context.Cars.Any(c => c.Id == id); 
        }
        public bool CarExists(Car car)
        {
            return _context.Cars
                .Any(c => c.LicensePlate == car.LicensePlate && c.Province == car.Province && c.Model == car.Model);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

    }
}
