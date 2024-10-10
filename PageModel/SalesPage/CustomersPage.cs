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
