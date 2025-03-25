using FuelGo.Data;
using FuelGo.Inerfaces;
using FuelGo.Models;

namespace FuelGo.Repository
{
    public class ConstantDictionaryRepository : BaseRepository, IConstantDictionaryRepository
    {
        public ConstantDictionaryRepository(DataContext context) : base(context)
        {
        }

        public ICollection<ConstantDictionary> GetConstantDictionaries()
        {
            return _context.ConstantDictionaries.OrderBy(cd => cd.Id).ToList();
        }

        public ConstantDictionary GetConstantDictionary(string key)
        {
            return _context.ConstantDictionaries.Where(cd => cd.Key == key).FirstOrDefault();
        }
    }
}
