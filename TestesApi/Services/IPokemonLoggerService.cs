using TestesApi.DTOs;

namespace TestesApi.Services
{
    public interface IPokemonLoggerService
    {
        void Log(Pokemon pkmn);
    }
}