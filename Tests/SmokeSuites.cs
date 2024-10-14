namespace MochaHomeAccounting.Tests
{
    using NUnit.Framework;
    using OpenQA.Selenium;
    using MochaHomeAccounting.Utilities.BaseTestLibrary;
    using DescriptionAttribute = DescriptionAttribute;
    using MochaHomeAccounting.PageModel.DashboardPage;
    using MochaHomeAccounting.PageModel.CommonPage;
    using MochaHomeAccounting.PageModel.SalesPage;

    [TestFixture]
    [Parallelizable(ParallelScope.None)]
    [FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
    public class SmokeSuites : BaseUITest
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly string Url = "https://mochaaccounting.com/";
        private IWebDriver? driver;
        private SignupPage? signupPage;
        private LoginPage? loginPage;
        private SettingPage? settingPage;
        private DashboardPage? dashboardPage;
        private CustomersPage? customersPage;
        string loginUsername;
        string loginPassword;

        [SetUp]
        public void TestSetup()
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            this.signupPage = new SignupPage(this.baseTestContext);
            this.loginPage = new LoginPage(this.baseTestContext);
            this.settingPage = new SettingPage(this.baseTestContext);
            this.dashboardPage = new DashboardPage(this.baseTestContext);
            this.customersPage = new CustomersPage(this.baseTestContext);
            this.driver = this.baseTestContext.Driver;
            this.driver.Navigate().GoToUrl(Url);
            this.LogInfoMessage(Log, $"Navigated to URL: {Url}");
            this.driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            var credentials = ExcelUtility.GetLoginCredentials();
            loginUsername = credentials.username;
            loginPassword = credentials.password;
        }

        [Test]
        [Description("Test to validate Signup functionality")]
        [Category("Smoke")]
        [Property("TestCaseNumber", "Test Case 1")]
        [Order(1)]
        [Retry(2)]
        public void VerifySignupFunctionality()
        {
            try
            {
                this.signupPage.ClickOnSignUpBtn();
                this.LogInfoMessage(Log, "Clicked on Signup button");
                this.signupPage.ValidateSignupPageOpened();
                this.LogInfoMessage(Log, "Signup Page Displayed");
                this.signupPage.EnterFullName("Test User");
                this.LogInfoMessage(Log, "Entered Full Name");
                this.signupPage.EnterEmailAddress();
                this.LogInfoMessage(Log, "Entered Email Address");
                this.signupPage.EnterMobileNumber("1234567890");
                this.LogInfoMessage(Log, "Entered Mobile Number");
                this.signupPage.EnterCompanyName("TestCompany01");
                this.LogInfoMessage(Log, "Entered Company Name");
                this.signupPage.EnterPassword();
                this.LogInfoMessage(Log, "Entered Password");
                this.signupPage.EnterConfirmPassword();
                this.LogInfoMessage(Log, "Entered Confirm Password");
                this.signupPage.ClickOnTermsAndConditions();
                this.LogInfoMessage(Log, "Clicked on Terms and Conditions");
                this.signupPage.ClickOnSignUpButton();
                this.LogInfoMessage(Log, "Clicked on Signup button");
                this.signupPage.ValidateUserIsRegisteredSuccessfully();
                this.LogInfoMessage(Log, "Validated Signup message");
                this.signupPage.VerifyEmail();
                this.LogInfoMessage(Log, "Verified Email");
                this.signupPage.ValidateLoginPageOpened();
                this.LogInfoMessage(Log, "Validated Login Page opened");
            }
            catch(Exception ex)
            {
                this.LogFailureMessageWoScreenshot(Log, ex.Message);
            }
        }

        [Test]
        [Description("Test to validate Login functionality")]
        [Category("Smoke")]
        [Property("TestCaseNumber", "Test Case 2")]
        [Order(2)]
        [Retry(2)]
        public void VerifyLoginFunctionality()
        {
            try
            {
                this.loginPage.ClickOnSignBtn();
                this.loginPage.TypeUsername(loginUsername);
                this.LogInfoMessage(Log, $"Entered username: {loginUsername}");
                this.loginPage.TypePassword(loginPassword);
                this.LogInfoMessage(Log, $"Entered password: {loginPassword}");
                this.loginPage.ClickLoginButton();
                this.LogInfoMessage(Log, "Clicked on Login button");
                this.loginPage.ValidateHoldOnMessageDisplayedAfterLogin();
                this.LogInfoMessage(Log, "Hold On Message displayed");
                this.dashboardPage.ValidateDashboardPageOpened();
                this.LogInfoMessage(Log, "Dashboard page opened successfully");
            }
            catch(Exception ex)
            {
                this.LogFailureMessageWoScreenshot(Log, ex.Message);
            }
        }

        [Test]
        [Description("Test to validate Settings functionality")]
        [Category("Smoke")]
        [Property("TestCaseNumber", "Test Case 3")]
        [Order(3)]
        [Retry(2)]
        public void VerifySettingsFunctionality()
        {
            try
            {
                this.loginPage.ValidateLogin(loginUsername, loginPassword);
                this.settingPage.ValidateSettingPageOpened();
                this.LogInfoMessage(Log, "Settings page is opened successfully");
                this.settingPage.EnterCompanyLegalName("TestCompany01");
                this.LogInfoMessage(Log, "Company Legal Name is entered successfully");
                this.settingPage.EnterCompanyAddress("12 New Delhi");
                this.LogInfoMessage(Log, "Company Address is entered successfully");
                this.settingPage.SelectIndustryOption();
                this.LogInfoMessage(Log, "Industry is selected successfully");
                this.settingPage.ClickOnAccountDetails();
                this.LogInfoMessage(Log, "Clicked on Account Details");
                this.settingPage.ClickOnSaveButton();
                this.LogInfoMessage(Log, "Clicked on Save button successfully");
                this.settingPage.ValidateSettingsIsSaved();
                this.LogInfoMessage(Log, "Settings are saved successfully");
            }
            catch(Exception ex)
            {
                this.LogFailureMessageWoScreenshot(Log, ex.Message);
            }
        }

        [Test]
        [Description("Test to validate Create Customer Type functionality")]
        [Category("Smoke")]
        [Property("TestCaseNumber", "Test Case 4")]
        [Order(4)]
        [Retry(2)]
        public void VerifyCreateCustomerTypeFunctionality()
        {
            try
            {
                this.loginPage.ValidateLogin(loginUsername, loginPassword);
                this.customersPage.OpenCustomersPage();
                this.customersPage.ClickOnCustomerType();
                this.LogInfoMessage(Log, "Clicked on Customer Type");
                this.customersPage.ValidateCustomerTypePageOpened();
                this.LogInfoMessage(Log, "Customer Type page opened successfully");
                this.customersPage.ClickOnNewCustomerType();
                this.LogInfoMessage(Log, "Clicked on New Customer Type");
                this.customersPage.EnterCustomerTypeName();
                this.LogInfoMessage(Log, "Entered Customer Type Name");
                this.customersPage.ClickOnAddCustomerType();
                this.LogInfoMessage(Log, "Clicked on Add Customer Type");
                this.customersPage.ValidateCustomerTypeIsCreated();
                this.LogInfoMessage(Log, "Customer Type is created successfully");
                this.customersPage.ValidateCustomerTypeIsDisplayed();
                this.LogInfoMessage(Log, "Customer Type is displayed successfully");
            }
            catch(Exception ex)
            {
                this.LogFailureMessageWoScreenshot(Log, ex.Message);
            }
        }

        [Test]
        [Description("Test to validate Edit Customer Type functionality")]
        [Category("Smoke")]
        [Property("TestCaseNumber", "Test Case 5")]
        [Order(5)]
        [Retry(2)]
        public void VerifyEditCustomerTypeFunctionality()
        {
            try
            {
                this.loginPage.ValidateLogin(loginUsername, loginPassword);
                this.customersPage.OpenCustomersPage();
                this.customersPage.ClickOnCustomerType();
                this.LogInfoMessage(Log, "Clicked on Customer Type");
                this.customersPage.ClickOnEditCustomerType();
                this.LogInfoMessage(Log, "Clicked on Edit Customer Type");
                this.customersPage.EnterCustomerTypeName();
                this.LogInfoMessage(Log, "Entered Customer Type Name");
                this.customersPage.ClickOnUpdateCustomerType();
                this.LogInfoMessage(Log, "Clicked on Update Customer Type");
                this.customersPage.ValidateCustomerTypeIsUpdated();
                this.LogInfoMessage(Log, "Customer Type is updated successfully");
                this.customersPage.ValidateCustomerTypeIsDisplayed();
                this.LogInfoMessage(Log, "Customer Type is displayed successfully");
            }
            catch(Exception ex)
            {
                this.LogFailureMessageWoScreenshot(Log, ex.Message);
            }
        }

        [Test]
        [Description("Test to validate all fields and error message are correctly displaying on Signup page")]
        [Category("Smoke")]
        [Property("TestCaseNumber", "Test Case 5")]
        [Order(6)]
        [Retry(2)]
        public void VerifyAllFieldsAndValidationErrorMessageDisplayed()
        {
            this.signupPage.ClickOnSignUpBtn();
            this.LogInfoMessage(Log, "Clicked on Signup button");
            this.signupPage.ValidateSignupPageTitleIsVisible();
            this.LogInfoMessage(Log, "Singup page is visible");
            this.signupPage.ValidateFullnameLabelIsVisible();
            this.LogInfoMessage(Log, "Fullname field is visible");
            this.signupPage.ValidateEmailAddressLabelIsVisible();
            this.LogInfoMessage(Log, "Email Address field is visible");
            this.signupPage.ValidateMobileNumberLabelIsVisible();
            this.LogInfoMessage(Log, "Mobile number field is visible");
            this.signupPage.ValidateCompanyNameLabelIsVisible();
            this.LogInfoMessage(Log, "Comapny field is visible");
            this.signupPage.ValidateDomainLabelIsVisible();
            this.LogInfoMessage(Log, "Domain field is visible");
            this.signupPage.ValidatePasswordLabelIsVisible();
            this.LogInfoMessage(Log, "Password field is visible");
            this.signupPage.ValidateConfirmPasswordLabelIsVisible();
            this.LogInfoMessage(Log, "Confirm password field is visible");
            this.signupPage.ClickOnSignUpButton();
            this.LogInfoMessage(Log, "Clicked on Signup button");
            this.signupPage.ValidateFullnameErrorMsg();
            this.LogInfoMessage(Log, "Fullname error message is visible");
            this.signupPage.ValidateEmailAddressErrorMsg();
            this.LogInfoMessage(Log, "Email address error message is visible");
            this.signupPage.ValidateCompanyNameErrorMsg();
            this.LogInfoMessage(Log, "Company Name error message is visible");
            this.signupPage.ValidateDomainNameErrorMsg();
            this.LogInfoMessage(Log, "Domain Name error message is visible");
            this.signupPage.ValidatePasswordErrorMsg();
            this.LogInfoMessage(Log, "Password error message is visible");
            this.signupPage.ValidateConfirmPasswordErrorMsg();
            this.LogInfoMessage(Log, "Confirm Password error message is visible");
        }

        [Test]
        [Description("Test to validate all fields and error message are correctly displaying on Signup page")]
        [Category("Smoke")]
        [Property("TestCaseNumber", "Test Case 5")]
        [Order(7)]
        [Retry(2)]
        public void VerifyCreateCustomerFunctionality()
        {
            try
            {
                this.loginPage.ValidateLogin(loginUsername, loginPassword);
                this.customersPage.OpenCustomersPage();
                this.customersPage.ClickOnCreateCustomer();
                this.LogInfoMessage(Log, "Clicked Create Customer Button");
                this.customersPage.EnterDisplayedName();
                this.LogInfoMessage(Log, "Entered display name");
                this.customersPage.EnterEmail();
                this.LogInfoMessage(Log, "Entered email");
                this.customersPage.EnterMobileNumber();
                this.customersPage.EnterBillingAddress();
                this.LogInfoMessage(Log, "Billing Address selected");
                this.customersPage.EnterShippingAddress();
                this.LogInfoMessage(Log, "Shipping address selected");
                this.customersPage.SelectCustomerTypeValue();
                this.LogInfoMessage(Log, "Customer type selected");
                this.customersPage.SelectSelectDeliveryMethodValue();
                this.LogInfoMessage(Log, "Delivery method selected");
                this.customersPage.SelectSelectPaymentMethodOptionValue();
                this.LogInfoMessage(Log, "Payment method selected");
                this.customersPage.ClickOnSaveAndCloseBtn();
                this.LogInfoMessage(Log, "Clicked Save and Close");
                this.customersPage.ValidateNewCustomerIsCreatedSuccessfylly();
                this.LogInfoMessage(Log, "New Customer is created successfull");
            }
            catch (Exception ex)
            {
                this.LogFailureMessageWoScreenshot(Log,ex.Message);
            }
        }

        [TearDown]
        public void Cleanup()
        {
            this.DisposeDriver();
        }
    }
}
