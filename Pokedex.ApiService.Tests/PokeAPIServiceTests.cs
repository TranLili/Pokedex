using Microsoft.Extensions.Caching.Memory;
using Moq;
using Pokedex.ApiService.Services;

namespace Pokedex.ApiService.Tests
{
    [TestClass]
    public sealed class PokeAPIServiceTests
    {
        private PokeAPIService _service;
        private Mock<HttpClient> _httpClient;
        private MemoryCache _memoryCache;

        public PokeAPIServiceTests()
        {
            _httpClient = new Mock<HttpClient>();
            _memoryCache = new MemoryCache(new MemoryCacheOptions());
            _service = new PokeAPIService(_httpClient.Object, _memoryCache);
        }

        [TestMethod]
        public async Task GetPokemon_UsingName_ReturnsPokemonData()
        {
            // Arrange
            var expectedName = "bulbasaur";
            var expectedId = 1;

            // Act
            var result = await _service.GetPokemon(expectedName);

            // Assert
            Assert.AreEqual(expectedName, result.Name.ToLower());
            Assert.AreEqual(expectedId, result.Id);
            Assert.IsFalse(string.IsNullOrEmpty(result.Image));
        }

        [TestMethod]
        public async Task GetPokemon_UsingId_ReturnsPokemonData()
        {
            // Arrange
            var expectedName = "bulbasaur";
            var expectedId = 1;

            // Act
            var result = await _service.GetPokemon(expectedId.ToString());

            // Assert
            Assert.AreEqual(expectedName, result.Name.ToLower());
            Assert.AreEqual(expectedId, result.Id);
            Assert.IsFalse(string.IsNullOrEmpty(result.Image));
        }

        [TestMethod]
        public async Task GetPokemon_UsingNonExistantPokemon_ReturnsNull()
        {
            // Arrange
            var pokemon = "notexists";

            // Act
            var result = await _service.GetPokemon(pokemon);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task GetPokemon_GetPokemonThenVerifyCache_CacheHit()
        {
            // Arrange
            _memoryCache.Clear();
            var pokemon = "bulbasaur";

            // Act
            var result = await _service.GetPokemon(pokemon);        

            // Assert
            var cacheResult = _memoryCache.Get(pokemon);
            Assert.IsNotNull(cacheResult);
            Assert.AreEqual(result, cacheResult);
        }

        [TestMethod]
        public async Task GetPokemon_GetPokemonByIdThenVerifyCacheByName_CacheMiss()
        {
            // Arrange
            _memoryCache.Clear();
            var pokemonName = "bulbasaur";
            var pokemonId = 1;

            // Act
            var result = await _service.GetPokemon(pokemonId.ToString());

            // Assert
            var cacheResult = _memoryCache.Get(pokemonName);
            Assert.IsNull(cacheResult);
        }
    }
}
