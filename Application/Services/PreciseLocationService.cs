using Application.Interfaces;
using Data.Interfaces;
using Data.Models;

namespace Application.Services
{
    public class PreciseLocationService : BaseService<PreciseLocation, int>, IPreciseLocationService
    {
        public PreciseLocationService(IRepository<PreciseLocation, int> repository) : base(repository) { }
    }
}
