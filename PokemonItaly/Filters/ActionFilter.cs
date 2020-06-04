using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokemonItaly.API.Filters
{
    public class ActionFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.ActionArguments.Count == 0)
            {
                throw new ArgumentException("Invalid Input");
            }

            if (context.ActionArguments.TryGetValue("pokemonName", out object output))
            {
                if (output == null || string.IsNullOrEmpty(output.ToString()))
                {
                    throw new ArgumentException("Invalid Input");
                }
                if (int.TryParse(output.ToString(), out int result))
                {
                    throw new ArgumentException("Invalid Input");
                }
            }
            await next();
        }
    }
}
