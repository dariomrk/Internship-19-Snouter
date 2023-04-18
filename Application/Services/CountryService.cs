using Application.Interfaces;
using Data.Interfaces;
using Data.Models;

namespace Application.Services
{
    public class CountryService : BaseService<Country, int>, ICountryService
    {
        public CountryService(IRepository<Country, int> repository) : base(repository) { }
    }
}
