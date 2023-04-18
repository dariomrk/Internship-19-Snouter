using Data.Interfaces;
using Data.Models;

namespace Application.Services
{
    public class CityService : BaseService<City, int>
    {
        public CityService(IRepository<City, int> repository) : base(repository) { }
    }
}
