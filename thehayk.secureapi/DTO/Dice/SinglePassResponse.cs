namespace thehayk.secureapi.DTO.Dice
{
    /// <summary>
    /// Response for single password.
    /// </summary>
    public class SinglePassResponse
    {
        /// <summary>
        /// Quantity of words in each password.
        /// </summary>
        public int WordsQuantity { get; set; }

        /// <summary>
        /// Separator for words concatention in password.
        /// </summary>
        public string Separator { get; set; }

        /// <summary>
        /// The password.
        /// </summary>
        public string Password { get; set; }
    }
}
