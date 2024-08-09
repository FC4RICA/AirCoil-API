using AirCoil_API.Models;

namespace AirCoil_API.Interface
{
    public interface IProvinceRepository
    {
        ICollection<Province> GetProvices();
        Province GetProvince(int id);
        bool ProvinceExists(string name);
        bool ProvinceExists(int id);
        bool CreateProvince(Province province);
        bool UpdateProvince(Province province);
        bool DeleteProvince(Province province);
        ICollection<Car> GetCarsByProvince(int provinceId);
        bool Save();
    }
}
