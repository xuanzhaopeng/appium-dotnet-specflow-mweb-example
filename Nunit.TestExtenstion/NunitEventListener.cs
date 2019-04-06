using NUnit.Engine;
using NUnit.Engine.Extensibility;
using System;

namespace Nunit.TestExtenstion
{
    [Extension]
    public class NunitEventListener : ITestEventListener
    {
        public void OnTestEvent(string report)
        {
            Console.Out.WriteLine(report);
        }
    }
}
