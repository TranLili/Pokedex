using Pokedex.ApiService.Services;
using Pokedex.Contract.Response;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Add required services for the PokeAPI
builder.Services.AddTransient<PokeAPIService>();
builder.Services.AddMemoryCache();
builder.Services.AddHttpClient();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/pokemon/{input}", async (PokeAPIService pokeAPIService, string input) =>
{
    var pokemon = await pokeAPIService.GetPokemon(input);
    return pokemon is not null
        ? Results.Ok(pokemon)
        : Results.NotFound();
})
.WithName("GetPokemon")
.WithSummary("Proxy to PokeAPI")
.WithDescription("Provides Pokémon details, including ID, name, and image, based on the input provided.")
.Produces<PokemonResponse>(StatusCodes.Status200OK)
.Produces(StatusCodes.Status404NotFound);


app.MapDefaultEndpoints();

app.Run();

// Make the implicit Program class public so test projects can access it
public partial class Program { }
