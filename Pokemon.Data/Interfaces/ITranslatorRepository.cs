using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PokemonItaly.Data.Interfaces
{
    public interface ITranslatorRepository
    {
        /// <summary>
        /// Signature to Convert English to Shakespeares Style
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        Task<string> ConvertToShakespear(string inputString);
    }
}
