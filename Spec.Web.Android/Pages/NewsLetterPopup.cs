using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using Spec.Web.Core.Pages;

namespace Spec.Web.Android.Pages
{
    public class NewsLetterPopup : BasePage
    {
        [FindsBy(How = How.XPath, Using = "//div[contains(@class, 'newsletter') and contains(@class, 'newsletter--modal')]")]
        public IWebElement Popup;

        [FindsBy(How = How.XPath, Using = "//div[@class='ReactModal__Close']")]
        public IWebElement CloseButton;
    }
}
