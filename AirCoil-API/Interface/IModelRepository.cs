using AirCoil_API.Models;

namespace AirCoil_API.Interface
{
    public interface IModelRepository
    {
        Task<ICollection<Model>> GetModelsAsync();
        Task<Model> GetModelAsync(int id);
        Task<ICollection<Car>> GetCarsByModelAsync(int id);
        Task<bool> CreateModelAsync(Model model);
        Task<bool> UpdateModelAsync(Model model);
        Task<bool> DeleteModelAsync(Model model);
        Task<bool> ModelExistsAsync(int id);
        Task<bool> ModelExistsAsync(string name);
        Task<bool> SaveAsync();
    }
}
