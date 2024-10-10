namespace MochaHomeAccounting.Utilities
{
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// Read and retrieve configuration level details stored on the Config file.
    /// </summary>
    class TestConfiguration
    {
        private IConfiguration config;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestConfiguration"/> class.
        /// Helper class to retrieve configuration details stored in a json file.
        /// </summary>
        public TestConfiguration()
        {
            this.config = this.GetConfigurationRoot();
        }

        /// <summary>
        /// Retrieves the entire configuration details from the provided JSON file.
        /// </summary>
        /// <returns>IConfiguration object containing the configuration details.</returns>
        public IConfiguration GetConfigurationRoot()
        {
            string directory = Directory.GetCurrentDirectory();
            var configurationBuilder = new ConfigurationBuilder();
            return new ConfigurationBuilder().SetBasePath(directory).AddJsonFile("AppSettings.json", optional: true, reloadOnChange: true)
                .Build();
        }

        /// <summary>
        /// Retrieve configuration value for the specified key from the retrieved config section.
        /// </summary>
        /// <param name="configKey">Config Key for which the value needs to be retrieved.</param>
        /// <returns>Value of the retrieve configuration key.</returns>
        public string GetConfigurationValue(string configKey)
        {
            return config.GetSection("configSetting").GetSection(configKey).Value;
        }

        /// <summary>
        /// Determine the Root of the provided the config file from the named folder.
        /// </summary>
        /// <param name="folderName">Name of the folder to scanned for JSON file.</param>
        /// <param name="jsonFileName">Name of the json file to be searched.</param>
        /// <returns>IConfiguration object containing the configuration details.</returns>
        public static IConfigurationRoot GetConfigurationRoot(string folderName, string jsonFileName = "AppSettings.json")
        {
            string homeDir = Directory.GetCurrentDirectory() + "\\" + folderName;
            return new ConfigurationBuilder().SetBasePath(homeDir).AddJsonFile(jsonFileName, optional: true, reloadOnChange: true)
            .Build();
        }
    }
}
