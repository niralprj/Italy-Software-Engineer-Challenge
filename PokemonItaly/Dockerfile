#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["PokemonItaly/PokemonItaly.API.csproj", "PokemonItaly/"]
COPY ["PokemonItaly.Service/PokemonItaly.Service.csproj", "PokemonItaly.Service/"]
COPY ["Pokemon.Data/PokemonItaly.Data.csproj", "Pokemon.Data/"]
COPY ["PokemonItaly.Interface/PokemonItaly.Interface.csproj", "PokemonItaly.Interface/"]
RUN dotnet restore "PokemonItaly/PokemonItaly.API.csproj"
COPY . .
WORKDIR "/src/PokemonItaly"
RUN dotnet build "PokemonItaly.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PokemonItaly.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PokemonItaly.API.dll"]