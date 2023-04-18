using Application.Interfaces;
using Data.Interfaces;
using Data.Models;

namespace Application.Services
{
    public class CountyService : BaseService<County, int>, ICountyService
    {
        public CountyService(IRepository<County, int> repository) : base(repository) { }
    }
}
