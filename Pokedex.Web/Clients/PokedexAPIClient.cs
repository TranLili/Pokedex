using Pokedex.Contract.Response;
using System.Net;

namespace Pokedex.Web.Clients;

public class PokedexAPIClient(HttpClient httpClient)
{
    public async Task<PokemonResponse?> GetPokemonAsync(string input, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"/pokemon/{input}", cancellationToken);

        if (response.StatusCode == HttpStatusCode.NotFound)
            return null;

        return await response.Content.ReadFromJsonAsync<PokemonResponse>(cancellationToken);
    }
}
