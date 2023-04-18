using Data.Interfaces;
using Data.Models;

namespace Application.Services
{
    public class CategoryService : BaseService<Category, int>
    {
        public CategoryService(IRepository<Category, int> repository) : base(repository) { }
    }
}
