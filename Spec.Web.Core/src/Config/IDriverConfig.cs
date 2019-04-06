using OpenQA.Selenium;
using System;

namespace Spec.Web.Core.Config
{
    interface IDriverConfig
    {
        Uri GetServerUri();

        DriverOptions GetDriverOptions(double newCommandTimeout);

        DriverOptions GetDriverOptions(String platformName, String platformVersion, String deviceName, String browserName, double newCommandTimeout);
    }
}
