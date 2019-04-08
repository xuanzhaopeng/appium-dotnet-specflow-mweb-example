using TechTalk.SpecFlow;

namespace Spec.Web.Bindings.Steps
{
    [Binding]
    public sealed class CommonSteps : BaseSteps
    {
        public CommonSteps(ScenarioContext injectedContext) : base(injectedContext) { }

        [StepDefinition("I navigate to CK website")]
        public void INavigateToCK()
        {
            testContext.Driver().Navigate().GoToUrl("https://www.calvinklein.nl");
        }
    }
}
