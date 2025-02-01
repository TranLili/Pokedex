using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;
using Pokedex.ApiService.Services;
using Pokedex.Contract.Response;
using System.Net.Http.Json;
using System.Net;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace Pokedex.ApiService.Tests
{
    [TestClass]
    public class PokemonControllerTests
    {
        private WebApplicationFactory<Program> _factory;

        private Mock<PokeAPIService> _pokeAPIServiceMock;
        private Mock<HttpClient> _httpClientMock;
        private Mock<IMemoryCache> _memoryCacheMock;
        private PokeAPIService _service;

        // see if response status code 200

        // see if response status code 404

        public PokemonControllerTests()
        {
            _factory = new();
            _httpClientMock = new();
            _memoryCacheMock = new();
            _pokeAPIServiceMock = new(_httpClientMock.Object, _memoryCacheMock.Object);
            _service = new(_httpClientMock.Object, new MemoryCache(new MemoryCacheOptions()));
        }

        [TestMethod]
        public async Task GetPokemon_ValidPokemonSearch_Returns200Ok()
        {
            // Arrange
            PokemonResponse expected = new()
            {
                Id = 1,
                Name = "bulbasaur",
                Image = "bulbasaur.png"
            };

            var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddSingleton(_service);
                });
            }).CreateClient();

            // Act
            var response = await client.GetAsync("/pokemon/bulbasaur");
            var pokemon = await response.Content.ReadFromJsonAsync<PokemonResponse>();

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(pokemon);
            Assert.AreEqual(expected.Name, pokemon?.Name);
        }

        [TestMethod]
        public async Task GetPokemon_InvalidPokemonSearch_Returns404NotFound()
        {
            // Arrange
            var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddSingleton(_service);
                });
            }).CreateClient();

            // Act
            var response = await client.GetAsync("/pokemon/abc");

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }
    }

}
