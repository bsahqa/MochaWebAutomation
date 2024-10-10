namespace MochaHomeAccounting.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using log4net;
    using OpenQA.Selenium;

    /// <summary>
    /// Actions for capturing, storing & retrieving screenshot during execution.
    /// </summary>
    public class TakeScreenShot
    {
        private static readonly ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Capture a screenshot using the provided instance of the IWebDriver.
        /// </summary>
        /// <param name="driver">Instance of the IWebDriver being used.</param>
        /// <returns>Path of the captured screenshot.</returns>
        public static string Capture(IWebDriver driver)
        {
            try
            {
                Log.Info("Create the Screen Shot");
                ITakesScreenshot takeScreenShot = (ITakesScreenshot)driver;
                Screenshot screenshot = takeScreenShot.GetScreenshot();
                string workingDirectory = CreatedScreenshotDirectory();
                string screenShotName = "ScreenShot_" + DateTime.Now.ToFileTime();
                string finalPath = workingDirectory + "\\" + screenShotName + ".png";
                string localPath = new Uri(finalPath).LocalPath;
                screenshot.SaveAsFile(finalPath);
                Log.Info("ScreenShot Path:" + localPath);
                return localPath;
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
                return string.Empty;
            }
        }

        /// <summary>
        /// Capture screenshot & return it in the Base64Encoded string format.
        /// </summary>
        /// <param name="driver">Instance of the IWebDriver being used.</param>
        /// <returns>Captured screenshot in the base64 encoded format.</returns>
        public static string CaptureBase64(IWebDriver driver)
        {
            try
            {
                Log.Info("Create the screen shot with ITakeScreenShot");
                ITakesScreenshot takeScreenShot = (ITakesScreenshot)driver;
                Screenshot screenshot = takeScreenShot.GetScreenshot();
                return screenshot.AsBase64EncodedString;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }

            return null;
        }

        /// <summary>
        /// Create a directory for storing captured screenshots.
        /// </summary>
        /// <returns>Path of the created screenshots directory.</returns>
        private static string CreatedScreenshotDirectory()
        {
            try
            {
                string screenShotDirectory = Directory.GetCurrentDirectory() + "\\ExtentReport\\ScreenShots";
                if (!Directory.Exists(screenShotDirectory))
                {
                    Directory.CreateDirectory(screenShotDirectory);
                }

                Log.Info(screenShotDirectory);
                return screenShotDirectory;
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
            }

            return null;
        }
    }
}
