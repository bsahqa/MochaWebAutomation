using MochaHomeAccounting.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using MochaHomeAccounting.Utilities;
using Bogus;

namespace MochaHomeAccounting.PageModel.SalesPage
{
    class CustomersPage : BasePageModel.BasePageModel
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        SoftAssert softAssert;
        Faker data;
        public CustomersPage(BaseTestContext baseTestContext) : base(baseTestContext) 
        { 
            softAssert = new(baseTestContext); 
            data = new Faker();
        }

        string CustomerName;
        private static readonly By SalesBtn = By.XPath("//li[@class='nav-group']//a[text()='Sales']");
        private static readonly By CustomersBtn = By.XPath("//a[contains(text(),'Customers')]");
        private static readonly By CustomerPageTitle = By.XPath("//h4[contains(text(),'Customers')]");
        private static readonly By CustomerTypeBtn = By.XPath("//a[contains(text(),'Customer Type')]");
        private static readonly By CreateCustomerBtn = By.XPath("//a[contains(text(),'Create Customer')]");
        private static readonly By CustomerTypePageTitle = By.XPath("//h4[contains(text(),'Customer Type')]");
        private static readonly By NewCustomerTypeBtn = By.XPath("//button[contains(text(),'New Customer Type')]");
        private static readonly By CustomerTypeName = By.XPath("//input[@name='name']");
        private static readonly By CustomerTypeAddBtn = By.XPath("//button[contains(text(),'Add')]");
        private static readonly By CustomerTypeSuccessMsg = By.XPath("//p[contains(text(),'Successfully created.')]");
        private static readonly string CustomerTypeItem = "//td[contains(text(), '{{placeholder}}')]";
        private static readonly By CustomerTypeEditBtn = By.XPath("(//div[@class='btn-group']//button)[1]");
        private static readonly By CustomerTypeUpdateBtn = By.XPath("//button[contains(text(),'Update')]");
        private static readonly By CustomerTypeUpdateSuccessMsg = By.XPath("//p[contains(text(),'Successfully updated.')]");
        private static readonly By DisplayedName = By.XPath("//input[@label='Display Name*']");
        private static readonly By Email = By.XPath("//input[@label='Email*']");
        private static readonly By MobileNumber = By.XPath("//input[@label='Mobile Number']");
        private static readonly By BillingAddress = By.XPath("(//input[@placeholder='Enter a location'])[1]");
        private static readonly By SelectAddress = By.CssSelector("div.pac-item:nth-of-type(1) > span:nth-of-type(3)");
        private static readonly By ShippingAddress = By.XPath("(//input[@placeholder='Enter a location'])[2]");
        private static readonly By SelectCustomerType = By.XPath("//body[1]/div[1]/div[1]/div[2]/div[2]/div[1]/form[1]/div[1]/div[2]/div[15]/div[3]/div[2]/div[1]/div[1]/div[1]/div[1]/div[1]/div[1]/div[1]/div[2]");
        private static readonly By SelectCutomerTypeOption = By.Id("react-select-2-option-0");
        private static readonly By SelectDeliveryMethod = By.XPath("//body[1]/div[1]/div[1]/div[2]/div[2]/div[1]/form[1]/div[1]/div[2]/div[15]/div[3]/div[2]/div[1]/div[2]/div[1]/div[1]/div[1]/div[1]/div[1]/div[2]");
        private static readonly By SelectDeliveryMethodOption = By.Id("react-select-3-option-0");
        private static readonly By SelectPaymentMethod = By.XPath("//body[1]/div[1]/div[1]/div[2]/div[2]/div[1]/form[1]/div[1]/div[2]/div[15]/div[3]/div[2]/div[1]/div[3]/div[1]/div[1]/div[1]/div[1]/div[1]/div[2]");
        private static readonly By SelectPaymentMethodOption = By.Id("react-select-4-option-0");
        private static readonly By SaveAndCloseBtn = By.Id("zoom-primary-cancel-btn");
        private static readonly By SuccessMsg = By.CssSelector(".toast-body > .text-white");


        public void EnterDisplayedName()
        {
            Thread.Sleep(5000);
            this.EnterTextBySelection(DisplayedName, data.Name.FullName());
            Thread.Sleep(2000);
        }

        public void EnterEmail()
        {
            this.WaitForElementToBeLoaded();
            this.EnterTextBySelection(Email, data.Name.FirstName() + "@yopmail.com");
            Thread.Sleep(2000);
        }

