using AirCoil_API.Models;

namespace AirCoil_API.Interface
{
    public interface IModelRepository
    {
        ICollection<Model> GetModels();
        Model GetModel(int id);
        ICollection<Car> GetCarsByModel(int id);
        bool CreateModel(Model model);
        bool UpdateModel(Model model);
        bool DeleteModel(Model model);
        bool ModelExists(int id);
        bool ModelExists(string name);
        bool Save();
    }
}
