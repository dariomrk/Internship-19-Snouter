using Data.Interfaces;
using Data.Models;

namespace Application.Services
{
    public class SubCategoryService : BaseService<SubCategory, int>
    {
        public SubCategoryService(IRepository<SubCategory, int> repository) : base(repository) { }
    }
}
