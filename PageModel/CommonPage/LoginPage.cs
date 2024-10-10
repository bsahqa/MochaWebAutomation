using NUnit.Framework;
using OpenQA.Selenium;
using MochaHomeAccounting.Utilities;

namespace MochaHomeAccounting.PageModel.CommonPage
{
    public class LoginPage : BasePageModel.BasePageModel
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public LoginPage(BaseTestContext baseTestContext) : base(baseTestContext) { }
        private static readonly By Username = By.Id("email");
        private static readonly By Password = By.XPath("//input[@name='password']");
        private static readonly By LoginBtn = By.XPath("//button[@type='submit']");
        private static readonly By SignBtn = By.XPath("(//button[text()='Login'])[1]");

        public void ClickOnSignBtn()
        {
            this.ClickElement(SignBtn);
        }
        public void TypeUsername(string username)
        {
            this.EnterText(Username, username);
        }

        public void TypePassword(string password)
        {
            this.EnterText(Password, password);
        }

        public void ClickLoginButton()
        {
            this.ClickElement(LoginBtn);
        }

        public void ValidateLogin(string username, string pass)
        {
            this.ClickOnSignBtn();
            this.WaitForElementToBeVisible(Username);
            this.TypeUsername(username);
            this.LogInfoMessage(Log, $"Entered username: {username}");
            this.TypePassword(pass);
            this.LogInfoMessage(Log, $"Entered password: {pass}");
            this.ClickLoginButton();
            this.LogInfoMessage(Log, "Clicked on Login button");
        }
    }
}
