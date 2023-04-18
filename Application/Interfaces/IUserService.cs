using Contracts.Requests;
using Contracts.Responses;
using Data.Models;

namespace Application.Interfaces
{
    public interface IUserService : IService<User, int>
    {
        Task<CreateUserResponse> CreateAsync(CreateUserRequest newUserDetails, CancellationToken cancellationToken = default);
    }
}
