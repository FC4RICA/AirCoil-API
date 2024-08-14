using AirCoil_API.Data;
using AirCoil_API.Interface;
using AirCoil_API.Models;

namespace AirCoil_API.Repository
{
    public class BranchRepository : IBranchRepository
    {
        private readonly DataContext _context;

        public BranchRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Branch> GetBranches()
        {
            return _context.Branches.OrderBy(b => b.Name).ToList();
        }

        public Branch GetBranch(int id)
        {
            return _context.Branches.Where(b => b.Id == id).FirstOrDefault();
        }

        public ICollection<User> GetUserByBranch(int id)
        {
            return _context.Users.Where(u => u.Branch.Id == id).OrderBy(u => u.Id).ToList();
        }

        public bool CreateBranch(Branch branch)
        {
            _context.Branches.Add(branch);
            return Save();
        }
            
        public bool UpdateBranch(Branch branch)
        {
            _context.Branches.Update(branch);
            return Save();
        }

        public bool DeleteBranch(Branch branch)
        {
            _context.Branches.Remove(branch);
            return Save();
        }

        public bool BranchExists(int id)
        {
            return _context.Branches.Any(b => b.Id == id);
        }

        public bool BranchExists(string name)
        {
            return _context.Branches.Any(u => u.Name.Equals(name));
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

    }
}
