using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using PactNet;
using Refit;
using System.Net;
using TestesApi.DTOs;
using TestesApi.HttpServices;

namespace TestesApi.Integration
{
    public class PokemonsControllerIntegrationTests
    {
        private readonly HttpClient httpClient;
        private readonly IPactBuilderV3 pactBuilder;

        private readonly int pactPort = 1337;

        public PokemonsControllerIntegrationTests()
        {
            pactBuilder = Pact.V3("PokeApiConsumer", "PokeApi", new PactConfig())
                .WithHttpInteractions(pactPort);

            var webAppFactory = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureTestServices(services =>
                    {
                        services.AddSingleton(RestService.For<IPokeApi>($"http://localhost:{pactPort}"));
                    });
                });
            httpClient = webAppFactory.CreateClient();
        }

        [Fact]
        public async Task ShouldGetPokemon()
        {
            // Arrange
            var mockResult = new Pokemon(0, "name", 0, 0);

            pactBuilder
                .UponReceiving("Get Pokemon")
                    .Given("Pokedex Id")
                    .WithRequest(HttpMethod.Get, $"/pokemon/0")
                .WillRespond()
                    .WithStatus(HttpStatusCode.OK)
                    .WithHeader("Content-Type", "application/json; charset=utf-8")
                    .WithJsonBody(mockResult);

            await pactBuilder.VerifyAsync(async ctx =>
            {
                // Act
                var result = await httpClient.GetAsync("/api/pokemons/0");

                // Assert
                Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            });
        }

        [Fact]
        public async Task ShouldReturnNotFoundPokemon()
        {
            // Arrange
            var mockResult = new Pokemon(0, "name", 0, 0);

            pactBuilder
                .UponReceiving("Get Pokemon")
                    .Given("Pokedex Id")
                    .WithRequest(HttpMethod.Get, $"/pokemon/0")
                .WillRespond()
                    .WithStatus(HttpStatusCode.NotFound)
                    .WithHeader("Content-Type", "application/json; charset=utf-8");

            await pactBuilder.VerifyAsync(async ctx =>
            {
                // Act
                var result = await httpClient.GetAsync("/api/pokemons/0");

                // Assert
                Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
            });
        }
    }
}