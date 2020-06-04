using PokemonItaly.Data.Repository;
using PokemonItaly.Interface.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PokemonItaly.Tests.Services
{
    public class PokemonServiceFake : IPokemonService
    {
        PokemonRepository pokemonRepository;
        HttpClient _client = new HttpClient();
        public PokemonServiceFake()
        {
        }
        public async Task<string> GetPokemonDescription(string pokemonName)
        {
            pokemonRepository = new PokemonRepository(_client);
            var pokemonData = await pokemonRepository.GetPokemon(pokemonName);
            return pokemonData;
        }
    }
}
