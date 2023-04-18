using Data.Interfaces;
using Data.Models;

namespace Application.Services
{
    public class CurrencyService : BaseService<Currency, int>
    {
        public CurrencyService(IRepository<Currency, int> repository) : base(repository) { }
    }
}
