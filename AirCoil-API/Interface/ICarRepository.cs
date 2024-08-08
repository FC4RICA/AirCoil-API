using AirCoil_API.Models;

namespace AirCoil_API.Interface
{
    public interface ICarRepository
    {
        ICollection<Car> GetCars();
        Car GetCar(int id);
        bool CarExists(int id);
    }
}
