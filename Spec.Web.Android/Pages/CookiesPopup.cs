using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using Spec.Web.Core.Pages;

namespace Spec.Web.Android.Pages
{
    public class CookiesPopup : BasePage
    {
        [FindsBy(How = How.XPath, Using = "//div[@class='cookie-notice']")]
        public IWebElement Popup;

        [FindsBy(How = How.XPath, Using = "//div[@class='cookie-notice__actions']/button[1]")]
        public IWebElement MoreInfoButton;

        [FindsBy(How = How.XPath, Using = "//div[@class='cookie-notice__actions']/button[2]")]
        public IWebElement AcceptButton;
    }
}
