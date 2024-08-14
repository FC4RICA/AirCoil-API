using AirCoil_API.Models;

namespace AirCoil_API.Interface
{
    public interface IBranchRepository
    {
        ICollection<Branch> GetBranches();
        Branch GetBranch(int id);
        ICollection<User> GetUserByBranch(int id); 
        bool CreateBranch(Branch branch);
        bool UpdateBranch(Branch branch);
        bool DeleteBranch(Branch branch);
        bool BranchExists(int id);
        bool BranchesExist(string name);
        bool Save();
    }
}
