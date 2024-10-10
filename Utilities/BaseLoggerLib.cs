namespace MochaHomeAccounting.Utilities
{
    using System;
    using AventStack.ExtentReports;
    using AventStack.ExtentReports.MarkupUtils;
    using log4net;
    using NUnit.Framework;

    /// <summary>
    /// Base method to handle the implementation of logging mechanism.
    /// </summary>
    public class BaseLoggerLib
    {
        private static ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseLoggerLib"/> class.
        /// </summary>
        /// <param name="baseTextContext">Instance of BaseTestContext object.</param>
        public BaseLoggerLib(BaseTestContext baseTextContext)
        {
            this.BaseTestContext = baseTextContext;
        }

        /// <summary>
        /// Gets or sets the value of BaseTestContext object.
        /// </summary>
        public BaseTestContext BaseTestContext { get; set; }

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
        /// Logging details to Information level logs & Extent report object.
        /// </summary>
        /// <param name="log">Instance of the logger object.</param>
        /// <param name="message">Content of the message to be logged.</param>
        public virtual void LogInfoMessage(ILog log, string message)
        {
            TestContext.WriteLine(message);
            log.Info(message);
            this.BaseTestContext.ExtentTest.Info(message);
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
            this.BaseTestContext.ExtentTest.Info(message, GetMediaEntityBuilder(base64String));
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
        /// Logging details to Error level logs & Extent report object.
        /// </summary>
        /// <param name="log">Instance of the logger object.</param>
        /// <param name="message">Content of the message to be logged.</param>
        public virtual void LogErrorMessage(ILog log, string message)
        {
            TestContext.WriteLine(message);
            log.Error(message);
            this.BaseTestContext.ExtentTest.Fail(message);
        }

        /// <summary>
        /// Logging Failure details to logs & Extent report object along with the captured screenshot.
        /// </summary>
        /// <param name="log">Instance of the logger object.</param>
        /// <param name="message">Content of the message to be logged.</param>
        public virtual void LogFailureMessage(ILog log, string message)
        {
            TestContext.WriteLine(message);
            TestContext.AddTestAttachment(TakeScreenShot.Capture(BaseTestContext.Driver));
            log.Error(message);
            this.BaseTestContext.ExtentTest.Fail(message, GetMediaEntityBuilder(TakeScreenShot.CaptureBase64(BaseTestContext.Driver)));
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
            this.BaseTestContext.ExtentTest.Fail(message);
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
            this.BaseTestContext.ExtentTest.Fail(message);
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
            this.BaseTestContext.ExtentTest.Pass(message);
        }

        /// <summary>
        /// Log XML block into Information level logs.
        /// </summary>
        /// <param name="log">Instance of the logger object.</param>
        /// <param name="message">Content of the message to be logged.</param>
        /// <param name="xmlString">XML content to be added to the log.</param>
        public virtual void LogInfoXMLBlock(ILog log, string message, string xmlString)
        {
            log.Info(message + xmlString);
            this.BaseTestContext.ExtentTest.Info(message);
            this.BaseTestContext.ExtentTest.Info(MarkupHelper.CreateCodeBlock(xmlString, CodeLanguage.Xml));
            TestContext.WriteLine(message + xmlString);
        }

        /// <summary>
        /// Log XML block for Success instance of Extent report object.
        /// </summary>
        /// <param name="log">Instance of the logger object.</param>
        /// <param name="message">Content of the message to be logged.</param>
        /// <param name="xmlString">XML content to be added to the log.</param>
        public virtual void LogPassXMLBlock(ILog log, string message, string xmlString)
        {
            log.Info(message + xmlString);
            this.BaseTestContext.ExtentTest.Pass(message);
            this.BaseTestContext.ExtentTest.Pass(MarkupHelper.CreateCodeBlock(xmlString, CodeLanguage.Xml));
            TestContext.WriteLine(message + xmlString);
        }

        /// <summary>
        /// Log XML block for Failure instance of Extent report object.
        /// </summary>
        /// <param name="log">Instance of the logger object.</param>
        /// <param name="message">Content of the message to be logged.</param>
        /// <param name="xmlString">XML content to be added to the log.</param>
        public virtual void LogFailXMLBlock(ILog log, string message, string xmlString)
        {
            log.Info(message + xmlString);
            this.BaseTestContext.ExtentTest.Fail(message);
            this.BaseTestContext.ExtentTest.Fail(MarkupHelper.CreateCodeBlock(xmlString, CodeLanguage.Xml));
            TestContext.WriteLine(message + xmlString);
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
                log.Info(ex.ToString());
                return null;
            }
        }
    }
}
