using AirCoil_API.Models;

namespace AirCoil_API.Interface
{
    public interface ICarRepository
    {
        ICollection<Car> GetCars();
        Car GetCar(int id);
        ICollection<Job> GetJobsByCar(int id);
        bool CreateCar(Car car);
        bool UpdateCar(Car car);
        bool DeleteCar(Car car);
        bool CarExists(int id);
        bool CarExists(Car car);
        bool Save();
    }
}
