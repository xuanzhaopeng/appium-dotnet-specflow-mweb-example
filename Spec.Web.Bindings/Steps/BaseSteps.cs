using Spec.Web.Core;
using Spec.Web.Core.Specflow;
using TechTalk.SpecFlow;

namespace Spec.Web.Bindings.Steps
{
    [Binding]
    public abstract class BaseSteps
    {
        protected readonly ScenarioContext context;
        protected readonly WebTestContext testContext;
        public BaseSteps(ScenarioContext injectedContext)
        {
            context = injectedContext;
            testContext = context.Get<WebTestContext>(ScenarioContextTypes.Driver);
        }
    }
}
