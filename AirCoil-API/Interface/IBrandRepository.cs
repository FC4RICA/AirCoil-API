using AirCoil_API.Models;

namespace AirCoil_API.Interface
{
    public interface IBrandRepository
    {
        Task<ICollection<Brand>> GetBrandsAsync();
        Task<Brand> GetBrandAsync(int id);
        Task<ICollection<Model>> GetModelsByBrandAsync(int id);
        Task<bool> CreateBrandAsync(Brand brand);
        Task<bool> UpdateBrandAsync(Brand brand);
        Task<bool> DeleteBrandAsync(Brand brand);
        Task<bool> BrandExistsAsync(int id);
        Task<bool> BrandExistsAsync(string name);
       Task<bool> SaveAsync();
    }
}
