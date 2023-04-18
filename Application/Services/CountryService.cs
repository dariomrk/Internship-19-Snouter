using Data.Interfaces;
using Data.Models;

namespace Application.Services
{
    public class CountryService : BaseService<Country, int>
    {
        public CountryService(IRepository<Country, int> repository) : base(repository) { }
    }
}
