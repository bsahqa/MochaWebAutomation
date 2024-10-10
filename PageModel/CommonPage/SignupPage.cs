using Mailosaur.Models;
using MochaHomeAccounting.Utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MochaHomeAccounting.PageModel.CommonPage
{
    class SignupPage : BasePageModel.BasePageModel
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        SoftAssert softAssert;
        EmailHelper emailHelper;
        LoginPage loginPage;
        SettingPage settingPage;
        public SignupPage(BaseTestContext baseTestContext) : base(baseTestContext)
        {
            softAssert = new(baseTestContext);
            loginPage = new(baseTestContext);
            settingPage = new(baseTestContext);
        }
        // Signup page object
        private static readonly By SignupBtn = By.XPath("(//a[@href='https://app.mochaaccounting.com/register']/button[contains(., 'Sign Up for Free')])[1]");
        private static readonly By Fullname = By.Id("first_name");
        private static readonly By EmailAddress = By.Id("email");
        private static readonly By MobileNumber = By.Id("phone_number");
        private static readonly By CompanyName = By.Id("companyName");
        private static readonly By Password = By.Id("password");
        private static readonly By ConfirmPassword = By.Id("confirmPassword");
        private static readonly By TermsAndConditions = By.Id("invalidCheck");
        private static readonly By SignUpBtn = By.XPath("//button[@type='submit']");
        private static readonly By SignUpSuccessMessage = By.XPath("//h5[contains(text(),'Please verify your email')]");

        // Signup page labels object

        private static readonly By SignupPageTitle = By.XPath("//p[contains(text(),'Create new account')]");
        private static readonly By FullnameLbl = By.XPath("//label[contains(text(),'Full Name')]");
        private static readonly By EmailAddressLbl = By.XPath("//label[contains(text(),'Email address')]");
        private static readonly By MobileNumberLbl = By.XPath("//label[contains(text(),'Mobile Number')]");
        private static readonly By CompanyNameLbl = By.XPath("//label[contains(text(),'Company Name')]");
        private static readonly By DomainLbl = By.XPath("//label[contains(text(),'Your Domain')]");
        private static readonly By PasswordLbl = By.XPath("//label[contains(text(),'Password')]");
        private static readonly By ConfirmPasswordLbl = By.XPath("//label[contains(text(),'Confirm Password')]");

        // Singup page Error Messages
        private static readonly By FullnameErrorMsg = By.XPath("//p[contains(text(),'Full name is required.')]");
        private static readonly By EmailAddressErrorMsg = By.XPath("//p[contains(text(),'Email is required.')]");
        private static readonly By CompanyNameErrorMsg = By.XPath("//p[contains(text(),'Company Name is required.')]");
        private static readonly By DomainNameErrorMsg = By.XPath("//p[contains(text(),'Domain prefix is required.')]");
        private static readonly By PasswordErrorMsg = By.XPath("//p[contains(text(),'Password is required')]");
        private static readonly By ConfirmPasswordErrorMsg = By.XPath("//p[contains(text(),'Confirm password is required')]");



        // Email Verification objects
        private static readonly By EmailField = By.Id("login");
        private static readonly By CheckEmailBtn = By.Id("refreshbut");
        private static readonly By EmailSubject = By.XPath("//span[contains(text(),'Mocha Technologies')]");
        private static readonly By EmailVerificationLink = By.XPath("//a[contains(text(),'Verify Email Address')]");
        private static readonly By LoginPage = By.XPath("//p[contains(text(),'Login to your Mocha account')]");

        
        public void ClickOnSignUpBtn()
        {
            ClickElement(SignupBtn);
        }
        public void EnterFullName(string fullname)
        {
            EnterText(Fullname, fullname);
        }

        string emailName;
        string emailAddress;
        string password = "Test@1234";

        public void EnterEmailAddress()
        {
            Random random = new Random();
            int randomNumber = random.Next(100, 1000);
            emailName = $"test{randomNumber}";
            emailAddress = $"{emailName}@yopmail.com";
            EnterText(EmailAddress, emailAddress);
        }

        public void EnterMobileNumber(string mobileNumber)
        {
            EnterText(MobileNumber, mobileNumber);
        }

        public void EnterCompanyName(string companyName)
        {
            EnterText(CompanyName, companyName);
        }

        public void EnterPassword()
        {
            EnterText(Password, password);
        }

        public void EnterConfirmPassword()
        {
            EnterText(ConfirmPassword, password);
        }

        public void ClickOnTermsAndConditions()
        {
            ClickElement(TermsAndConditions);
        }

        public void ClickOnSignUpButton()
        {
            Task.Delay(5000).Wait();
            ClickElement(SignUpBtn);
            ExcelUtility.WriteLoginCredentials(emailAddress, password);
        }

        public void ValidateSignupMessage()
        {
            this.WaitForElementToBeLoaded();
            string actualSignupMessage = GetElementText(SignUpSuccessMessage);
            string expectedSignupMessage = "Please verify your email";
            LogInfoMessage(Log, $"Actual Message {actualSignupMessage}");
            softAssert.IsContains("IsSignupSuccessDisplayed", expectedSignupMessage, actualSignupMessage);
            Task.Delay(5000).Wait();
        }

        public void VerifyEmail()
        {
            BaseTestContext.Driver.Navigate().GoToUrl("https://yopmail.com/en/");
            EnterText(EmailField, emailName);
            ClickElement(CheckEmailBtn);
            IWebElement iFrameElement = BaseTestContext.Driver.FindElement(By.Id("ifmail"));
            BaseTestContext.Driver.SwitchTo().Frame(iFrameElement);
            ClickElement(EmailSubject);
            IWebElement emailVerificationLinkElement = BaseTestContext.Driver.FindElement(EmailVerificationLink);
            string verificationUrl = emailVerificationLinkElement.GetAttribute("href");
            BaseTestContext.Driver.SwitchTo().DefaultContent();
            BaseTestContext.Driver.Navigate().GoToUrl(verificationUrl);
        }

        public void ValidateLoginPageOpened()
        {
            this.WaitForElementToBeLoaded();
            string actualLoginPage = GetElementText(LoginPage);
            string expectedLoginPage = "Login to your Mocha account";
            LogInfoMessage(Log, $"Actual Login Message {actualLoginPage}");
            softAssert.IsContains("IsLoginPageDisplayed", expectedLoginPage, actualLoginPage);
        }

        public void ValidateSignupFunctionality()
        {
            ClickOnSignUpBtn();
            LogInfoMessage(Log, "Clicked on Signup button");
            EnterFullName("Test User");
            LogInfoMessage(Log, "Entered Full Name");
            EnterEmailAddress();
            LogInfoMessage(Log, "Entered Email Address");
            EnterMobileNumber("1234567890");
            LogInfoMessage(Log, "Entered Mobile Number");
            EnterCompanyName("TestCompany01");
            LogInfoMessage(Log, "Entered Company Name");
            EnterPassword();
            LogInfoMessage(Log, "Entered Password");
            EnterConfirmPassword();
            LogInfoMessage(Log, "Entered Confirm Password");
            ClickOnTermsAndConditions();
            LogInfoMessage(Log, "Clicked on Terms and Conditions");
            ClickOnSignUpButton();
            LogInfoMessage(Log, "Clicked on Signup button");
            ValidateSignupMessage();
            LogInfoMessage(Log, "Validated Signup message");
            VerifyEmail();
            LogInfoMessage(Log, "Verified Email");
            ValidateLoginPageOpened();
            LogInfoMessage(Log, "Validated Login Page opened");
        }
    }
}
