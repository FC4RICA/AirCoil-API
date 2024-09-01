using AirCoil_API.Data;
using AirCoil_API.Interface;
using AirCoil_API.Models;
using Microsoft.EntityFrameworkCore;

namespace AirCoil_API.Repository
{
    public class BranchRepository : IBranchRepository
    {
        private readonly DataContext _context;

        public BranchRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Branch>> GetBranchesAsync()
        {
            return await _context.Branches.OrderBy(b => b.Name).ToListAsync();
        }

        public async Task<Branch> GetBranchAsync(int id)
        {
            return await _context.Branches.Where(b => b.Id == id).FirstOrDefaultAsync();
        }

        public async Task<ICollection<User>> GetUserByBranchAsync(int id)
        {
            return await _context.Users.Where(u => u.Branch.Id == id).OrderBy(u => u.Id).ToListAsync();
        }

        public async Task<bool> CreateBranchAsync(Branch branch)
        {
            _context.Branches.Add(branch);
            return await SaveAsync();
        }
            
        public async Task<bool> UpdateBranchAsync(Branch branch)
        {
            _context.Branches.Update(branch);
            return await SaveAsync();
        }

        public async Task<bool> DeleteBranchAsync(Branch branch)
        {
            _context.Branches.Remove(branch);
            return await SaveAsync();
        }

        public async Task<bool> BranchExistsAsync(int id)
        {
            return await _context.Branches.AnyAsync(b => b.Id == id);
        }

        public async Task<bool> BranchExistsAsync(string name)
        {
            return await _context.Branches.AnyAsync(u => u.Name.Equals(name));
        }

        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }

    }
}
