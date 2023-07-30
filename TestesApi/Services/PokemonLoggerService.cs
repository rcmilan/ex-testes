using TestesApi.DTOs;

namespace TestesApi.Services
{
    internal class PokemonLoggerService : IPokemonLoggerService
    {
        private readonly ILogger logger;

        public PokemonLoggerService(ILogger<PokemonLoggerService> logger)
        {
            this.logger = logger;
        }

        public void Log(Pokemon pkmn)
        {
            // Faz alguma coisa

            logger.LogInformation($"{pkmn.Id} - {pkmn.Name}");

            // Faz alguma outra coisa
        }
    }
}