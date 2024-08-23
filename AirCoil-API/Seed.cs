using AirCoil_API.Data;
using AirCoil_API.Models;

namespace AirCoil_API
{
    public class Seed
    {
        private readonly DataContext dataContext;

        public Seed(DataContext context) { this.dataContext = context; }

        public void SeedDataContext()
        {
            if (!dataContext.Cars.Any())
            {
                var cars = new List<Car>()
                {
                    new Car()
                    {
                        LicensePlate = "มก123",
                        CreatedAt = DateTime.Now,
                        Province = new Province()
                        {
                            Name = "นครราชสีมา"
                        },
                        Model = new Model()
                        {
                            Name = "GR Supra",
                            Brand = new Brand()
                            {
                                Name = "Toyota"
                            }
                        }
                    }
                };

                dataContext.Cars.AddRange(cars);
                dataContext.SaveChanges();
            }
        }
    }
}
