# Italy-Software-Engineer-Challenge

Task: Read Pokemon description from external API and return the result in Shakespearean Style

## Summary

Italy Software Engineer Challenge is an REST API build with .net core 3.1 framework & usage of Newtonsoft.Json Nuget library to perform external API.


## External APIs used

  https://pokeapi.co/api/v2/
  
  https://api.funtranslations.com/translate/shakespeare.json
  
## Usage
  
  using httpie
  
    http http://localhost:52282/api/pokemon/Charizard 
    
  using Postman
  
    Action Type : Get
    
    URL : http://localhost:52282/api/pokemon/Charizard 
    
## How to Run

  Easiest Way: 
  
  1. Clone repository 
  2. Get the source code 
  3. Open PokemonItaly.sln file in Visual Studio 2019 
  4. Run the project
  
  With Docker:
  
  0. Download the source code
  
  1. Download .net core 3.1 Run time,SDK for relevent OS (Microsoft, Linux, Mac, Docker)
  
    Link: https://dotnet.microsoft.com/download
  
  2. Install Docker for relevent OS (Microsoft, Linux, Mac)
  
    Link: https://www.docker.com/products/docker-desktop
  
  3. Open Terminal and create docker image 
  
    Command to Run :   docker build -t PokemonItaly.API-image -f Dockerfile
  
  4. Crete Container
  
    Command to Run :   docker create --name core-PokemonItaly.API PokemonItaly.API-image
  
  5. Start Container
  
    Command to Run :   docker start core-PokemonItaly.API
    
  6. API Should start listening at http://localhost:52282
  
    6.a open terminal and hit "http http://localhost:52282/pokemon/Charizard" if httpie is installed on your machine
    
    OR 
    
    6.a install and open PostMan application Configure New request:
    
    Action Type :  GET
    
    URL : http://localhost:52282/pokemon/Charizard 
  
  7. To Stop Container : 
  
    Command to Run : docker stop core-PokemonItaly.API
    
    
## Useful link to Run

    https://docs.microsoft.com/en-us/dotnet/core/docker/build-container?tabs=windows
