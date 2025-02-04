using Microsoft.AspNetCore.Components.Web;
using Pokedex.Contract.Response;

namespace Pokedex.Web.Components.Pages;

public partial class Home
{
	private string? input;
    private PokemonResponse? pokemon;
    private string? errorMessage;

    private async Task SearchPokemon()
    {
        errorMessage = null;
        if (!string.IsNullOrEmpty(input))
        {
            try
            {
                pokemon = await pokedexAPI.GetPokemonAsync(input);

                if (pokemon is null)
                    errorMessage = "No pokémon :(";
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }
    }

    private async Task EnterKeyPress(KeyboardEventArgs e)
    {
        if (e.Key is "Enter")
        {
            await SearchPokemon();
        }
    }
}
