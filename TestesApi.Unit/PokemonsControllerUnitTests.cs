using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using TestesApi.Controllers;
using TestesApi.HttpServices;

namespace TestesApi.Unit
{
    public class PokemonsControllerUnitTests
    {
        private readonly PokemonsController controller;

        private readonly Mock<IPokeApi> mockPokeApi;

        public PokemonsControllerUnitTests()
        {
            mockPokeApi = new Mock<IPokeApi>();

            controller = new PokemonsController(mockPokeApi.Object);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(150)]
        public async Task GetPokemon(int id)
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
    }
}