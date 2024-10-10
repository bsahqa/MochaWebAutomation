namespace MochaHomeAccounting.Utilities.WebDriverLibraries
{
    using System;
    using log4net;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;

    /// <summary>
    /// Generate and return a ChromeDriver object.
    /// </summary>
    public class ChromeDriverLib
    {
        private static ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Retrieve ChromeDriver object.
        /// </summary>
        /// <returns>ChromeDriver Object.</returns>
        public IWebDriver GetChromeDriver()
        {
            IWebDriver driver;
            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("test-type");
            chromeOptions.AddArgument("--allow-running-insecure-content");
            chromeOptions.AddArgument("--disable-extensions");
            chromeOptions.AddArgument("--ignore-certificate-errors");
            chromeOptions.AddArgument("--no-sanbox");
            chromeOptions.AddArgument("--start-maximized");
            chromeOptions.AddArgument("disable-extensions");
            chromeOptions.AddArgument("--incognito");
            chromeOptions.AddArgument("--start-maximized");
            chromeOptions.AddExcludedArgument("enable-automation");
            chromeOptions.AddArguments("--disable-gpu");
            chromeOptions.AddArgument("force-device-scale-factor=0.70");
            driver = new ChromeDriver(ChromeDriverService.CreateDefaultService(), chromeOptions, TimeSpan.FromMinutes(3));
            driver.Manage().Window.Maximize();
            log.Info("Returning Chrome Driver object");
            return driver;
        }
    }
}
