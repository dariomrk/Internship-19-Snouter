using Contracts.Requests;
using Contracts.Responses;
using Data.Models;

namespace Application.Interfaces
{
    public interface IUserService : IService<User, int>
    {
        Task<UserResponse> CreateAsync(CreateUserRequest newUserDetails, CancellationToken cancellationToken = default);
    }
}
