using Spec.Web.Bindings.Pages;
using TechTalk.SpecFlow;

namespace Spec.Web.Bindings.Steps
{
    [Binding]
    public sealed class CookiesPopupStep : BaseSteps
    {
        public CookiesPopupStep(ScenarioContext injectedContext) : base(injectedContext) { }

        [StepDefinition("I accept cookies setting in cookies popup")]
        public void IAcceptCookies()
        {
            testContext.GetPage<CookiesPopup>().AcceptButton.Click();
        }
    }
}
