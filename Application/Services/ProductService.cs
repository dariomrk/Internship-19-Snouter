using Application.Interfaces;
using Data.Interfaces;
using Data.Models;

namespace Application.Services
{
    public class ProductService : BaseService<Product, int>, IProductService
    {
        public ProductService(IRepository<Product, int> repository) : base(repository) { }
    }
}
