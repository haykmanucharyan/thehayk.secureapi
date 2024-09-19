using thehayk.secureapi.Helpers;

namespace thehayk.secureapi.Configuration
{
    /// <summary>
    /// Class for API configuation.
    /// </summary>
    public class SecureApiConfiguration : ISecureApiConfiguration
    {
        /// <summary>
        /// If basic authentication for API is turned on,
        /// </summary>
        public bool BasicAuthIsOn { get; private set; }

        /// <summary>
        /// API's basic authentication username.
        /// </summary>
        public string BasicAuthUsername { get; private set; }

        /// <summary>
        /// API's basic authentication password.
        /// </summary>
        public string BasicAuthPassword { get; private set; }

        /// <summary>
        /// Swagger's basic authentication username..
        /// </summary>
        public string SwaggerUsername { get; private set; }

        /// <summary>
        /// Swagger's basic authentication password.
        /// </summary>
        public string SwaggerPassword { get; private set; }

        /// <summary>
        /// Path of word dictionary file.
        /// </summary>
        public string DictionaryFilePath { get; private set; }

        /// <summary>
        /// Initializes configuration of API.
        /// </summary>
        /// <exception cref="ArgumentNullException">In case of bad arguments (conditional).</exception>
        public void Init()
        {
            BasicAuthIsOn = ConfigHelper.GetEnvVarBool("BASIC_AUTH_IS_ON", false);

            BasicAuthUsername = ConfigHelper.GetEnvVarString("BASIC_AUTH_USER", null);
            if (BasicAuthIsOn && string.IsNullOrEmpty(BasicAuthUsername))
                throw new ArgumentNullException(nameof(BasicAuthUsername));

            BasicAuthPassword = ConfigHelper.GetEnvVarString("BASIC_AUTH_PASS", null);
            if (BasicAuthIsOn && string.IsNullOrEmpty(BasicAuthPassword))
                throw new ArgumentNullException(nameof(BasicAuthPassword));

            SwaggerUsername = ConfigHelper.GetEnvVarString("SWAGGER_USER", "root");
            SwaggerPassword = ConfigHelper.GetEnvVarString("SWAGGER_PASS", "pass");

            DictionaryFilePath = ConfigHelper.GetEnvVarString("DICT_FILE", null);

            if (string.IsNullOrEmpty(DictionaryFilePath))
                throw new ArgumentNullException(nameof(DictionaryFilePath));

            if (!File.Exists(DictionaryFilePath))
                throw new FileNotFoundException($"{nameof(DictionaryFilePath)}: DictionaryFilePath");
        }
    }
}
