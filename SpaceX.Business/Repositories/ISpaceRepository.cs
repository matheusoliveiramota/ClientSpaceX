using SpaceX.Business.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpaceX.Business
{
    public interface ISpaceRepository
    {
        Task<IEnumerable<Launch>> GetNextLaunch();
        Task<IEnumerable<Launch>> GetLastLaunch();
        Task<IEnumerable<Launch>> GetPastLaunches();
        Task<IEnumerable<Launch>> GetUpcomingLaunches();
    }
}
