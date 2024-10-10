using MochaHomeAccounting.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using MochaHomeAccounting.Utilities;

namespace MochaHomeAccounting.PageModel.CommonPage
{
    class SettingPage : BasePageModel.BasePageModel
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        SoftAssert softAssert;
        public SettingPage(BaseTestContext baseTestContext) : base(baseTestContext) { softAssert = new(baseTestContext); }

        private static readonly By SettingTitle = By.XPath("//h4[contains(text(),'Settings')]");
        private static readonly By CompanyLegalName = By.XPath("//input[@name='legalName']");
        private static readonly By CompanyAddress = By.XPath("//input[@placeholder='Enter a location']");
        private static readonly By SelectAddress = By.CssSelector("div.pac-item:nth-of-type(1) > .pac-item-query");
        private static readonly By IndustryField = By.Id("react-select-4-input");
        private static readonly By SelectIndustry = By.XPath("//div[@id='react-select-4-option-0']");
        private static readonly By SaveBtn = By.XPath("//button[@type='submit']");
        private static readonly By DashboardBtn = By.XPath("//a[@href='/dashboard']");
        private static readonly By AccountDetails = By.XPath("//p[contains(text(),'Accounting Details')]");



        public void ValidateSettingPageOpened()
        {
            WaitForElementToBeVisible(SettingTitle);
            string actualSettingTitle = GetElementText(SettingTitle);
            softAssert.IsContains("IsSettingPageOpened", "Settings", actualSettingTitle);
        }

        public void EnterCompanyLegalName(string legalName)
        {
            EnterText(CompanyLegalName, legalName);
        }

        public void EnterCompanyAddress(string address)
        {
            EnterText(CompanyAddress, address);
            ClickElement(SelectAddress);
        }

        public void SelectIndustryOption()
        {
            Task.Delay(5000).Wait();
            ClickElement(IndustryField);
            ClickElement(SelectIndustry);
        }

        public void ClickOnSaveButton()
        {
            Task.Delay(3000).Wait();
            IWebElement SubmitBtn = BaseTestContext.Driver.FindElement(SaveBtn);
            ClickOnElementWithJS(SubmitBtn);
            Task.Delay(5000).Wait();
        }
        public void ClickOnAccountDetails()
        {
            ClickElement(AccountDetails);
        }

        public void ValidateSettingsIsSaved()
        {
            WaitForElementToBeVisible(DashboardBtn);
            string IsDashboardEnabled = GetElementText(DashboardBtn);
            softAssert.IsContains("IsSettingSaved", "Dashboard", IsDashboardEnabled);
        }
    }
}
