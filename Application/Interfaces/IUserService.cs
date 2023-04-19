using Contracts.Requests;
using Contracts.Responses;
using Data.Models;

namespace Application.Interfaces
{
    public interface IUserService : IService<User, int>
    {
        Task<UserResponse> CreateAsync(UserRequest newUserDetails, CancellationToken cancellationToken = default);
        Task<UserResponse> UpdateAsync(int id, UserRequest updateUserDetails, CancellationToken cancellationToken = default);
    }
}
