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

        public void ValidateSignupPageOpened()
        {
            string actualSignupTitle = GetElementText(SignupPageTitle);
            string expectedSignupTitle = "Create new account";
            this.softAssert.IsContains("IsSignupPageDisplayed", expectedSignupTitle, actualSignupTitle);
        }

        public void ValidateSignupPageTitleIsVisible()
        {
            string actualSignupTitle = GetElementText(SignupPageTitle);
            string expectedSignupTitle = "Create new account";
            this.softAssert.IsContains("IsSignupPageDisplayed", expectedSignupTitle, actualSignupTitle);
        }

        public void ValidateFullnameLabelIsVisible()
        {
            string actualFullnameLabel = GetElementText(FullnameLbl);
            string expectedFullnameLabel = "Full Name";
            this.softAssert.IsContains("IsFullnameLabelDisplayed", expectedFullnameLabel, actualFullnameLabel);
        }

        public void ValidateEmailAddressLabelIsVisible()
        {
            string actualEmailAddressLabel = GetElementText(EmailAddressLbl);
            string expectedEmailAddressLabel = "Email address";
            this.softAssert.IsContains("IsEmailAddressLabelDisplayed", expectedEmailAddressLabel, actualEmailAddressLabel);
        }

        public void ValidateMobileNumberLabelIsVisible()
        {
            string actualMobileNumberLabel = GetElementText(MobileNumberLbl);
            string expectedMobileNumberLabel = "Mobile Number";
            this.softAssert.IsContains("IsMobileNumberLabelDisplayed", expectedMobileNumberLabel, actualMobileNumberLabel);
        }

        public void ValidateCompanyNameLabelIsVisible()
        {
            string actualCompanyNameLabel = GetElementText(CompanyNameLbl);
            string expectedCompanyNameLabel = "Company Name";
            this.softAssert.IsContains("IsCompanyNameLabelDisplayed", expectedCompanyNameLabel, actualCompanyNameLabel);
        }

        public void ValidateDomainLabelIsVisible()
        {
            string actualDomainLabel = GetElementText(DomainLbl);
            string expectedDomainLabel = "Your Domain";
            this.softAssert.IsContains("IsDomainLabelDisplayed", expectedDomainLabel, actualDomainLabel);
        }

        public void ValidatePasswordLabelIsVisible()
        {
            string actualPasswordLabel = GetElementText(PasswordLbl);
            string expectedPasswordLabel = "Password";
            this.softAssert.IsContains("IsPasswordLabelDisplayed", expectedPasswordLabel, actualPasswordLabel);
        }

        public void ValidateConfirmPasswordLabelIsVisible()
        {
            string actualConfirmPasswordLabel = GetElementText(ConfirmPasswordLbl);
            string expectedConfirmPasswordLabel = "Confirm Password";
            this.softAssert.IsContains("IsConfirmPasswordLabelDisplayed", expectedConfirmPasswordLabel, actualConfirmPasswordLabel);
        }


        public void ValidateFullnameErrorMsg()
        {
            string actualFullnameErrorMsg = GetElementText(FullnameErrorMsg);
            string expectedFullnameErrorMsg = "Full name is required.";
            this.softAssert.IsContains("IsFullnameErrorDisplayed", expectedFullnameErrorMsg, actualFullnameErrorMsg);
        }

        public void ValidateEmailAddressErrorMsg()
        {
            string actualEmailAddressErrorMsg = GetElementText(EmailAddressErrorMsg);
            string expectedEmailAddressErrorMsg = "Email is required.";
            this.softAssert.IsContains("IsEmailAddressErrorDisplayed", expectedEmailAddressErrorMsg, actualEmailAddressErrorMsg);
        }

        public void ValidateCompanyNameErrorMsg()
        {
            string actualCompanyNameErrorMsg = GetElementText(CompanyNameErrorMsg);
            string expectedCompanyNameErrorMsg = "Company Name is required.";
            this.softAssert.IsContains("IsCompanyNameErrorDisplayed", expectedCompanyNameErrorMsg, actualCompanyNameErrorMsg);
        }

        public void ValidateDomainNameErrorMsg()
        {
            string actualDomainNameErrorMsg = GetElementText(DomainNameErrorMsg);
            string expectedDomainNameErrorMsg = "Domain prefix is required.";
            this.softAssert.IsContains("IsDomainNameErrorDisplayed", expectedDomainNameErrorMsg, actualDomainNameErrorMsg);
        }

        public void ValidatePasswordErrorMsg()
        {
            string actualPasswordErrorMsg = GetElementText(PasswordErrorMsg);
            string expectedPasswordErrorMsg = "Password is required";
            this.softAssert.IsContains("IsPasswordErrorDisplayed", expectedPasswordErrorMsg, actualPasswordErrorMsg);
        }

        public void ValidateConfirmPasswordErrorMsg()
        {
            string actualConfirmPasswordErrorMsg = GetElementText(ConfirmPasswordErrorMsg);
            string expectedConfirmPasswordErrorMsg = "Confirm password is required";
            this.softAssert.IsContains("IsConfirmPasswordErrorDisplayed", expectedConfirmPasswordErrorMsg, actualConfirmPasswordErrorMsg);
        }



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

        public void ClickOnSubmitButton()
        {
            Task.Delay(5000).Wait();
            ClickElement(SignUpBtn);
        }

        public void ValidateUserIsRegisteredSuccessfully()
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
            ValidateUserIsRegisteredSuccessfully();
            LogInfoMessage(Log, "Validated Signup message");
            VerifyEmail();
            LogInfoMessage(Log, "Verified Email");
            ValidateLoginPageOpened();
            LogInfoMessage(Log, "Validated Login Page opened");
        }
    }
}
