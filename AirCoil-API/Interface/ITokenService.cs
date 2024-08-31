using AirCoil_API.Models;

namespace AirCoil_API.Interface
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
