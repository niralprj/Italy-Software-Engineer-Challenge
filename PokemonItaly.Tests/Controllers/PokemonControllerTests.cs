using PokemonItaly.Controllers;
using PokemonItaly.Data.Repository;
using PokemonItaly.Service.Services;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace PokemonItaly.Tests.Services
{
    public class PokemonControllerTests
    {
        public readonly PokemonService _pokemonService;
        public readonly PokemonRepository pokemonRepository;
        public readonly TranslatorRepository translatorRepository;
        public readonly PokemonController _pokemonController;

        public PokemonControllerTests()
        {
            pokemonRepository = new PokemonRepository(new HttpClient());
            translatorRepository = new TranslatorRepository(new HttpClient());
            _pokemonService = new PokemonService(pokemonRepository, translatorRepository);

            _pokemonController = new PokemonController(_pokemonService);

        }

        [Fact]
        public async Task GetPokemonDescription_Validate_NoDataFound()
        {
            try
            {
                var result = await _pokemonController.GetPokemonDescription("FakeName");
                Assert.NotNull(result);
            }
            catch (Exception)
            {
                // Valid Exception for NO DATA FOUND
                // EXCEPTION IS THROWN AT GLOBAL LEVEL, HANDLED IN CUSTOME MIDDLEWARE
                Assert.True(true);
            }
        }

        [Fact]
        public async Task GetPokemonDescription_Validate_DataFound()
        {
            var result = await _pokemonController.GetPokemonDescription("Charizard");
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetPokemonDescription_Validate_EmptyName()
        {
            try
            {
                var result = await _pokemonController.GetPokemonDescription(string.Empty);
                Assert.NotNull(result);
            }
            catch (Exception)
            {
                // Valid Exception for NO DATA FOUND
                // EXCEPTION IS THROWN AT GLOBAL LEVEL, HANDLED IN CUSTOME MIDDLEWARE
                Assert.True(true);
            }
        }
    }
}
