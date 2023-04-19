using Application.Interfaces;
using Data.Interfaces;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class CountryService : BaseService<Country, int>, ICountryService
    {
        public CountryService(IRepository<Country, int> countryRepository) : base(countryRepository) { }

        public override async Task<ICollection<Country>> GetAll(CancellationToken cancellationToken = default)
        {
            var countries = await _repository
                .Query()
                .AsNoTracking()
                .Include(c => c.Counties)
                .ToListAsync(cancellationToken);

            return countries;
        }
    }
}
