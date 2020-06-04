using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PokemonItaly.API.Filters;
using PokemonItaly.API.Models;
using PokemonItaly.Interface.Interfaces;

namespace PokemonItaly.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PokemonController : ControllerBase
    {

        #region Declaration
        public IPokemonService _pokemonService;
        #endregion


        #region Constructor
        public PokemonController(IPokemonService pokemonService)
        {
            _pokemonService = pokemonService;
        }

        #endregion

        #region Action Methods


        [ServiceFilter(typeof(ActionFilter))]
        [HttpGet("{pokemonName}")]
        public async Task<JsonResult> GetPokemonDescription(string pokemonName)
        {
            var description = await _pokemonService.GetPokemonDescription(pokemonName);

            return new JsonResult(new PokemonDetails() { Description = description, Name = pokemonName });
        }

        #endregion
    }

}