        public void EnterMobileNumber()
        {
            this.WaitForElementToBeLoaded();
            this.EnterTextBySelection(MobileNumber, "0987654321");
            Thread.Sleep(2000);
        }
        public void EnterBillingAddress()
        {
            this.WaitForElementToBeVisible(BillingAddress);
            this.EnterText(BillingAddress,"12 New Delhi");
            this.ClickElement(SelectAddress);
            Thread.Sleep(2000);
        }

        public void EnterShippingAddress()
        {
            this.WaitForElementToBeVisible(ShippingAddress);
            this.EnterText(ShippingAddress, "12 Noida");
            this.ClickElement(SelectAddress);
            Thread.Sleep(2000);
        }

        public void SelectCustomerTypeValue()
        {
            this.ClickOnElement(SelectCustomerType);
            this.ClickOnElement(SelectCutomerTypeOption);
        }

        public void SelectSelectDeliveryMethodValue()
        {
            Thread.Sleep(2000);
            this.ClickElement(SelectDeliveryMethod);
            this.ClickElement(SelectDeliveryMethodOption);
        }

        public void SelectSelectPaymentMethodOptionValue()
        {
            Thread.Sleep(2000);
            this.ClickElement(SelectPaymentMethod);
            this.ClickElement(SelectPaymentMethodOption);
        }

        public void ClickOnSaveAndCloseBtn()
        {
            Thread.Sleep(2000);
            this.ClickElement(SaveAndCloseBtn);
        }
        public void ValidateNewCustomerIsCreatedSuccessfylly()
        {
            Thread.Sleep(2000);
            string actualSuccessMsg=GetElementText(SuccessMsg);
            string expectedSuccessMsg = "Successfully created.";
            this.softAssert.IsContains("IsCustomerCreated", expectedSuccessMsg, actualSuccessMsg);
        }
        public void ClickOnSales()
        {
            this.WaitForElementToBeLoaded();
            ClickElement(SalesBtn);
        }

        public void ClickOnCustomers()
        {
            ClickElement(CustomersBtn);
        }

        public void ValidateCustomersPageOpened()
        {
            this.WaitForElementToBeLoaded();
            string actualCustomerPageTitle = GetElementText(CustomerPageTitle);
            softAssert.IsContains("IsCustomersPageOpened", "Customers", actualCustomerPageTitle);
        }

        public void ClickOnCustomerType()
        {
            ClickElement(CustomerTypeBtn);
        }
        
        public void ValidateCustomerTypePageOpened()
        {
            this.WaitForElementToBeLoaded();
            string actualCustomerTypePageTitle = GetElementText(CustomerTypePageTitle);
            softAssert.IsContains("IsCustomerTypePageOpened", "Customer Type", actualCustomerTypePageTitle);
        }

        public void ClickOnCreateCustomer()
        {
            ClickElement(CreateCustomerBtn);
        }

        public void ClickOnNewCustomerType()
        {
            ClickElement(NewCustomerTypeBtn);
        }

        public void EnterCustomerTypeName()
        {
            this.WaitForElementToBeLoaded();
            CustomerName = data.Name.FirstName();
            EnterText(CustomerTypeName, CustomerName);
        }

        public void ClickOnAddCustomerType()
        {
            ClickElement(CustomerTypeAddBtn);
        }

        public void ValidateCustomerTypeIsCreated()
        {
            this.WaitForElementToBeLoaded();
            string actualSuccessMsg = GetElementText(CustomerTypeSuccessMsg);
            softAssert.IsContains("IsCustomerTypeCreated", "Successfully created.", actualSuccessMsg);
        }
        public void ValidateCustomerTypeIsDisplayed()
        {
            this.WaitForElementToBeLoaded();
            By locator = GetElement(CustomerTypeItem, CustomerName);
            string actualCustomerType = GetElementText(locator);
            softAssert.IsContains("IsCustomerTypeDisplayed", CustomerName, actualCustomerType);
        }

        public void ClickOnEditCustomerType()
        {
            ClickElement(CustomerTypeEditBtn);
        }

        public void ClickOnUpdateCustomerType()
        {
            ClickElement(CustomerTypeUpdateBtn);
        }

        public void ValidateCustomerTypeIsUpdated()
        {
            this.WaitForElementToBeLoaded();
            string actualSuccessMsg = GetElementText(CustomerTypeUpdateSuccessMsg);
            softAssert.IsContains("IsCustomerTypeUpdated", "Successfully updated.", actualSuccessMsg);
        }

        public void OpenCustomersPage()
        {
            this.ClickOnSales();
            this.LogInfoMessage(Log, "Clicked on Sales");
            this.ClickOnCustomers();
            this.LogInfoMessage(Log, "Clicked on Customers");
            this.ValidateCustomersPageOpened();
            this.LogInfoMessage(Log, "Customers page opened successfully");
        }
    }
}
