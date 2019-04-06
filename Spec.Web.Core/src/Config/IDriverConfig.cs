using OpenQA.Selenium;
using System;

namespace Spec.Web.Core.Config
{
    interface IDriverConfig
    {
        Uri GetServerUri();

        DriverOptions GetDriverOptions();

        DriverOptions GetDriverOptions(String platformName, String platformVersion, String deviceName, String browserName);

        Double GetNewCommandTimeout();

        Double GetImplicitWait();

        Double GetPageLoadTimeout();
    }
}
