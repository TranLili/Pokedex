# Pokedex
Simple frontend and api acting as a reverse proxy for PokeAPI. \
Displays Pokémon details: Name, ID, Image.

## Image of the .NET Aspire portal when running the application
![image](https://github.com/user-attachments/assets/48b0e957-db1f-4b55-be00-bea8dfb6779e)

## The Api Swagger portal
![image](https://github.com/user-attachments/assets/fc42cca1-6305-4f1b-a477-1f12343128d8)

## Blazor Frontend 
![image](https://github.com/user-attachments/assets/4e20b1a0-8b0c-4b1f-8cdd-540609015434)

## Features
* Search Pokemon
  - Search by Pokémon name
  - Search by Pokémon ID
  - Shows error message if Pokémon can't be found by search input

## Programming languages
* C#

## Dependencies
* .NET SDK 9.0
* PokeAPI (https://pokeapi.co/)

## API Integration
### PokeAPI
The app integrates with the PokeAPI to retrieve Pokémon details. API endpoints used:

* GET https://pokeapi.co/api/v2/pokemon/{input}:
  - {input} can be a Pokémon name or ID.
  - Returns Pokémon details or 404 if not found.

## Compilation and Installation
```
# Open a terminal (Command Prompt, PowerShell, or Terminal on macOS/Linux)

# Ensure Git is installed
# Visit https://git-scm.com to download and install Git if necessary

# Clone the repository
git clone https://github.com/yourusername/PokedexApp.git

# Navigate to the project directory
cd PokedexApp

# Make sure .NET SDK is installed
# Visit the official Microsoft website to install or update it if necessary

# Restore dependencies
dotnet restore

# Compile the project
dotnet build --configuration Release

# Run the application
dotnet run --project Pokedex
```

### Running Tests
```
# Navigate to the test project directory
cd Pokedex.ApiService.Tests

# Execute test suite
dotnet test
```
