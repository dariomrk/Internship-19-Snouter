using Application.Interfaces;
using Data.Interfaces;
using Data.Models;

namespace Application.Services
{
    public class UserService : BaseService<User, int>, IUserService
    {
        public UserService(IRepository<User, int> repository) : base(repository) { }
    }
}
