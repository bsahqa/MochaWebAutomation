namespace MochaHomeAccounting.Utilities.BaseTestLibrary
{
    using NUnit.Framework;
    using MochaHomeAccounting.Utilities.WebDriverLibraries;

    /// <summary>
    /// Base Test class for UI Tests.
    /// </summary>
    public class BaseUITest : BaseTest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseUITest"/> class.
        /// </summary>
        public BaseUITest()
        {
        }

        /// <summary>
        /// Initialize the Base Test Context object for UI Tests.
        /// </summary>
        [SetUp]
        public void BaseUiTestInitialize()
        {
            this.baseTestContext = new BaseTestContext(new WebDriverLibrary().GetWebDriver(), this.extentTest, TestContext.CurrentContext);
        }
    }
}
