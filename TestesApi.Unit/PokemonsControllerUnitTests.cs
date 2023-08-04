using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using TestesApi.Controllers;
using TestesApi.HttpServices;
using TestesApi.Services;

namespace TestesApi.Unit
{
    public class PokemonsControllerUnitTests
    {
        private readonly PokemonsController controller;

        private readonly Mock<IPokeApi> mockPokeApi;
        private readonly Mock<IPokemonLoggerService> mockLogger;

        public PokemonsControllerUnitTests()
        {
            mockPokeApi = new Mock<IPokeApi>();
            mockLogger = new Mock<IPokemonLoggerService>();

            controller = new PokemonsController(mockPokeApi.Object, mockLogger.Object);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(150)]
        public async Task ShouldGetPokemon(int id)
        {
            // Arrange
            var mockResult = new Refit.ApiResponse<DTOs.Pokemon>(
                new HttpResponseMessage(HttpStatusCode.OK),
                new DTOs.Pokemon(id, "teste", 0, 0),
                new Refit.RefitSettings());

            mockPokeApi.Setup(m => m.Get(id, CancellationToken.None))
                .ReturnsAsync(mockResult);

            // Act
            var result = await controller.Get(id) as OkObjectResult;

            // Assert
            mockPokeApi.Verify(m => m.Get(id, CancellationToken.None), Times.AtLeastOnce);

            Assert.Equal((int)HttpStatusCode.OK, result!.StatusCode);
        }
        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(150)]
        public async Task ShouldFailOnGetPokemon(int id)
        {
            // Arrange
            var mockResult = new Refit.ApiResponse<DTOs.Pokemon>(
                new HttpResponseMessage(HttpStatusCode.NotFound),
                new DTOs.Pokemon(id, "teste", 0, 0),
                new Refit.RefitSettings());

            mockPokeApi.Setup(m => m.Get(id, CancellationToken.None))
                .ReturnsAsync(mockResult);

            // Act
            var result = await controller.Get(id) as ObjectResult;

            // Assert
            Assert.Equal((int)HttpStatusCode.NotFound, result!.StatusCode);
        }
    }
}