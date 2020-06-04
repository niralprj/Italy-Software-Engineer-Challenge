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
    [Route("api/[controller]")]
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
            var description = await _pokemonService.GetPokemonDescription(pokemonName.ToLower());

            return new JsonResult(new PokemonDetails() { Description = description, Name = pokemonName });
        }

        #endregion
        //public static bool VerifyControllerActionAttribute(Controller controller, Func<ActionResult> action, Type attributeType)
        //{
        //    MethodInfo methodInfo = action.Method;
        //    object[] attributes = methodInfo.GetCustomAttributes(attributeType, true);

        //    return attributes.Any(a => a.GetType() == attributeType);
        //}


    }

}