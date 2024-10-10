namespace MochaHomeAccounting.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using log4net;
    using NUnit.Framework;

    /// <summary>
    /// Initialize an object of SoftAssert object.
    /// SoftAsserts can be used as an alternative to Assert class, for validating non critical items such as field labels.
    /// </summary>
    public class SoftAssert : BaseLoggerLib
    {
        private static readonly ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Initializes a new instance of the <see cref="SoftAssert"/> class.
        /// </summary>
        /// <param name="baseTextContext">Object of type BaseTestContext.</param>
        public SoftAssert(BaseTestContext baseTextContext)
            : base(baseTextContext)
        {
            this.FailedAssertList = new List<string>();
            this.PassedAssertList = new List<string>();
            this.AssertMessage = "Exception '{key}': {expectedValue},Actual:{actualValue}";
        }

        private List<string> FailedAssertList { get; set; }

        private List<string> PassedAssertList { get; set; }

        private string AssertMessage { get; set; }

        /// <summary>
        /// Verify that provided expected and actual objects are equal.
        /// </summary>
        /// <param name="key">Assert Message for the instance of SoftAssert being validated.</param>
        /// <param name="expectedValue">Expected object for comparison.</param>
        /// <param name="actualValue">Actual object for comparison.</param>
        public void AreEqual(string key, object expectedValue, object actualValue)
        {
            string assertString = this.GetAssertMessage(key, expectedValue?.ToString(), actualValue?.ToString());
            try
            {
                Assert.That(actualValue, Is.EqualTo(expectedValue));
                this.PassedAssertList.Add(assertString);
            }
            catch (Exception exception)
            {
                Log.Error(exception.ToString());
                this.FailedAssertList.Add(assertString);
            }
        }

        /// <summary>
        /// Verify that provided expected object contains the value of actual object.
        /// </summary>
        /// <param name="key">Name to provided for the instance of SoftAssert validation.</param>
        /// <param name="expectedValue">Expected value to be verified.</param>
        /// <param name="actualValue">Actual value for comparison.</param>
        public void IsContains(string key, object expectedValue, object actualValue)
        {
            string assertString = this.GetAssertMessage(key, expectedValue?.ToString(), actualValue?.ToString());
            try
            {
                Assert.That(actualValue?.ToString(), Does.Contain(expectedValue?.ToString()));
                this.PassedAssertList.Add(assertString);
            }
            catch (Exception exception)
            {
                Log.Error(exception.ToString());
                this.FailedAssertList.Add(assertString);
            }
        }

        /// <summary>
        /// Verify that value of provided object does not equal to null.
        /// </summary>
        /// <param name="key">Name to provided for the instance of SoftAssert validation.</param>
        /// <param name="actualValue">Actual value for comparison.</param>
        public void IsNotNull(string key, object actualValue)
        {
            string assertString = this.GetAssertMessage(key, "Not Null", actualValue?.ToString());
            try
            {
                Assert.That(actualValue, Is.Not.Null);
                this.PassedAssertList.Add(assertString);
            }
            catch (Exception exception)
            {
                Log.Error(exception.ToString());
                this.FailedAssertList.Add(assertString);
            }
        }

        /// <summary>
        /// Verify that value of provided object does is not empty.
        /// </summary>
        /// <param name="key">Name to provided for the instance of SoftAssert validation.</param>
        /// <param name="actualValue">Actual value for comparison.</param>
        public void IsEmpty(string key, object actualValue)
        {
            string assertString = this.GetAssertMessage(key, "Empty", actualValue?.ToString());
            try
            {
                Assert.That(actualValue?.ToString(), Is.Empty);
                this.PassedAssertList.Add(assertString);
            }
            catch (Exception exception)
            {
                Log.Error(exception.ToString());
                this.FailedAssertList.Add(assertString);
            }
        }

        /// <summary>
        /// Verify that value of provided condition is true.
        /// </summary>
        /// <param name="key">Name to provided for the instance of SoftAssert validation.</param>
        /// <param name="condition">Condition to be verified.</param>
        public void IsTrue(string key, bool condition)
        {
            string assertString = this.GetAssertMessage(key, "true", condition.ToString());
            try
            {
                Assert.That(condition, Is.True);
                this.PassedAssertList.Add(assertString);
            }
            catch (Exception exception)
            {
                Log.Error(exception.ToString());
                this.FailedAssertList.Add(assertString);
            }
        }

        /// <summary>
        /// Verify that value of provided condition is false.
        /// </summary>
        /// <param name="key">Name to provided for the instance of SoftAssert validation.</param>
        /// <param name="condition">Condition to be verified.</param>
        public void IsFalse(string key, bool condition)
        {
            string assertString = this.GetAssertMessage(key, "false", condition.ToString());
            try
            {
                Assert.That(condition, Is.False);
                this.PassedAssertList.Add(assertString);
            }
            catch (Exception exception)
            {
                Log.Error(exception.ToString());
                this.FailedAssertList.Add(assertString);
            }
        }

        /// <summary>
        /// Verify that value of expected and actual values match each other in RegEx comparison.
        /// </summary>
        /// <param name="key">Name to provided for the instance of SoftAssert validation.</param>
        /// <param name="expectedValue">Expected value for comparison.</param>
        /// <param name="actualValue">Actual value for comparison.</param>
        public void IsMatch(string key, object expectedValue, object actualValue)
        {
            string assertString = this.GetAssertMessage(key, expectedValue.ToString(), actualValue.ToString());
            try
            {
                if (Regex.IsMatch(actualValue.ToString(), expectedValue.ToString()))
                {
                    this.PassedAssertList.Add(assertString);
                }
                else
                {
                    throw new Exception(assertString);
                }
            }
            catch (Exception exception)
            {
                Log.Error(exception.ToString());
                this.FailedAssertList.Add(assertString);
            }
        }

        /// <summary>
        /// Verify if the provided sting is Null or Empty.
        /// </summary>
        /// <param name="key">Name to provided for the instance of SoftAssert validation.</param>
        /// <param name="expectedValue">Value of the expected value for comarison.</param>
        /// <param name="actualValue">Value of the actual value for comparison.</param>
        public void IsNullOrEmptyOrString(string key, string expectedValue, string actualValue)
        {
            if (string.IsNullOrEmpty(expectedValue.Trim()))
            {
                this.IsEmpty(key, actualValue);
            }
            else
            {
                this.AreEqual(key, expectedValue, actualValue);
            }
        }

        /// <summary>
        /// Add Message for Passed Asserts List.
        /// </summary>
        /// <param name="message">String containing the message to be Added.</param>
        public void AddMessage(string message)
        {
            this.PassedAssertList.Add(message);
        }

        /// <summary>
        /// Verify that provided value of Actual Value is DB Null.
        /// </summary>
        /// <param name="key">Name to provided for the instance of SoftAssert validation.</param>
        /// <param name="actualValue">Value of the actual value for comparison.</param>
        public void IsDBNull(string key, object actualValue)
        {
            string assertString = this.GetAssertMessage(key, string.Empty, actualValue?.ToString());
            try
            {
                Assert.That(actualValue, Is.EqualTo(DBNull.Value));
                this.PassedAssertList.Add(assertString);
            }
            catch (Exception exception)
            {
                Log.Error(exception.ToString());
                this.FailedAssertList.Add(assertString);
            }
        }

        /// <summary>
        /// Assert the list of all asserts in the available list.
        /// </summary>
        public void AssertAll()
        {
            this.LogSuccessMessage(Log, this.GetListString(this.PassedAssertList));
            Assert.That(this.FailedAssertList.Count, Is.EqualTo(0), "<br>\n" + this.GetListString(this.FailedAssertList) + "<br>\n");
        }

        /// <summary>
        /// Get an aggregate from the provided list of strings.
        /// </summary>
        /// <param name="listString">List Of input strings.</param>
        /// <returns>String object containing the output value.</returns>
        private string GetListString(List<string> listString)
        {
            try
            {
                return listString.Aggregate((a, b) => a + "<br>\n" + b);
            }
            catch
            {
                return "List was Empty";
            }
        }

        /// <summary>
        /// Retrieve the value of Assert Message.
        /// </summary>
        /// <param name="key">Assert Message for the instance of SoftAssert being validated.</param>
        /// <param name="expectedValue">Expected value being asserted.</param>
        /// <param name="actualValue">Actual value being asserted.</param>
        /// <returns>Value of the Assert Message Key of the soft assert being validated.</returns>
        private string GetAssertMessage(string key, string expectedValue, string actualValue)
        {
            return this.AssertMessage.Replace("{expectedValue}", expectedValue).Replace("{actualValue}", actualValue).Replace("{key}", key);
        }

        /// <summary>
        /// Verify that the actual value is greater than zero.
        /// </summary>
        /// <param name="key">Assert Message for the instance of SoftAssert being validated.</param>
        /// <param name="actualValue">Actual object for comparison.</param>
        public void IsGreaterThanZero(string key, object actualValue)
        {
            string assertString = this.GetAssertMessage(key, "greater than zero", actualValue?.ToString());
            try
            {
                Assert.That(actualValue, Is.GreaterThan(0));
                this.PassedAssertList.Add(assertString);
            }
            catch (Exception exception)
            {
                Log.Error(exception.ToString());
                this.FailedAssertList.Add(assertString);
            }
        }
    }
}
