using Microsoft.AspNetCore.Mvc;
using TestesApi.HttpServices;
using TestesApi.Services;

namespace TestesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonsController : ControllerBase
    {
        private readonly IPokeApi pokeApi;
        private readonly IPokemonLoggerService logger;

        public PokemonsController(IPokeApi pokeApi, IPokemonLoggerService logger)
        {
            this.pokeApi = pokeApi;
            this.logger = logger;
        }

        [HttpGet("{number}")]
        public async Task<IActionResult> Get(int number)
        {
            var apiResponse = await pokeApi.Get(number);

            if (apiResponse.IsSuccessStatusCode)
            {
                logger.Log(apiResponse.Content);
                return Ok(apiResponse.Content);
            }

            return Problem(
                detail: apiResponse.ReasonPhrase,
                statusCode: (int)apiResponse.StatusCode
            );
        }
    }
}