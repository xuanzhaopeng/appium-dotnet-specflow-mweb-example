using TechTalk.SpecFlow;

namespace Spec.Web.Android.Steps
{
    [Binding]
    public sealed class GlobalIndexPageSteps
    {
        private readonly ScenarioContext context;

        public GlobalIndexPageSteps(ScenarioContext injectedContext)
        {
            context = injectedContext;
        }
    }
}
