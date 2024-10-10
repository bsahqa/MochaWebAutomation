namespace MochaHomeAccounting.Utilities
{
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// Establish & Initialize a Databse connection using the provided configuration details.
    /// </summary>
    public class DataBaseConnection
    {
        private IConfiguration iconfig;
        private string environment;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataBaseConnection"/> class.
        /// </summary>
        public DataBaseConnection()
        {
            this.environment = new TestConfiguration().GetConfigurationValue("environment");
            this.iconfig = TestConfiguration.GetConfigurationRoot("Database", "Database");
        }

        /// <summary>
        /// Retrieve the value of the section containing the DB configuration details from the Config file.
        /// </summary>
        /// <param name="configKey">Key for the Configuration name to be fetched from the Config file.</param>
        /// <returns>Retrieved configuration value from the Config file.</returns>
        public string GetConfigurationValue(string configKey)
        {
            return this.iconfig.GetSection(environment).GetSection(configKey).Value;
        }
    }
}
