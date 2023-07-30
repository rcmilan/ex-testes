using Refit;
using TestesApi.DTOs;

namespace TestesApi.HttpServices
{
    public interface IPokeApi
    {
        [Get("/pokemon/{number}")]
        Task<ApiResponse<Pokemon>> Get([AliasAs("number")] int number, CancellationToken ct = default);
    }
}