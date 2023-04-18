using Data.Interfaces;
using Data.Models;

namespace Application.Services
{
    public class ProductService : BaseService<Product, int>
    {
        public ProductService(IRepository<Product, int> repository) : base(repository) { }
    }
}
