﻿using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using thehayk.secureapi.security.Dice;

namespace thehayk.secureapi.Controllers
{
    /// <summary>
    /// Endpoint for dice password(s) CRUD.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class DicePasswordController : ControllerBase
    {
        IDiceDictionary diceDictionary;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="diceDictionary">Injected dice dictionary.</param>
        public DicePasswordController(IDiceDictionary diceDictionary)
        {
            this.diceDictionary = diceDictionary;
        }

        /// <summary>
        /// Gets a single dice password.
        /// </summary>
        /// <param name="WordsQuantity">Quantity of random words in password. Mandatory param. Should be in [1, 50] range.</param>
        /// <param name="Separator">Separator string for password. Words will be concatenated with this separator. Optional param, default is a single space.</param>
        /// <response code="200">Ok.</response>
        /// <response code="400">BadRequest.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="500">Internal server error.</response>
        /// <returns></returns>
        [HttpGet]
        public string Get([FromQuery][Required] int WordsQuantity, [FromQuery] string Separator)
        {
            if (WordsQuantity < 1 || WordsQuantity > 50)
                throw new ArgumentOutOfRangeException(nameof(WordsQuantity));

            return diceDictionary.GetPassword(WordsQuantity, Separator);
        }

        /// <summary>
        /// Get a list of password by given quantity.
        /// </summary>
        /// <param name="WordsQuantity">Quantity of random words in password. Mandatory param. Should be in [1, 50] range. </param>
        /// <param name="Separator"></param>
        /// <param name="Quantity">>Quantity of passwords. Mandatory param. Should be in [2, 100] range.</param>
        /// <response code="200">Ok.</response>
        /// <response code="400">BadRequest.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="500">Internal server error.</response>
        /// <returns></returns>
        [HttpGet]
        [Route("List")]
        public string[] List([FromQuery][Required] int WordsQuantity, [FromQuery] string Separator, [FromQuery][Required] int Quantity)
        {
            if (Quantity < 2 || Quantity > 100)
                throw new ArgumentOutOfRangeException(nameof(Quantity));

            if (WordsQuantity < 1 || WordsQuantity > 50)
                throw new ArgumentOutOfRangeException(nameof(WordsQuantity));

            return diceDictionary.GetPasswords(WordsQuantity, Separator, Quantity);
        }
    }
}
