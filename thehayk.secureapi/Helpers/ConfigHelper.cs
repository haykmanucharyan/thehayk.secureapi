namespace thehayk.secureapi.Helpers
{
    /// <summary>
    /// Helper class for configuration of API.
    /// </summary>
    public static class ConfigHelper
    {
        private static string GetEnvVar(string varName)
        {
            return Environment.GetEnvironmentVariable(varName);
        }

        /// <summary>
        /// Gets environment variable converted to string with default value.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <param name="defaultValue">Default string value in case of variable is empty or null.</param>
        /// <returns></returns>
        public static int GetEnvVarInt(string name, int defaultValue)
        {
            string var = GetEnvVar(name);

            if (string.IsNullOrEmpty(var))
                return defaultValue;

            if (!int.TryParse(var, out int res))
                return defaultValue;

            return res;
        }

        /// <summary>
        /// Gets environment variable converted to Int32 with default value.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <param name="defaultValue">Default Int32 value in case of variable is empty or null.</param>
        /// <returns></returns>
        public static string GetEnvVarString(string name, string defaultValue)
        {
            string var = GetEnvVar(name);

            if (string.IsNullOrEmpty(var))
                return null;

            return var;
        }

        /// <summary>
        /// Gets environment variable converted to boolean with default value.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <param name="defaultValue">Default string value in case of variable is empty or null.</param>
        /// <returns></returns>
        public static bool GetEnvVarBool(string name, bool defaultValue)
        {
            string var = GetEnvVar(name);

            if (string.IsNullOrEmpty(var))
                return defaultValue;

            if (!bool.TryParse(var, out bool res))
                return defaultValue;

            return res;
        }
    }
}
