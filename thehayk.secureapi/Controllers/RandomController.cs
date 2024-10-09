using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using thehayk.secureapi.security.Helpers;
using thehayk.secureapi.security.Random;

namespace thehayk.secureapi.Controllers
{
    /// <summary>
    /// Endpoint for random CRUD.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class RandomController : ControllerBase
    {
        IRandomProvider randomProvider;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="randomProvider">Random provider instance.</param>
        public RandomController(IRandomProvider randomProvider)
        {
            this.randomProvider = randomProvider;
        }

        /// <summary>
        /// Get a single UID.
        /// </summary>
        /// <response code="200">Ok.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="500">Internal server error.</response>
        /// <returns></returns>
        [HttpGet]
        [Route("Uid")]
        public Guid GetGuid()
        {
            return randomProvider.Uid();
        }

        /// <summary>
        /// Get multiple UIDs.
        /// </summary>
        /// <param name="Quantity">Quantity of UID. Mandatory param. Should be in range [2, 1000]</param>
        /// <response code="200">Ok.</response>
        /// <response code="400">BadRequest.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="500">Internal server error.</response>
        /// <returns></returns>
        [HttpGet]
        [Route("Uid/List")]
        public Guid[] GetGuids([FromQuery][Required] int Quantity)
        {
            if (Quantity < 2 || Quantity > 1000)
                throw new ArgumentOutOfRangeException(nameof(Quantity));

            return randomProvider.Uids(Quantity);
        }

        /// <summary>
        /// Get a single Int32.
        /// </summary>
        /// <param name="StartInclusive">From number inclusive. Mandatory param. Should be in range [-2.147.483.648, 2.147.483.647].</param>
        /// <param name="EndExcusive">To number exclusive. Mandatory param. Should be in range [-2.147.483.648, 2.147.483.647].</param>
        /// <response code="200">Ok.</response>
        /// <response code="400">BadRequest.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="500">Internal server error.</response>
        /// <returns></returns>
        [HttpGet]
        [Route("Int32")]
        public int GetInt32([FromQuery][Required] int StartInclusive, [FromQuery][Required] int EndExcusive)
        {
            return randomProvider.GetInt32(StartInclusive, EndExcusive);
        }

        /// <summary>
        /// Get multiple UIDs.
        /// </summary>
        /// <param name="StartInclusive">From number inclusive. Mandatory param. Should be in range [-2.147.483.648, 2.147.483.647].</param>
        /// <param name="EndExcusive">To number exclusive. Mandatory param. Should be in range [-2.147.483.648, 2.147.483.647].</param>
        /// <param name="Quantity">Quantity of Int32s. Mandatory param. Should be in range [2, 5000]</param>
        /// <response code="200">Ok.</response>
        /// <response code="400">BadRequest.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="500">Internal server error.</response>
        /// <returns></returns>
        [HttpGet]
        [Route("Int32/List")]
        public int[] GetInt32s([FromQuery][Required] int StartInclusive, [FromQuery][Required] int EndExcusive, [FromQuery][Required] int Quantity)
        {
            if (Quantity < 2 || Quantity > 5_000)
                throw new ArgumentOutOfRangeException(nameof(Quantity));

            return randomProvider.GetListOfInt32(StartInclusive, EndExcusive, Quantity);
        }

        /// <summary>
        /// Get random bytes by given quantity in hexadecimal format.
        /// </summary>
        /// <param name="Quantity">Quantity of bytes. Mandatory param. Should be in range [1, 10.000].</param>
        /// <response code="200">Ok.</response>
        /// <response code="400">BadRequest.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="500">Internal server error.</response>
        /// <returns></returns>
        [HttpGet]
        [Route("Bytes")]
        public string GetBytes([FromQuery][Required] int Quantity)
        {
            if (Quantity < 1 || Quantity > 10_000)
                throw new ArgumentOutOfRangeException(nameof(Quantity));

            byte[] array = randomProvider.GetBytes(Quantity);

            return array.ToHexString();
        }
    }
}
