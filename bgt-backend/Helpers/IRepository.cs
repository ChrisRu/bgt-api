using System.Collections.Generic;
using System.Threading.Tasks;

namespace BGTBackend.Helpers
{
    internal interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAll();

        Task<T> Get(Dictionary<string, string> match);
    }
}