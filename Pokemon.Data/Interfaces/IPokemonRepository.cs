using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PokemonItaly.Data.Interfaces
{
    public interface IPokemonRepository
    {
        /// <summary>
        /// Signature to Return Pokemon Description
        /// </summary>
        /// <param name="pokemonName"></param>
        /// <returns></returns>
        public Task<string> GetPokemonDescription(string pokemonId);
        public Task<string> GetPokemon(string pokemonName);
    }
}
