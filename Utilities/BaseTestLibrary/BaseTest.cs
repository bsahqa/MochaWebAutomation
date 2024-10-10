namespace MochaHomeAccounting.Utilities.BaseTestLibrary
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AventStack.ExtentReports;
    using log4net;
    using NUnit.Framework;

    /// <summary>
    /// Base Test class for both API & UI Tests.
    /// </summary>
    public class BaseTest
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static int passTestCaseCount = 0;
        private static int failedTestCaseCount = 0;
        protected ExtentTest extentTest;
        protected BaseTestContext baseTestContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseTest"/> class.
        /// </summary>
        public BaseTest()
        {
        }

        /// <summary>
        /// Gets or Sets the TestContext Object.
        /// </summary>
        public TestContext TestContext { get; set; }

        /// <summary>
        /// Initialize the instance of ExtentReports for each individual test being executed.
        /// </summary>
        [SetUp]
        public void BaseTestInitialize()
        {
            try
            {
                log4net.ThreadContext.Properties["Guid"] = Guid.NewGuid().ToString();
                this.extentTest = ExtentReporting.GetExtentReports().CreateTest(TestContext.CurrentContext.Test.Name, this.GetTestDescription()).AssignCategory(this.GetTestCategories());
                this.LogInfoMessage(Log, $"************************************* Base Class Initialized block starts *************************");
            }
            catch (Exception ex)
            {
                this.LogErrorMessage(Log, ex.ToString());
            }

            this.LogInfoMessage(Log, $"************************************* Base Class Initialized block ended *************************");
        }

        /// <summary>
        /// Logging details to Information level logs.
        /// </summary>
        /// <param name="log">Instace of ILog.</param>
        /// <param name="message">Content of the message to be logged.</param>
        public virtual void LogInfo(ILog log, string message)
        {
            TestContext.WriteLine(message);
            log.Info(message);
        }

        /// <summary>
        /// Logging details to Error level logs.
        /// </summary>
        /// <param name="log">Instance of the logger object.</param>
        /// <param name="message">Content of the message to be logged.</param>
        public virtual void LogError(ILog log, string message)
        {
            TestContext.WriteLine(message);
            log.Error(message);
        }

        /// <summary>
        /// Logging details to Information level logs & Extent report object.
        /// </summary>
        /// <param name="log">Instance of the logger object.</param>
        /// <param name="message">Content of the message to be logged.</param>
        public virtual void LogInfoMessage(ILog log, string message)
        {
            TestContext.WriteLine(message);
            log.Info(message);
            this.extentTest.Info(message);
        }

        /// <summary>
        /// Overload method for logging details to Information level logs & Extent report object along with screenshot.
        /// </summary>
        /// <param name="log">Instance of the logger object.</param>
        /// <param name="message">Content of the message to be logged.</param>
        /// <param name="base64String">Base64 string containing for the screenshot image.</param>
        public virtual void LogInfoMessage(ILog log, string message, string base64String)
        {
            TestContext.WriteLine(message);
            log.Info(message);
            this.extentTest.Info(message, this.GetMediaEntityBuilder(base64String));
        }

        /// <summary>
        /// Logging details to Error level logs & Extent report object.
        /// </summary>
        /// <param name="log">Instance of the logger object.</param>
        /// <param name="message">Content of the message to be logged.</param>
        public virtual void LogErrorMessage(ILog log, string message)
        {
            TestContext.WriteLine(message);
            log.Error(message);
            this.extentTest.Fail(message);
        }

        /// <summary>
        /// Logging Failure details to logs & Extent report object along with the captured screenshot.
        /// </summary>
        /// <param name="log">Instance of the logger object.</param>
        /// <param name="message">Content of the message to be logged.</param>
        public virtual void LogFailureMessage(ILog log, string message)
        {
            TestContext.WriteLine(message);
            TestContext.AddTestAttachment(TakeScreenShot.Capture(this.baseTestContext.Driver));
            log.Error(message);
            this.extentTest.Fail(message, this.GetMediaEntityBuilder(TakeScreenShot.CaptureBase64(this.baseTestContext.Driver)));
            Assert.Fail(message);
        }

        /// <summary>
        /// Logging Failure details to logs & Extent report object along without screenshot.
        /// This method can be particularly useful for logging failrues while running API tests.
        /// </summary>
        /// <param name="log">Instance of the logger object.</param>
        /// <param name="message">Content of the message to be logged.</param>
        public virtual void LogFailureMessageWoScreenshot(ILog log, string message)
        {
            TestContext.WriteLine(message);
            log.Error(message);
            this.extentTest.Fail(message);
            Assert.Fail(message);
        }

        /// <summary>
        /// Log Failure details to logs & Extent report object without any screenshot.
        /// </summary>
        /// <param name="log">Instance of the logger object.</param>
        /// <param name="message">Content of the message to be logged.</param>
        public virtual void LogFailure(ILog log, string message)
        {
            TestContext.WriteLine(message);
            log.Error(message);
            this.extentTest.Fail(message);
        }

        /// <summary>
        /// Log Success message to logs & Extent report object without any screenshot.
        /// </summary>
        /// <param name="log">Instance of the logger object.</param>
        /// <param name="message">Content of the message to be logged.</param>
        public virtual void LogSuccessMessage(ILog log, string message)
        {
            TestContext.WriteLine(message);
            log.Info(message);
            this.extentTest.Pass(message);
        }

        /// <summary>
        /// Dispose the objects of WebDriver & BaseTestContext object after completing test execution.
        /// </summary>
        public virtual void DisposeDriver()
        {
            try
            {
                this.LogInfoMessage(Log, "Disposing the WebDriver Object", TakeScreenShot.CaptureBase64(this.baseTestContext.Driver));
                this.LogTestOutCome();
                Log.Info("Driver is getting closed");
                this.baseTestContext.Driver.Close();
                Log.Info("Driver is closed");
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
            }
            finally
            {
                this.baseTestContext.Driver.Quit();
            }
        }

        /// <summary>
        /// Build Image from the provided Base64 Image string.
        /// </summary>
        /// <param name="base64String">Input Base64 string to be converted into Image.</param>
        /// <returns>Image Media converted from the Base64 Image.</returns>
        public MediaEntityModelProvider GetMediaEntityBuilder(string base64String)
        {
            try
            {
                return MediaEntityBuilder.CreateScreenCaptureFromBase64String(base64String).Build();
            }
            catch (Exception ex)
            {
                Log.Info(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Logging the outcome of tests after execution.
        /// </summary>
        public void LogTestOutCome()
        {
            string message = TestContext.CurrentContext.Test.Name + " is " + TestContext.CurrentContext.Result.Outcome.Status.ToString();

            switch (TestContext.CurrentContext.Result.Outcome.Status.ToString())
            {
                case "Passed":
                    this.LogSuccessMessage(Log, message);
                    break;
                case "Inconclusive":
                    this.LogInfoMessage(Log, message);
                    break;
                default:
                    this.LogFailure(Log, message);
                    break;
            }
        }

        /// <summary>
        /// Common test cleanup to be executed after each test execution & Log the output to ExtentReports & Log files.
        /// </summary>
        [TearDown]
        public void BaseTestCleanUp()
        {
            try
            {
                this.LogInfoMessage(Log, "************************************* base class cleanup block started *************************");
                if (TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Passed)
                {
                    passTestCaseCount++;
                }
                else
                {
                    failedTestCaseCount++;
                }

                this.PrintExecutionStatus();
                ExtentReporting.FlushExtentObject();
            }
            catch (Exception ex)
            {
                this.LogErrorMessage(Log, "Base Cleanup Exception :" + ex);
            }
            finally
            {
                this.LogInfoMessage(Log, "************************************* base class cleanup block Ends *************************");
            }
        }

        /// <summary>
        /// Pre-formatted message content to print execution outcomes after test execution.
        /// </summary>
        protected void PrintExecutionStatus()
        {
            this.LogInfoMessage(Log, "******************** Current Execution Outcome **************************");
            this.LogInfoMessage(Log, "*************************************************************************");
            this.LogInfoMessage(Log, "Total Tests Executed: " + (passTestCaseCount + failedTestCaseCount));
            this.LogInfoMessage(Log, "Total Tests Passed: " + passTestCaseCount);
            this.LogInfoMessage(Log, "Total Tests Failed: " + failedTestCaseCount);
            this.LogInfoMessage(Log, "*************************************************************************");
        }

        /// <summary>
        /// Retrieve the Description attribute details for each test being executed.
        /// </summary>
        /// <returns>Retrieved value of the Description attribute.</returns>
        protected string GetTestDescription()
        {
            var currentClassType = this.GetType().Assembly.GetTypes().FirstOrDefault(f => f.FullName == TestContext.CurrentContext.Test.ClassName);
            var currentMethod = currentClassType.GetMethod(TestContext.CurrentContext.Test.Name);
            string description = ((DescriptionAttribute[])currentMethod.GetCustomAttributes(typeof(DescriptionAttribute), true))[0].Properties["Description"][0].ToString();
            return string.IsNullOrEmpty(description) ? TestContext.CurrentContext.Test.Name : description;
        }

        /// <summary>
        /// Retrieve the Category attribute details for each test being executed.
        /// </summary>
        /// <returns>Retrieved value of the Category attribute.</returns>
        private string[] GetTestCategories()
        {
            var currentClassType = this.GetType().Assembly.GetTypes().FirstOrDefault(f => f.FullName == TestContext.CurrentContext.Test.ClassName);
            var currentMethod = currentClassType.GetMethod(TestContext.CurrentContext.Test.Name);
            var requiredCat = (from attribute in (IEnumerable<CategoryAttribute>)currentMethod.GetCustomAttributes(typeof(CategoryAttribute), true)
                               where !attribute.Name.ToString().Contains("Smoke")
                               select attribute.Name.ToString()).ToArray();

            return requiredCat.Length > 0 ? requiredCat : new string[] { "Unassigned" };
        }
    }
}