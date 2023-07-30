using Microsoft.AspNetCore.Mvc;
using TestesApi.HttpServices;

namespace TestesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonsController : ControllerBase
    {
        private readonly IPokeApi pokeApi;
        public PokemonsController(IPokeApi pokeApi)
        {
            this.pokeApi = pokeApi;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int number)
        {
            var apiResponse = await pokeApi.Get(number);

            if (apiResponse.IsSuccessStatusCode)
                return Ok(apiResponse.Content);

            return Problem(
                detail: apiResponse.ReasonPhrase,
                statusCode: (int)apiResponse.StatusCode
            );
        }

    }
}
