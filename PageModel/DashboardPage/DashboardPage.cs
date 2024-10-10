using MochaHomeAccounting.Utilities;
using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using MochaHomeAccounting.Utilities;

namespace MochaHomeAccounting.PageModel.DashboardPage
{
    class DashboardPage : BasePageModel.BasePageModel
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        SoftAssert softAssert;
        public DashboardPage(BaseTestContext baseTestContext) : base(baseTestContext)
        {
            this.softAssert = new(baseTestContext);
        }

        private static readonly By LogoImg = By.XPath("//img[@alt='Mocha Technologies']");

        public void ValidateDashboardPageOpened()
        {
            this.WaitForElementToBeVisible(LogoImg);
            bool isLogoImgDisplayed = IsElementPresentAndVisible(LogoImg, Log);
            this.softAssert.IsTrue("IsLogoImgDisplayed", isLogoImgDisplayed);
            this.LogInfoMessage(Log, "User is logged in successfully");
        }
    }
}
