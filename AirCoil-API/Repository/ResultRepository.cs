using AirCoil_API.Data;
using AirCoil_API.Dto;
using AirCoil_API.Interface;
using AirCoil_API.Models;
using Microsoft.EntityFrameworkCore;

namespace AirCoil_API.Repository
{
    public class ResultRepository : IResultRepository
    {
        private readonly DataContext _context;

        public ResultRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Result>> GetResultsAsync()
        {
            return await _context.Results.OrderBy(r => r.Id).ToListAsync();
        }

        public async Task<Result?> GetResultAsync(int id)
        {
            return await _context.Results.Where(r => r.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Result?> GetResultAsync(PredictResult predict)
        {
            return await _context.Results.Where(r => r.EDLLevel == predict.Predictions[0]).FirstOrDefaultAsync();
        }

        public async Task<ICollection<Job>> GetJobsByResultAsync(int id)
        {
            return await _context.Jobs.Where(j => j.ResultId == id).ToListAsync();
        }

        public async Task<bool> CreateResultAsync(Result result)
        {
            await _context.Results.AddAsync(result);
            return await SaveAsync();
        }

        public async Task<bool> UpdateResultAsync(Result result)
        {
            _context.Results.Update(result);
            return await SaveAsync();
        }


        public async Task<bool> DeleteResultAsync(Result result)
        {
            _context.Results.Remove(result);
            return await SaveAsync();
        }


        public async Task<bool> ResultExistsAsync(int id)
        {
            return await _context.Results.AnyAsync(r => r.Id == id);
        }

        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }
    }
}
