using Application.Interfaces;
using Data.Interfaces;
using Data.Models;

namespace Application.Services
{
    public class CategoryService : BaseService<Category, int>, ICategoryService
    {
        public CategoryService(IRepository<Category, int> repository) : base(repository) { }
    }
}
