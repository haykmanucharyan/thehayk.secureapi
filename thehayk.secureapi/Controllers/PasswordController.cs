using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using thehayk.secureapi.security.Password;

namespace thehayk.secureapi.Controllers
{
    /// <summary>
    /// Endpoint for password(s) CRUD.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class PasswordController : ControllerBase
    {
        IPasswordProvider passwordProvider;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="passwordProvider">Injected password provider instance.</param>
        public PasswordController(IPasswordProvider passwordProvider)
        { 
            this.passwordProvider = passwordProvider;
        }

        /// <summary>
        /// Get reference of special chars.
        /// </summary>
        /// <response code="200">Ok.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="500">Internal server error.</response>
        /// <returns></returns>
        [HttpGet]
        [Route("Specials")]
        public char[] GetSpecialChars()
        { 
            return passwordProvider.GetSpecialChars();
        }

        /// <summary>
        /// Get a single password.
        /// </summary>
        /// <param name="Length">Length of password. Mandatory param. Should be in [6, 256] range.</param>
        /// <param name="IncludeUppercase">If password should include uppercase letters. Optional param. Default is false.</param>
        /// <param name="IncludeNumbers">If password should include numbers. Optional param. Default is false.</param>
        /// <param name="IncludeSpecials">If password should include special characters. Optional param. Default is false.</param>
        /// <response code="200">Ok.</response>
        /// <response code="400">BadRequest.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="500">Internal server error.</response>
        /// <returns></returns>
        [HttpGet]
        public string GetPassword([FromQuery][Required] int Length, [FromQuery] bool IncludeUppercase, 
            [FromQuery] bool IncludeNumbers, [FromQuery] bool IncludeSpecials)
        {
            if (Length < 6 || Length > 256)
                throw new ArgumentOutOfRangeException(nameof(Length));

            return passwordProvider.GetPassword(Length, IncludeUppercase, IncludeNumbers, IncludeSpecials);
        }

        /// <summary>
        /// Get a list of passwords.
        /// </summary>
        /// <param name="Quantity">Quantity of passwords. Mandatory param. Should be in [2, 1.000] range.</param>
        /// <param name="Length">Length of password. Mandatory param. Should be in [6, 256] range.</param>
        /// <param name="IncludeUppercase">If password should include uppercase letters. Optional param. Default is false.</param>
        /// <param name="IncludeNumbers">If password should include numbers. Optional param. Default is false.</param>
        /// <param name="IncludeSpecials">If password should include special characters. Optional param. Default is false.</param>
        /// <response code="200">Ok.</response>
        /// <response code="400">BadRequest.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="500">Internal server error.</response>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        [HttpGet]
        [Route("List")]
        public string[] GetPasswords([FromQuery][Required] int Quantity, [FromQuery][Required] int Length, 
            [FromQuery] bool IncludeUppercase, [FromQuery] bool IncludeNumbers, [FromQuery] bool IncludeSpecials)
        {
            if (Quantity < 2 || Quantity > 1_000)
                throw new ArgumentOutOfRangeException(nameof(Quantity));

            if (Length < 6 || Length > 256)
                throw new ArgumentOutOfRangeException(nameof(Length));

            return passwordProvider.GetPasswords(Length, IncludeUppercase, IncludeNumbers, IncludeSpecials, Quantity);
        }
    }
}
