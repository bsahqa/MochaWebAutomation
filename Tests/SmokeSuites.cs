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
                signupPage.ClickOnSignUpBtn();
                LogInfoMessage(Log, "Clicked on Signup button");
                signupPage.EnterFullName("Test User");
                LogInfoMessage(Log, "Entered Full Name");
                signupPage.EnterEmailAddress();
                LogInfoMessage(Log, "Entered Email Address");
                signupPage.EnterMobileNumber("1234567890");
                LogInfoMessage(Log, "Entered Mobile Number");
                signupPage.EnterCompanyName("TestCompany01");
                LogInfoMessage(Log, "Entered Company Name");
                signupPage.EnterPassword();
                LogInfoMessage(Log, "Entered Password");
                signupPage.EnterConfirmPassword();
                LogInfoMessage(Log, "Entered Confirm Password");
                signupPage.ClickOnTermsAndConditions();
                LogInfoMessage(Log, "Clicked on Terms and Conditions");
                signupPage.ClickOnSignUpButton();
                LogInfoMessage(Log, "Clicked on Signup button");
                signupPage.ValidateSignupMessage();
                LogInfoMessage(Log, "Validated Signup message");
                signupPage.VerifyEmail();
                LogInfoMessage(Log, "Verified Email");
                signupPage.ValidateLoginPageOpened();
                LogInfoMessage(Log, "Validated Login Page opened");
            }
            catch(Exception ex)
            {
                LogFailureMessageWoScreenshot(Log, ex.Message);
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
                loginPage.ValidateLogin(loginUsername, loginPassword);
                settingPage.ValidateSettingPageOpened();
                LogInfoMessage(Log, "Settings page is opened successfully");
                settingPage.EnterCompanyLegalName("TestCompany01");
                LogInfoMessage(Log, "Company Legal Name is entered successfully");
                settingPage.EnterCompanyAddress("12 New Delhi");
                LogInfoMessage(Log, "Company Address is entered successfully");
                settingPage.SelectIndustryOption();
                LogInfoMessage(Log, "Industry is selected successfully");
                settingPage.ClickOnAccountDetails();
                LogInfoMessage(Log, "Clicked on Account Details");
                settingPage.ClickOnSaveButton();
                LogInfoMessage(Log, "Clicked on Save button successfully");
                settingPage.ValidateSettingsIsSaved();
                LogInfoMessage(Log, "Settings are saved successfully");
            }
            catch(Exception ex)
            {
                LogFailureMessageWoScreenshot(Log, ex.Message);
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
                loginPage.ValidateLogin(loginUsername, loginPassword);
                customersPage.OpenCustomersPage();
                customersPage.ClickOnCustomerType();
                this.LogInfoMessage(Log, "Clicked on Customer Type");
                customersPage.ValidateCustomerTypePageOpened();
                this.LogInfoMessage(Log, "Customer Type page opened successfully");
                customersPage.ClickOnNewCustomerType();
                this.LogInfoMessage(Log, "Clicked on New Customer Type");
                customersPage.EnterCustomerTypeName();
                this.LogInfoMessage(Log, "Entered Customer Type Name");
                customersPage.ClickOnAddCustomerType();
                this.LogInfoMessage(Log, "Clicked on Add Customer Type");
                customersPage.ValidateCustomerTypeIsCreated();
                this.LogInfoMessage(Log, "Customer Type is created successfully");
                customersPage.ValidateCustomerTypeIsDisplayed();
                this.LogInfoMessage(Log, "Customer Type is displayed successfully");
            }
            catch(Exception ex)
            {
                LogFailureMessageWoScreenshot(Log, ex.Message);
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
                loginPage.ValidateLogin(loginUsername, loginPassword);
                customersPage.OpenCustomersPage();
                customersPage.ClickOnCustomerType();
                this.LogInfoMessage(Log, "Clicked on Customer Type");
                customersPage.ClickOnEditCustomerType();
                this.LogInfoMessage(Log, "Clicked on Edit Customer Type");
                customersPage.EnterCustomerTypeName();
                this.LogInfoMessage(Log, "Entered Customer Type Name");
                customersPage.ClickOnUpdateCustomerType();
                this.LogInfoMessage(Log, "Clicked on Update Customer Type");
                customersPage.ValidateCustomerTypeIsUpdated();
                this.LogInfoMessage(Log, "Customer Type is updated successfully");
                customersPage.ValidateCustomerTypeIsDisplayed();
                this.LogInfoMessage(Log, "Customer Type is displayed successfully");
            }
            catch(Exception ex)
            {
                LogFailureMessageWoScreenshot(Log, ex.Message);
            }
        }

        [TearDown]
        public void Cleanup()
        {
            this.DisposeDriver();
        }
    }
}
