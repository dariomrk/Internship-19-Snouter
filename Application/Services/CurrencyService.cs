using Application.Interfaces;
using Data.Interfaces;
using Data.Models;

namespace Application.Services
{
    public class CurrencyService : BaseService<Currency, int>, ICurrencyService
    {
        public CurrencyService(IRepository<Currency, int> repository) : base(repository) { }
    }
}
