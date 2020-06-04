using System.Threading.Tasks;

namespace PokemonItaly.Interface.Interfaces
{
    public interface IPokemonService
    {
       public Task<string> GetPokemonDescription(string pokemonId);
    }
}
