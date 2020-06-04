using PokemonItaly.Controllers;
using PokemonItaly.Tests.Services;
using Xunit;

namespace PokemonItaly.Tests
{
    public class PokemonServiceUnitTest
    {
        PokemonServiceFake _pokemonService;
        PokemonController _controller;

        public PokemonServiceUnitTest()
        {
            _pokemonService = new PokemonServiceFake();
            _controller = new PokemonController(_pokemonService);

        }
        [Fact]
        public async void Search_With_WrongName_PokemonName()
        {
            var data = await _controller.GetPokemonDescription("Test");
            //PokemonDetails details = JsonConvert.DeserializeObject<PokemonDetails>(data);
            Assert.Contains("Not Found", "");

        }


        [Fact]
        public async void Search_With_properName_PokemonName()
        {

            var data = await _controller.GetPokemonDescription("charizard");
            //Assert.NotNull(data);
        }

    }
}
