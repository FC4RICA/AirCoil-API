using AirCoil_API.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace AirCoil_API.Interface
{
    public interface IProvinceRepository
    {
        ICollection<Province> GetProvices();
        bool ProvinceExists(string name);
    }
}
