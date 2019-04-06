# Mobile web/Desktop Browser test starter

[![Build Status](https://travis-ci.org/xuanzhaopeng/appium-dotnet-specflow-mweb-example.svg?branch=master)](https://travis-ci.org/xuanzhaopeng/appium-dotnet-specflow-mweb-example)

> The project is in C#.Net, with the help of Appium/Selenium(.net version), NUnit and Specflow.  

### Configure driver and timeout
```xml
  <!-- You could config Driver and timeout in App.config -->
  <TestSettings>
    <Driver PlatformName="Android" BrowserName="Chrome" DeviceName="Android Emulator" ServerUrl="http://localhost:4723/wd/hub"></Driver>
    <Timeout NewCommandTimeout="90000" ImplicitWait="90000" PageLoad="90000"></Timeout>
  </TestSettings>
```
### Run test
* Run with SauceLabs Real Device (TestObject)
```bash
export SERVER_PROVIDER=SauceLabs
export SAUCE_KEY=<Your Sauce API Key>

nuget restore Spec.Web.sln
nuget install NUnit.Console -Version 3.9.0 -OutputDirectory testrunner
msbuild /p:Configuration=Release Spec.Web.sln
./testrunner/NUnit.ConsoleRunner.3.9.0/tools/nunit3-console.exe ./Spec.Web.Mobile/bin/Release/Spec.Web.Mobile.dll
```

* Run with local Appium and Android
```bash
nuget restore Spec.Web.sln
nuget install NUnit.Console -Version 3.9.0 -OutputDirectory testrunner
msbuild /p:Configuration=Release Spec.Web.sln
./testrunner/NUnit.ConsoleRunner.3.9.0/tools/nunit3-console.exe ./Spec.Web.Mobile/bin/Release/Spec.Web.Mobile.dll
```

### Project structure
* Spec.Web.Core: The core library that could be shared by all test assembly
* Spec.Web.Mobile: The mobile web test project for both Android and iOS.

### Copyright
Please kindly notice this project is only could be used for LEARNING usage of Appium / Selenium / Specflow in C#.Net environment.
