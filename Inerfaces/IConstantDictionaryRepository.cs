using FuelGo.Models;

namespace FuelGo.Inerfaces
{
    public interface IConstantDictionaryRepository : IBaseRepository
    {
        ConstantDictionary GetConstantdictionary(string key);
    }
}
