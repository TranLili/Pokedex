using Pokedex.Contract.Response;

namespace Pokedex.Web.Clients;

public class PokedexAPIClient(HttpClient httpClient)
{
    public async Task<PokemonResponse?> GetPokemonAsync(string input, CancellationToken cancellationToken = default)
    {
        return await httpClient.GetFromJsonAsync<PokemonResponse>($"/pokemon/{input}", cancellationToken);
    }
}
