using Application.Interfaces;
using Data.Interfaces;
using Data.Models;

namespace Application.Services
{
    public class SubCategoryService : BaseService<SubCategory, int>, ISubCategoryservice
    {
        public SubCategoryService(IRepository<SubCategory, int> repository) : base(repository) { }
    }
}
