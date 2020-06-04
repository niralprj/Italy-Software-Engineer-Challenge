using PokemonItaly.Data.Constants;
using PokemonItaly.Data.Interfaces;
using System.Net.Http;
using System.Threading.Tasks;

namespace PokemonItaly.Data.Repository
{
    public class PokemonRepository : IPokemonRepository
    {

        #region Declaration
        public HttpClient _client;
        #endregion

        #region Constructor
        public PokemonRepository(HttpClient httpClient)
        {
            _client = httpClient;
        }
        #endregion


        /// <summary>
        /// Search Pokemon on External API
        /// </summary>
        /// <param name="pokemonName"></param>
        /// <returns></returns>
        public async Task<string> GetPokemon(string pokemonName)
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync(ExternalAPIConstants.Pokemon_GetPokemon_API_URL + pokemonName);
                var data = await response.Content.ReadAsStringAsync();
                return data;
            }
            catch
            {
                throw new HttpRequestException(ExceptionConstants.API_Connection_Error);
            }
        }

        /// <summary>
        /// Get Pokemon description on External API
        /// </summary>
        /// <param name="pokemonId"></param>
        /// <returns></returns>
        public async Task<string> GetPokemonDescription(string pokemonId)
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync(ExternalAPIConstants.Pokemon_GetPokemonDescription_API_URL + pokemonId);
                var data = await response.Content.ReadAsStringAsync();
                return data;
            }
            catch
            {
                throw new HttpRequestException(ExceptionConstants.API_Connection_Error);
            }

        }
    }
}
