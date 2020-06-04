using PokemonItaly.Data.Constants;
using PokemonItaly.Data.Interfaces;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PokemonItaly.Data.Repository
{
    public class TranslatorRepository : ITranslatorRepository
    {

        #region Declaration
        public HttpClient _client ;
        #endregion

        #region Constructor
        public TranslatorRepository(HttpClient httpClient)
        {
            _client = httpClient;
        }
        #endregion


        /// <summary>
        /// Translates input text to shakespeares style
        /// </summary>
        /// <param name="inputString">string to convert</param>
        /// <returns></returns>
        public async Task<string> ConvertToShakespear(string inputString)
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync(ExternalAPIConstants.Shakespeares_Translator_URL + inputString);
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
