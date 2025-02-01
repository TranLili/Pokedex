var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.Pokedex_ApiService>("api");

builder.AddProject<Projects.Pokedex_Web>("frontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
