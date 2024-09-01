using AirCoil_API.Models;

namespace AirCoil_API.Interface
{
    public interface IBranchRepository
    {
        Task<ICollection<Branch>> GetBranchesAsync();
        Task<Branch> GetBranchAsync(int id);
        Task<ICollection<User>> GetUserByBranchAsync(int id); 
        Task<bool> CreateBranchAsync(Branch branch);
        Task<bool> UpdateBranchAsync(Branch branch);
        Task<bool> DeleteBranchAsync(Branch branch);
        Task<bool> BranchExistsAsync(int id);
        Task<bool> BranchExistsAsync(string name);
        Task<bool> SaveAsync();
    }
}
