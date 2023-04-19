using Data.Models;

namespace Application.Interfaces
{
    public interface ICountyService : IService<County, int>
    {
        Task<County?> FindByNameAsync(string name, CancellationToken cancellationToken = default);
    }
}
