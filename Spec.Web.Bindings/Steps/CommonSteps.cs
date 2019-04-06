using Spec.Web.Core;
using Spec.Web.Core.Specflow;
using TechTalk.SpecFlow;

namespace Spec.Web.Bindings.Steps
{
    [Binding]
    public sealed class CommonSteps
    {
        private readonly ScenarioContext context;
        private readonly WebTestContext testContext;

        public CommonSteps(ScenarioContext injectedContext)
        {
            context = injectedContext;
            testContext = context.Get<WebTestContext>(ScenarioContextTypes.Driver);
        }

        [StepDefinition("I navigate to CK website")]
        public void INavigateToCK()
        {
            testContext.Driver().Navigate().GoToUrl("https://www.calvinklein.nl");
        }
    }
}
