namespace MochaHomeAccounting.Utilities.WebDriverLibraries
{
    using log4net;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.Events;

    /// <summary>
    /// Generate and return a WebDriver object for the provided browser name.
    /// </summary>
    public class WebDriverLibrary
    {
        private static readonly ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly string browser;

        /// <summary>
        /// Initializes a new instance of the <see cref="WebDriverLibrary"/> class.
        /// </summary>
        public WebDriverLibrary()
        {
            this.browser = "Chrome";
        }

        /// <summary>
        /// Intialize an instance of EventFiringWebdriver for the provided browser name.
        /// </summary>
        /// <returns>Object of type EventFiringWebDriver.</returns>
        public IWebDriver? GetWebDriver()
        {
            Log.Info("The WebDriver for " + this.browser);
            switch (this.browser.ToLower())
            {
                case "chrome":
                    return this.GetEventFiringListener(new ChromeDriverLib().GetChromeDriver());
                default:
                    Log.Info("No browser specified");
                    break;
            }

            return null;
        }

        /// <summary>
        /// Initializes EventFiringListener for the provided instance of IWebDriver.
        /// </summary>
        /// <param name="driver">Instace of IWebDriver.</param>
        /// <returns>Instance of EventFiringWebDriver for the instance of IWebDriver.</returns>
        public IWebDriver GetEventFiringListener(IWebDriver driver)
        {
            EventFiringWebDriver eventFiringWebDriver = new (driver);
            return eventFiringWebDriver;
        }
    }
}
