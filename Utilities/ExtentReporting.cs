namespace MochaHomeAccounting.Utilities
{
    using AventStack.ExtentReports;
    using AventStack.ExtentReports.Reporter;
    using log4net;

    /// <summary>
    /// Create and use extent report for storing the execution results.
    /// </summary>
    public class ExtentReporting
    {
        private static ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static ExtentReports extentReport;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtentReporting"/> class.
        /// </summary>
        private ExtentReporting()
        {
        }

        /// <summary>
        /// Create a Directory on the specified path if it doesn't exist already for storing the Extent Reports.
        /// </summary>
        /// <returns>String containing the Directory path</returns>
        public static string CreateReportDirectory()
        {
            var path = Directory.GetCurrentDirectory() + "\\ExtentReports";
            log.Info("The Extent Report directory " + path);
            if (!Directory.Exists(path))
            {
                log.Info("Creating the directory at " + path);
                Directory.CreateDirectory(path);
            }

            return path;
        }

        /// <summary>
        /// Singleton class to Create or return the ExtentReports object.
        /// </summary>
        /// <returns>Report of the type ExtentRerport.</returns>
        public static ExtentReports GetExtentReports()
        {
            log.Info("Check for Extent Report Object");
            if (extentReport == null)
            {
                log.Info("Creating the Extent Report Object");
                string reportFileName = DateTime.Now.ToString("dd_MMMM_yyyy_HH_mm_ss");
                var path = CreateReportDirectory();
                var htmlReporter = new ExtentV3HtmlReporter(path + "/ExtentReport_" + reportFileName + ".html");
                htmlReporter.Start();
                htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Dark;
                extentReport = new ExtentReports();
                extentReport.AttachReporter(htmlReporter);
            }

            return extentReport;
        }

        /// <summary>
        /// Write the contents held by ExtentReport object to report files.
        /// </summary>
        public static void FlushExtentObject()
        {
            log.Info("Flush the extent Report Object");
            extentReport.Flush();
        }
    }
}
