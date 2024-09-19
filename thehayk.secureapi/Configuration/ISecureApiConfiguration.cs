namespace thehayk.secureapi.Configuration
{
    /// <summary>
    /// Interface for API configuation.
    /// </summary>
    public interface ISecureApiConfiguration
    {
        /// <summary>
        /// If basic authentication for API is turned on,
        /// </summary>
        bool BasicAuthIsOn { get; }

        /// <summary>
        /// API's basic authentication username.
        /// </summary>
        string BasicAuthUsername { get; }

        /// <summary>
        /// API's basic authentication password.
        /// </summary>
        string BasicAuthPassword { get; }

        /// <summary>
        /// Swagger's basic authentication username..
        /// </summary>
        string SwaggerUsername { get; }

        /// <summary>
        /// Swagger's basic authentication password.
        /// </summary>
        string SwaggerPassword { get; }

        /// <summary>
        /// Path of word dictionary file.
        /// </summary>
        string DictionaryFilePath { get; }

        /// <summary>
        /// Initializes configuration of API.
        /// </summary>
        void Init();
    }
}
