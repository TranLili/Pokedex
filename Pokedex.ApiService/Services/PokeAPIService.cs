using Microsoft.Extensions.Caching.Memory;
using System.Net;
using Pokedex.Contract.Response;
using Pokedex.ApiService.Models;

namespace Pokedex.ApiService.Services;

public class PokeAPIService
{
    private readonly HttpClient _httpClient;
    private readonly IMemoryCache _memoryCache;
    private readonly TimeSpan _duration = TimeSpan.FromMinutes(60);

    public PokeAPIService(HttpClient httpClient, IMemoryCache memoryCache)
    {
        _httpClient = httpClient;
        _memoryCache = memoryCache;
    }

    public async Task<PokemonResponse?> GetPokemon(string input)
    {
        input = input.ToLower().Trim();
        if (_memoryCache.TryGetValue(input, out PokemonResponse? cachedPokemon))
            return cachedPokemon;

        try
        {
            var response = await _httpClient.GetAsync($"https://pokeapi.co/api/v2/pokemon/{input}");
            if (response.StatusCode is HttpStatusCode.NotFound)
                return null;

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException($"Failed request: {response.StatusCode}");
            
            var pokemon = await response.Content.ReadFromJsonAsync<Pokemon>();
            if (pokemon is null)
                return null;

            PokemonResponse pokemonResponse = new()
            {
                Id = pokemon.Id,
                Name = pokemon.Name,
                Image = pokemon.Sprites.Front_Default
            };
            _memoryCache.Set(input, pokemonResponse, _duration);

            return pokemonResponse;
        }
        catch (HttpRequestException ex)
        {
            throw new Exception("Something wrong occured.", ex);
        }
    }
}
