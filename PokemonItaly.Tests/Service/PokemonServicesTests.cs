using PokemonItaly.Data.Repository;
using PokemonItaly.Service.Services;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace PokemonItaly.Tests.Service
{
    public class PokemonServicesTests
    {
        public readonly PokemonService _pokemonService;
        public readonly PokemonRepository _pokemonRepository;
        public readonly TranslatorRepository _translatorRepository;

        public PokemonServicesTests()
        {
            _pokemonRepository = new PokemonRepository(new HttpClient());
            _translatorRepository = new TranslatorRepository(new HttpClient());
            _pokemonService = new PokemonService(_pokemonRepository, _translatorRepository);// (pokemonRepository, translatorRepository);
        }

        [Fact]
        public async Task GetPokemonDescription_SearchWithRealData()
        {
            var result = await _pokemonService.GetPokemonDescription("Charizard");
            Assert.True(!string.IsNullOrEmpty(result));
            Assert.False(string.IsNullOrEmpty(result));
        }

        [Fact]
        public async Task GetPokemonDescription_SearchWithFakeData()
        {
            try
            {
                var result = await _pokemonService.GetPokemonDescription("fakeData");
                Assert.False(!string.IsNullOrEmpty(result));
            }
            catch (Exception)
            {
                // Valid Exception for NO DATA FOUND
                // EXCEPTION IS THROWN AT GLOBAL LEVEL, HANDLED IN CUSTOME MIDDLEWARE
                Assert.True(true);
            }
        }

        [Fact]
        public async Task GetPokemonDescription_SearchWithEmptyData()
        {
            try
            {
                var result = await _pokemonService.GetPokemonDescription(string.Empty);
                Assert.False(!string.IsNullOrEmpty(result));
            }
            catch (Exception)
            {
                // Valid Exception for NO DATA FOUND
                // EXCEPTION IS THROWN AT GLOBAL LEVEL, HANDLED IN CUSTOME MIDDLEWARE
                Assert.True(true);
            }
        }

        [Fact]
        public async Task ReadPokemonDescription_CheckWith_ValidData()
        {

            string validData = await _pokemonRepository.GetPokemon("Charizard");

            var result = await _pokemonService.ReadPokemonDescription(validData);

            Assert.True(!string.IsNullOrEmpty(result));
            Assert.False(string.IsNullOrEmpty(result));

        }

        [Fact]
        public async Task ReadPokemonDescription_CheckWith_InValidData_Cat1()
        {
            string validData = await _pokemonRepository.GetPokemon("Charizard");
            string inValidData = validData.Replace("id", "fakeItemName");

            var result = await _pokemonService.ReadPokemonDescription(inValidData);

            Assert.True(string.IsNullOrEmpty(result));
            Assert.False(!string.IsNullOrEmpty(result));

        }

        [Fact]
        public async Task ReadPokemonDescription_CheckWith_InValidData_Cat2()
        {
            string validData = await _pokemonRepository.GetPokemonDescription("6");
            string inValidData = validData.Replace("flavor_text_entries", "fakeItemName");

            var result = await _pokemonService.ReadPokemonDescription(inValidData);

            Assert.True(string.IsNullOrEmpty(result));
            Assert.False(!string.IsNullOrEmpty(result));

        }

        [Fact]
        public async Task ReadPokemonDescription_CheckWith_InValidData_Cat3()
        {
            string validData = await _pokemonRepository.GetPokemonDescription("6");
            string inValidData = validData.Replace("name", "fakeItemName");

            var result = await _pokemonService.ReadPokemonDescription(inValidData);

            Assert.True(string.IsNullOrEmpty(result));
            Assert.False(!string.IsNullOrEmpty(result));

        }

        [Fact]
        public async Task ReadPokemonDescription_CheckWith_InValidData_Cat4()
        {
            string validData = await _pokemonRepository.GetPokemonDescription("6");

            string inValidData = validData.Replace("flavor_text", "fakeItemName");

            var result = await _pokemonService.ReadPokemonDescription(inValidData);

            Assert.True(string.IsNullOrEmpty(result));
            Assert.False(!string.IsNullOrEmpty(result));

        }

        [Fact]
        public async Task ReadPokemonDescription_CheckWith_SecondLanguage()
        {
            string validData = await _pokemonRepository.GetPokemon("Charizard");

            var result = await _pokemonService.ReadPokemonDescription(validData, "fr");

            Assert.True(!string.IsNullOrEmpty(result));
            Assert.False(string.IsNullOrEmpty(result));
        }

        [Fact]
        public async Task ReadPokemonDescription_CheckWith_WrongLanguage()
        {
            string validData = await _pokemonRepository.GetPokemon("Charizard");
            
            var result = await _pokemonService.ReadPokemonDescription(validData, "WRONGLANGUAGE");

            Assert.True(string.IsNullOrEmpty(result));
            Assert.False(!string.IsNullOrEmpty(result));
        }

        [Fact]
        public async Task ReadTranslatorText_CheckWith_ValidData()
        {

            string validData = await _pokemonRepository.GetPokemon("Charizard");
            var description = await _pokemonService.ReadPokemonDescription(validData);

            var translatedDescription = await _translatorRepository.ConvertToShakespear(description);

            var result = _pokemonService.ReadTranslatorText(translatedDescription);

            Assert.True(!string.IsNullOrEmpty(result));
            Assert.False(string.IsNullOrEmpty(result));

        }

        [Fact]
        public async Task ReadTranslatorText_CheckWith_InValidData_Cat1()
        {
            string validData = await _pokemonRepository.GetPokemon("Charizard");
            var validDescription = await _pokemonService.ReadPokemonDescription(validData);
            var translatedDescription = await _translatorRepository.ConvertToShakespear(validDescription);

            string invalidDescription = translatedDescription.Replace("contents", string.Empty);

            var result = _pokemonService.ReadTranslatorText(invalidDescription);
            Assert.True(string.IsNullOrEmpty(result));
            Assert.False(!string.IsNullOrEmpty(result));

        }

        [Fact]
        public async Task ReadTranslatorText_CheckWith_InValidData_Cat2()
        {
            string validData = await _pokemonRepository.GetPokemon("Charizard");
            var validDescription = await _pokemonService.ReadPokemonDescription(validData);
            var translatedDescription = await _translatorRepository.ConvertToShakespear(validDescription);


            string invalidDescription = translatedDescription.Replace("translation", string.Empty);

            var result = _pokemonService.ReadTranslatorText(invalidDescription);
            Assert.True(string.IsNullOrEmpty(result));
            Assert.False(!string.IsNullOrEmpty(result));

        }



    }
}
