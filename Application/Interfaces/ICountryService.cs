using Contracts.Requests;
using Contracts.Responses;
using Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace Application.Interfaces
{
    public interface ICountryService : IService<Country, int>
    {
        Task<CreateCountryResponse> CreateAsync([FromBody] CreateCountryRequest request, CancellationToken cancellationToken = default);
    }
}
