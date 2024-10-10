namespace MochaHomeAccounting.Utilities
{
    using AventStack.ExtentReports;
    using log4net;
    using NUnit.Framework;
    using OpenQA.Selenium;

    /// <summary>
    /// 
    /// </summary>
    public class BaseTestContext
    {
        private static readonly ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseTestContext"/> class.
        /// </summary>
        /// <param name="driver">Parameter for IWebDriver object.</param>
        /// <param name="extentTest">Parameter for ExtentTest object.</param>
        /// <param name="testContext">Parameter for TestContext object.</param>
        public BaseTestContext(IWebDriver driver, ExtentTest extentTest, TestContext testContext)
        {
            this.Driver = driver;
            this.ExtentTest = extentTest;
            this.TestContext = testContext;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseTestContext"/> class.
        /// </summary>
        /// <param name="extentTest">Parameter for ExtentTest object.</param>
        /// <param name="testContext">Parameter for TestContext object.</param>
        public BaseTestContext(ExtentTest extentTest, TestContext testContext)
        {
            this.ExtentTest = extentTest;
            this.TestContext = testContext;
        }

        /// <summary>
        /// Gets or sets the IWebDriver Instance.
        /// </summary>
        public IWebDriver Driver { get; set; }

        /// <summary>
        /// Gets or sets the ExtentTest Instance.
        /// </summary>
        public ExtentTest ExtentTest { get; set; }

        /// <summary>
        /// Gets or sets the TestContext instace.
        /// </summary>
        public TestContext TestContext { get; set; }
    }
}
