namespace MochaHomeAccounting.Utilities.BaseTestLibrary
{
    using NUnit.Framework;

    /// <summary>
    /// Base Test class for API Tests.
    /// </summary>
    public class BaseAPITest : BaseTest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseAPITest"/> class.
        /// </summary>
        public BaseAPITest()
        {
        }

        /// <summary>
        /// Initialize the Base Test Context object for API Tests.
        /// </summary>
        [SetUp]
        public void BaseAPITestInitialize()
        {
            this.baseTestContext = new BaseTestContext(this.extentTest, TestContext.CurrentContext);
        }
    }
}
