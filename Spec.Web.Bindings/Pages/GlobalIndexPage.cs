using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using Spec.Web.Core.Pages;

namespace Spec.Web.Bindings.Pages
{
    public class GlobalIndexPage : BasePage
    {
        [FindsBy(How = How.XPath, Using = "//a[@data-track-button-id='sp_19_cku_splash_globalcampaign_men']")]
        public IWebElement EntryButtonOfMenShop;

        [FindsBy(How = How.XPath, Using = "//a[@data-track-button-id='sp_19_cku_splash_globalcampaign_women']")]
        public IWebElement EntryButtonOfWomenShop;
    }
}
