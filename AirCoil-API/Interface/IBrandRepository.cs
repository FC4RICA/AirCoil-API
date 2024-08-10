using AirCoil_API.Models;

namespace AirCoil_API.Interface
{
    public interface IBrandRepository
    {
        ICollection<Brand> GetBrands();
        Brand GetBrand(int id);
        ICollection<Model> GetModelsByBrand(int id);
        bool CreateBrand(Brand brand);
        bool UpdateBrand(Brand brand);
        bool DeleteBrand(Brand brand);
        bool BrandExists(int id);
        bool BrandExists(string name);
        bool Save();
    }
}
