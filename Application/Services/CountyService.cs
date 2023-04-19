using Application.Interfaces;
using Data.Interfaces;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class CountyService : BaseService<County, int>, ICountyService
    {
        public CountyService(IRepository<County, int> repository) : base(repository) { }

        public override async Task<County?> FindAsync(int id, CancellationToken cancellationToken = default)
        {
            var county = await _repository
                .Query()
                .AsNoTracking()
                .Include(c => c.Cities)
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

            return county;
        }

        public async Task<County?> FindByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            var county = await _repository
                .Query()
                .AsNoTracking()
                .Include(c => c.Cities)
                .FirstOrDefaultAsync(c => c.Name.ToLower() == name.ToLower(), cancellationToken);

            return county;
        }
    }
}
