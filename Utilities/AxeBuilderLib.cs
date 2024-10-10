namespace MochaHomeAccounting.Utilities
{
    using AventStack.ExtentReports;
    using log4net;
    using NUnit.Framework;
    using OpenQA.Selenium;
    using Selenium.Axe;

    /// <summary>
    /// Library methods to perform Accessibility analysis on tests via AXE tool.
    /// </summary>
    public class AxeBuilderLib
    {
        private static readonly ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private AxeBuilder axeBuilder;
        private AxeResult axeResult;
        private ExtentTest extentTest;

        /// <summary>
        /// Initializes a new instance of the <see cref="AxeBuilderLib"/> class.
        /// </summary>
        /// <param name="driver">Instance of IWebDriver object.</param>
        /// <param name="extentTest">Instance of ExtentText object.</param>
        public AxeBuilderLib(IWebDriver driver, ExtentTest extentTest)
        {
            Log.Info("Initialize the AxeBuilder Library");
            this.axeBuilder = new AxeBuilder(driver);
            this.extentTest = extentTest;
        }

        /// <summary>
        /// Run AXE Analysis on the page.
        /// </summary>
        /// <param name="tags">List of AXE tags to be validated in AXE analysis</param>
        public void AnalyisePage(string tags)
        {
            string[] tagArray = tags.Split(',');
            Log.Info("Create the Axe Result Object with Axe Tags " + tagArray.ToString());
            this.axeResult = this.axeBuilder.WithTags(tagArray).Analyze();
        }

        /// <summary>
        /// Retrieve the AXE Analysis results.
        /// </summary>
        /// <returns>AXE Analysis retult.</returns>
        public AxeResult GetAxeResult()
        {
            return this.axeResult;
        }

        /// <summary>
        /// Retrieve the List of AXE violation detected in the analysis.
        /// </summary>
        /// <returns>List of detected AXE Violations</returns>
        public AxeResultItem[] GetViolations()
        {
            return this.axeResult.Violations;
        }

        /// <summary>
        /// Validate that no AXE violations were detected, assert fails if there are violations detected on the page.
        /// </summary>
        public void ValidateViolations()
        {
            Log.Info("Verify the Violations in the Page");
            Assert.That(this.axeResult.Violations.Length, Is.EqualTo(0), "There were violations detected on the page");
        }

        /// <summary>
        /// Run AXE Analysis on the page & validate that if any violations are detected.
        /// </summary>
        /// <param name="tags">List of tags to be validated within the AXE validation</param>
        public void AnalysePageForViolations(string tags)
        {
            this.AnalyisePage(tags);
            this.ValidateViolations();
            Log.Info("Violation count is :" + this.GetViolations().Length);
        }
    }
}