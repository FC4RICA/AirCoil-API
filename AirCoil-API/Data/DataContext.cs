using AirCoil_API.Models;
using Microsoft.EntityFrameworkCore;

namespace AirCoil_API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DbContext> options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<ServiceCenter> ServiceCenters { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<Series> Series { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Result> Results { get; set; }

    }
}
