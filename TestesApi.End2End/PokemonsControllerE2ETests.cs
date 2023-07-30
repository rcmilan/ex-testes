using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net;
using System.Text.Json;
using TestesApi.DTOs;

namespace TestesApi.End2End
{
    public class PokemonsControllerE2ETests
    {
        private readonly HttpClient httpClient;

        public PokemonsControllerE2ETests()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            httpClient = webAppFactory.CreateClient();
        }

        [Theory]
        [InlineData(150, "Mewtwo")]
        public async Task ShouldGetPokemonData(int dexNo, string expected)
        {
            // Act
            var httpResult = await httpClient.GetAsync($"/api/pokemons/{dexNo}");

            var jsonResult = await httpResult.Content.ReadAsStringAsync();
            var res = JsonSerializer.Deserialize<Pokemon>(jsonResult)!;

            // Assert
            Assert.Equal(HttpStatusCode.OK, httpResult.StatusCode);

            Assert.Equal(dexNo, res.Id);
            Assert.Equal(expected, res.Name, ignoreCase: true);
        }
    }
}