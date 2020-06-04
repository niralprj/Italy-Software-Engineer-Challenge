using Newtonsoft.Json.Linq;
using PokemonItaly.Data.Interfaces;
using PokemonItaly.Interface.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PokemonItaly.Service.Services
{
    public class PokemonService : IPokemonService
    {

        #region Declaration
        public IPokemonRepository _pokemonRepository;
        public ITranslatorRepository _translatorRepository;
        #endregion

        #region Constructor
        public PokemonService(IPokemonRepository pokemonRepository, ITranslatorRepository translatorRepository)
        {
            _pokemonRepository = pokemonRepository;
            _translatorRepository = translatorRepository;
        }
        #endregion


        #region Methods

        /// <summary>
        /// Returns Pokemon Description in Shakespears Style
        /// </summary>
        /// <param name="pokemonName">Pokemon to Search</param>
        /// <returns></returns>
        public async Task<string> GetPokemonDescription(string pokemonName)
        {
            if (string.IsNullOrEmpty(pokemonName))
            {
                throw new ArgumentException("Invalid input to read translation");
            }
            var pokemonDetails = await _pokemonRepository.GetPokemon(pokemonName);

            var pokemonDescription = await ReadPokemonDescription(pokemonDetails);

            var translatedDescription = await _translatorRepository.ConvertToShakespear(pokemonDescription);

            return ReadTranslatorText(translatedDescription);

        }

        /// <summary>
        /// Internal method to filter translated text from the shakespears API results
        /// </summary>
        /// <param name="translator">Shakespeares translated text</param>
        /// <returns></returns>
        public string ReadTranslatorText(string translator, string languageToConvert = "shakespeare")
        {
            if (string.IsNullOrEmpty(translator))
            {
                throw new ArgumentException("Invalid input to read translation");
            }

            var dataSet = new JObject(JObject.Parse(translator));
            var filteredContents = dataSet.SelectTokens("$.contents").Children().ToList();

            if (filteredContents != null && filteredContents.Count() > 0)
            {
                if (dataSet.SelectTokens("$.contents.translation").FirstOrDefault().ToString().ToLower() == languageToConvert)
                {
                    return dataSet.SelectTokens("$.contents.translated").FirstOrDefault().ToString();
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// Internal method to filter pokemon description from pokemon API result
        /// </summary>
        /// <param name="pokemonData">pokemon details</param>
        /// <returns></returns>
        public async Task<string> ReadPokemonDescription(string pokemonData ,string readInLanguage = "en")
        {
            string description = string.Empty;

            if (string.IsNullOrEmpty(pokemonData) || pokemonData.ToLower().Contains("not found"))
            {
                throw new ArgumentException("Invalid Pokemon Name");
            }
            var dataSet = new JObject(JObject.Parse(pokemonData));
            var filteredSettings = dataSet.SelectTokens("$.id");

            if (filteredSettings != null && filteredSettings.Count() > 0)
            {
                var id = filteredSettings.FirstOrDefault().ToString();

                var pokemonDescription = await _pokemonRepository.GetPokemonDescription(id);

                var descriptionDataSet = new JObject(JObject.Parse(pokemonDescription));

                var flavor_text_entries = descriptionDataSet.SelectTokens("$.flavor_text_entries").Children().ToList();
                if (flavor_text_entries != null && flavor_text_entries.Count() > 0)
                {
                    for (int i = 0; i < flavor_text_entries.Count(); i++)
                    {
                        var flavor_text_entriesDataSet = new JObject(JObject.Parse(flavor_text_entries[i].ToString()));

                        if (flavor_text_entriesDataSet.SelectTokens("$.language.name").FirstOrDefault().ToString().ToLower() == readInLanguage)
                        {
                            description += flavor_text_entriesDataSet.SelectTokens("$.flavor_text").FirstOrDefault().ToString();
                        }
                    }
                }
            }
            return description.Replace("\n", string.Empty);
        }
        #endregion

    }
}
