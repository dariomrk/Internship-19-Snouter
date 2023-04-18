using Data.Interfaces;
using Data.Models;

namespace Application.Services
{
    public class LocationService : BaseService<Location, int>
    {
        public LocationService(IRepository<Location, int> repository) : base(repository) { }
    }
}
