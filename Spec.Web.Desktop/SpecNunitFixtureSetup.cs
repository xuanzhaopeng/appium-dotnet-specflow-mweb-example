using NUnit.Framework;
using System;
using System.IO;

namespace Spec.Web.Desktop.Features
{
    [SetUpFixture]
    public class SpecNunitFixtureSetup
    {
        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            // CurrentDirectory will be used by Allure.Specflow to generate allure-results folder
            Environment.CurrentDirectory = Path.GetDirectoryName(GetType().Assembly.Location);
        }

        [OneTimeTearDown]
        public void RunAfterAnyTests()
        {
        }
    }
}
