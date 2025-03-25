using FuelGo.Models;

namespace FuelGo.Inerfaces
{
    public interface IConstantDictionaryRepository : IBaseRepository
    {
        ConstantDictionary GetConstantDictionary(string key);
        ICollection<ConstantDictionary> GetConstantDictionaries();
    }
}
