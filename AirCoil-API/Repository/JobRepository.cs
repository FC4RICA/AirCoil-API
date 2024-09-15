using AirCoil_API.Data;
using AirCoil_API.Helpers;
using AirCoil_API.Interface;
using AirCoil_API.Models;
using Microsoft.EntityFrameworkCore;

namespace AirCoil_API.Repository
{
    public class JobRepository : IJobRepository
    {
        private readonly DataContext _context;

        public JobRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Job>> GetJobsAsync(JobQueryObject query)
        {
            var jobs = _context.Jobs
                .Include(j => j.User)
                .Include(j => j.Car).ThenInclude(c => c.Province)
                .Include(j => j.Car).ThenInclude(c => c.Model).ThenInclude(m => m.Brand)
                .Include(j => j.Images)
                .Include(j => j.Result)
                .AsQueryable();

            if (query.StartDate.HasValue)
            {
                jobs = jobs.Where(j => j.CreatedAt >= query.StartDate);
            }

            if (query.EndDate.HasValue)
            {
                jobs = jobs.Where(j => j.CreatedAt <= query.EndDate);
            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await jobs.Skip(skipNumber).Take(query.PageSize).OrderBy(j => j.Id).ToListAsync();
        }

        public async Task<Job> GetJobAsync(int id)
        {
            return await _context.Jobs.Where(j => j.Id == id)
                .Include(j => j.User)
                .Include(j => j.Car).ThenInclude(c => c.Province)
                .Include(j => j.Car).ThenInclude(c => c.Model).ThenInclude(m => m.Brand)
                .Include(j => j.Images)
                .Include(j => j.Result)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> CreateJobAsync(Job job)
        {
            await _context.Jobs.AddAsync(job);
            return await SaveAsync();
        }
        public async Task<bool> UpdateJobAsync(Job job)
        {
            _context.Jobs.Update(job);
            return await SaveAsync();
        }

        public async Task<bool> DeleteJobAsync(Job job)
        {
            _context.Jobs.Remove(job);
            return await SaveAsync();
        }        

        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }
    }
}
