namespace thehayk.secureapi.DTO.Dice
{
    /// <summary>
    /// Response for requesting multiple passwords.
    /// </summary>
    public class MultiplePassResponse
    {
        /// <summary>
        /// Quantity of passwords requested.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Quantity of words in each password.
        /// </summary>
        public int WordsQuantity { get; set; }

        /// <summary>
        /// Separator for words concatention in password.
        /// </summary>
        public string Separator { get; set; }

        /// <summary>
        /// List of passwords.
        /// </summary>
        public string[] Passwords { get; set; }
    }
}
