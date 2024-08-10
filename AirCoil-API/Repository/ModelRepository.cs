using AirCoil_API.Data;
using AirCoil_API.Interface;
using AirCoil_API.Models;

namespace AirCoil_API.Repository
{
    public class ModelRepository : IModelRepository
    {
        private readonly DataContext _context;

        public ModelRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Model> GetModels()
        {
            return _context.Models.OrderBy(m => m.Id).ToList();
        }

        public Model GetModel(int id)
        {
            return _context.Models.Where(m => m.Id == id).FirstOrDefault();
        }

        public bool CreateModel(Model model)
        {
            throw new NotImplementedException();
        }

        public bool UpdateModel(Model model)
        {
            throw new NotImplementedException();
        }

        public bool DeleteModel(Model model)
        {
            throw new NotImplementedException();
        }

        public bool ModelExists(int id)
        {
            return _context.Models.Any(m => m.Id == id);
        }

        public bool ModelExists(string name)
        {
            return _context.Models.Any(m => m.Name.Equals(name));
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }
    }
}
