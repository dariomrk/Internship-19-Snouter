using Data.Interfaces;
using Data.Models;

namespace Application.Services
{
    public class PreciseLocationService : BaseService<PreciseLocation, int>
    {
        public PreciseLocationService(IRepository<PreciseLocation, int> repository) : base(repository) { }
    }
}
