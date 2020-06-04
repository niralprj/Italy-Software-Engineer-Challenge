using Newtonsoft.Json;

namespace PokemonItaly.API.Models
{
    public class PokemonDetails
    {

        public string Name { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
