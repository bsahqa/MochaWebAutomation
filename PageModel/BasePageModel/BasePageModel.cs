
namespace MochaHomeAccounting.PageModel.BasePageModel
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Interactions;
    using OpenQA.Selenium.Support.UI;
    using MochaHomeAccounting.PageModel;
    using MochaHomeAccounting.Utilities;
    using SeleniumExtras.WaitHelpers;

    /// <summary>
    /// Base implementation for the Page Models across the project.
    /// </summary>
    public abstract class BasePageModel : BaseModel
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly WebDriverWait wait;
        private Actions actions;
       
        /// <summary>
        /// Identifier for the IFrames Element containing the Ads Modal.
        /// </summary>
        private static readonly By IFrameIdentifier = By.XPath("//iframe[contains(@id,'aswift')]");

        /// <summary>
        /// Initializes a new instance of the <see cref="BasePageModel"/> class.
        /// </summary>
        /// <param name="baseTextContext">Instance of the BaseTestContext object.</param>
        protected BasePageModel(BaseTestContext baseTextContext)
            : base(baseTextContext)
        {
            this.wait = new WebDriverWait(this.BaseTestContext.Driver, TimeSpan.FromSeconds(30));
        }

        /// <summary>
        /// Verify if the stated element is present on the web page and is visible.
        /// </summary>
        /// <param name="objectIdentifier">Identifier of the object to be searched.</param>
        /// <param name="log">Instance of the Logger object.</param>
        /// <returns>Boolean value indicating if the element was present and visible.</returns>
        public bool IsElementPresentAndVisible(By objectIdentifier, log4net.ILog log)
        {
            try
            {
                IWebElement webElement = this.BaseTestContext.Driver.FindElement(objectIdentifier);
                if (webElement != null && webElement.Displayed)
                {
                    this.LogInfo(log, " Element found using the identifier :" + objectIdentifier);
                    return true;
                }
            }
            catch (Exception exception)
            {
                this.LogError(log, " Element found using the identifier :" + objectIdentifier + exception.ToString());
            }

            return false;
        }

        /// <summary>
        /// Click on the provided WebElement.
        /// </summary>
        /// <param name="element">IWebElement to be interacted with.</param>
        public void ClickOnElement(IWebElement element)
        {
            this.wait.Until(ExpectedConditions.ElementToBeClickable(element));
            element.Click();
        }

        /// <summary>
        /// Click on the element for which the Element Identifier is provided.
        /// </summary>
        /// <param name="elementIdentifier">Identifier of the element to be interacted with.</param>
        public void ClickOnElement(By elementIdentifier)
        {
            var element = this.GetWebElement(elementIdentifier);
            this.wait.Until(ExpectedConditions.ElementToBeClickable(element));
            this.ScrollIntoView(element);
            element.Click();
        }

        /// <summary>
        /// Click on the element for which the Element Identifier is provided.
        /// </summary>
        /// <param name="elementIdentifier">Identifier of the element to be interacted with.</param>
        public void ClickElement(By elementIdentifier)
        {
            var element = this.GetWebElement(elementIdentifier);
            this.wait.Until(ExpectedConditions.ElementToBeClickable(element));
            element.Click();
        }

        /// <summary>
        /// Click on the webelement using JavaScrpipt.
        /// </summary>
        /// <param name="element">IWebElement to be interacted with.</param>
        public void ClickOnElementWithJS(IWebElement element)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)this.BaseTestContext.Driver;
            js.ExecuteScript("arguments[0].click();", element);
        }

        /// <summary>
        /// Scroll the webpage to the specified web-element.
        /// </summary>
        /// <param name="element">IWebElement to be interacted with.</param>
        public void ScrollIntoView(IWebElement element)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)this.BaseTestContext.Driver;
            js.ExecuteScript("arguments[0].scrollIntoView(true)", element);
        }

        public void ScrollDownToTheElement(By element)
        {
            // Find the scrollable element (adjust the locator as necessary)
            IWebElement scrollableElement = this.BaseTestContext.Driver.FindElement(element);

            // Scroll the specific element
            IJavaScriptExecutor js = (IJavaScriptExecutor)this.BaseTestContext.Driver;
            js.ExecuteScript("arguments[0].scrollTop = arguments[0].scrollHeight;", scrollableElement);
        }

        /// <summary>
        /// Uncheck the provided checkbox element.
        /// </summary>
        /// <param name="element">IWebElement to be interacted with.</param>
        public void UnCheckElement(By element)
        {
            this.wait.Until(ExpectedConditions.ElementToBeClickable(element));
            IWebElement checkBox = this.BaseTestContext.Driver.FindElement(element);

            if (checkBox.Selected)
            {
                checkBox.Click();
            }
        }

        /// <summary>
        /// Check the provided checkbox element.
        /// </summary>
        /// <param name="element">IWebElement to be interacted with.</param>
        public void CheckElement(By element)
        {
            this.wait.Until(ExpectedConditions.ElementToBeClickable(element));
            IWebElement checkBox = this.BaseTestContext.Driver.FindElement(element);

            if (!checkBox.Selected)
            {
                checkBox.Click();
            }
        }

        /// <summary>
        /// Click on the provided web element using Actions class.
        /// </summary>
        /// <param name="element">Identifier of the IWebElement to be interacted with.</param>
        public void ClickElementByAction(By element)
        {
            this.wait.Until(ExpectedConditions.ElementToBeClickable(element));
            IWebElement iElement = this.BaseTestContext.Driver.FindElement(element);
            Actions actions = new Actions(this.BaseTestContext.Driver);
            actions.MoveToElement(iElement).Click().Build().Perform();
        }

        /// <summary>
        /// Enter specified text in the provided element.
        /// </summary>
        /// <param name="element">Identifier of the IWebElement to be interacted with.</param>
        /// <param name="text">Text value to be entered.</param>
        public void EnterText(By element, string text)
        {
            this.wait.Until(ExpectedConditions.ElementToBeClickable(element));
            IWebElement webElement = this.BaseTestContext.Driver.FindElement(element);
            webElement.Click();
            webElement.Clear();
            webElement.SendKeys(text);
        }

        /// <summary>
        /// Enter specified text in the provided element.
        /// </summary>
        /// <param name="element">Identifier of the IWebElement to be interacted with.</param>
        /// <param name="text">Text value to be entered.</param>
        public void UploadFile(By element, string text)
        {
            this.wait.Until(ExpectedConditions.ElementToBeClickable(element));
            IWebElement webElement = this.BaseTestContext.Driver.FindElement(element);
            webElement.SendKeys(text);
        }

        /// <summary>
        /// Select all current text in the provided element and enter the specified test into it.
        /// </summary>
        /// <param name="element">Identifier of the IWebElement to be interacted with.</param>
        /// <param name="text">Text value to be entered.</param>
        public void EnterTextBySelection(By element, string text)
        {
            this.wait.Until(ExpectedConditions.ElementToBeClickable(element));
            IWebElement webElement = this.BaseTestContext.Driver.FindElement(element);
            webElement.SendKeys(Keys.Control + "a");
            webElement.SendKeys(text);
        }

        /// <summary>
        /// Drag and drop a source element to a target element identified by their element identifiers.
        /// </summary>
        /// <param name="sourceElement">Identifier of the source IWebElement to be dragged.</param>
        /// <param name="targetElement">Identifier of the target IWebElement where the source should be dropped.</param>
        public void DragAndDrop(By sourceElement, By targetElement)
        {
            this.wait.Until(ExpectedConditions.ElementToBeClickable(sourceElement));
            this.wait.Until(ExpectedConditions.ElementToBeClickable(targetElement));

            IWebElement sourceWebElement = this.BaseTestContext.Driver.FindElement(sourceElement);
            IWebElement targetWebElement = this.BaseTestContext.Driver.FindElement(targetElement);

            Actions actions = new Actions(this.BaseTestContext.Driver);
            actions.DragAndDrop(sourceWebElement, targetWebElement).Perform();
        }
        /// Select value in the specified dropdown by using value attribute.
        /// </summary>
        /// <param name="element">Identifier of the IWebElement to be interacted with.</param>
        /// <param name="value">Value to be selected.</param>
        public void SelectByValueFromDropDown(By element, string value)
        {
            this.wait.Until(ExpectedConditions.ElementToBeClickable(element));
            SelectElement selectElement = new SelectElement(this.BaseTestContext.Driver.FindElement(element));
            this.ScrollIntoView(this.BaseTestContext.Driver.FindElement(element));
            selectElement.SelectByValue(value);
        }

        /// <summary>
        /// Enter specified text in the provided element.
        /// </summary>
        /// <param name="element">Identifier of the IWebElement to be interacted with.</param>
        /// <param name="text">Text value to be entered.</param>
        public void TypeText(By element, string text)
        {
            this.WaitForElementToBeVisible(element);
            IWebElement webElement = this.BaseTestContext.Driver.FindElement(element);
            webElement.Click();
            webElement.SendKeys(text);
        }

        /// <summary>
        /// Retrieve the text of the provided web element.
        /// </summary>
        /// <param name="element">Identifier of the IWebElement to be interacted with.</param>
        /// <returns>Text retrieved from the specified webelement.</returns>
        public string GetElementText(By element)
        {
            this.wait.Until(ExpectedConditions.ElementIsVisible(element));
            return this.BaseTestContext.Driver.FindElement(element).Text.Trim();
        }

        /// <summary>
        /// Retrieve the value of the specified attribute of the provided web element.
        /// </summary>
        /// <param name="element">Identifier of the IWebElement to be interacted with.</param>
        /// <param name="attributeName">Name of the attribute for which value needs to be retrieved.</param>
        /// <returns>Returned value of the attribute.</returns>
        public string GetElementValueAttribute(By element, string attributeName)
        {
            return this.BaseTestContext.Driver.FindElement(element).GetAttribute(attributeName);
        }

        /// <summary>
        /// Retrieve the value of the specified CSS attribute of the provided web element.
        /// </summary>
        /// <param name="element">Identifier of the IWebElement to be interacted with.</param>
        /// <param name="attributeName">Name of the CSS attribute for which value needs to be retrieved.</param>
        /// <returns>Returned value of the CSS attribute.</returns>
        public string GetValueForCSSAttributeOfElement(By element, string attributeName)
        {
            return this.BaseTestContext.Driver.FindElement(element).GetCssValue(attributeName);
        }

        /// <summary>
        /// Retrieve list of elements identified by the provided identifier.
        /// </summary>
        /// <param name="element">Identifier of the IWebElement to be interacted with.</param>
        /// <returns>List of identified webelements.</returns>
        public IReadOnlyCollection<IWebElement> GetWebElements(By element)
        {
            return this.BaseTestContext.Driver.FindElements(element);
        }

        /// <summary>
        /// Retrieve the web element identified by the provided identifier.
        /// </summary>
        /// <param name="element">Identifier of the IWebElement to be interacted with.</param>
        /// <returns></returns>
        public IWebElement GetWebElement(By element)
        {
            return this.BaseTestContext.Driver.FindElement(element);
        }

        /// <summary>
        /// Wait for the provided element to be visible.
        /// </summary>
        /// <param name="elementIdentifer">Identifier of the IWebElement to be interacted with.</param>
        public void WaitForElementToBeVisible(By elementIdentifer)
        {
            this.wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(elementIdentifer));
        }

        /// <summary>
        /// Wait for the full page load.
        /// </summary>
        public void WaitForElementToBeLoaded()
        {
            this.BaseTestContext.Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);
        }

        /// <summary>
        /// Wait for the specified time until the element is visible.
        /// </summary>
        /// <param name="by">The locator to find the element.</param>
        /// <param name="timeInSeconds">The maximum time to wait in seconds.</param>
        public void WaitUntilElementToBeVisible(By element)
        {
            WebDriverWait wait = new WebDriverWait(BaseTestContext.Driver, TimeSpan.FromSeconds(30));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(element));
        }

        /// <summary>
        /// Wait for the provided web element to be invisible.
        /// </summary>
        /// <param name="elementIdentifer">Identifier of the IWebElement to be interacted with.</param>
        public void WaitForElementToBeInvisible(By elementIdentifer)
        {
            this.wait.Until(ExpectedConditions.InvisibilityOfElementLocated(elementIdentifer));
        }

        /// <summary>
        /// Verify that the URL contains the specified text.
        /// </summary>
        /// <param name="expectedText">Expected text to be verified.</param>
        /// <returns>Boolean flag indicating if the value was found in the URL.</returns>
        public bool VerifyURLContainsExpectedText(string expectedText)
        {
            return this.BaseTestContext.Driver.Url.Contains(expectedText);
        }

        /// <summary>
        /// Get element locators.
        /// </summary>
        public static By GetElement(string elementName, string placeholder)
        {
            string xpath = elementName.Replace("{{placeholder}}", placeholder);
            return By.XPath(xpath);
        }
    }
}
