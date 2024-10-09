using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Authentication;
using thehayk.secureapi.security.Hash;
using thehayk.secureapi.security.Helpers;

namespace thehayk.secureapi.Controllers
{
    /// <summary>
    /// Endpoint for dice hash CRUD.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class HashController : ControllerBase
    {
        IHashProvider hashProvider {  get; set; }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="hashProvider">Injected hash provider instance.</param>
        public HashController(IHashProvider hashProvider)
        {
            this.hashProvider = hashProvider;
        }

        /// <summary>
        /// Get a list of supported hash algorithms.
        /// </summary>
        /// <response code="200">Ok.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="500">Internal server error.</response>
        /// <returns></returns>
        [HttpGet]
        [Route("Algos")]
        public string[] GetAlgos()
        { 
            return hashProvider.GetAlgorithms();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Algorithm"></param>
        /// <param name="Data">Byte array. Must be not null, length should be in [1, 8192] range.</param>
        /// <response code="200">Ok.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="500">Internal server error.</response>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        [HttpPost]
        public string GetHash([FromQuery][Required] string Algorithm, [FromBody][Required] byte[] Data)
        {
            if(Data == null || Data.Length < 1 || Data.Length > 8192)
                throw new ArgumentNullException(nameof(Data));

            HashAlgorithmType hashAlgorithmType = hashProvider.GetHashAlgorithmType(Algorithm);
            byte[] hash = hashProvider.ComputeHash(hashAlgorithmType, Data);
            
            return hash.ToHexString();
        }
    }
}
