using System.Threading.Tasks;
using BGTBackend.Helpers;

namespace BGTBackend.Clients
{
    public class BaseRepository : Repository
    {
        public Task<string> Test()
        {
            return this.QueryFirstOrDefault<string>("SELECT SYSDATETIME();");
        }
    }
}