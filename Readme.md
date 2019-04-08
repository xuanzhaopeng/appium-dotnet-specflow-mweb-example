# Mobile web/Desktop Browser test starter

<p align="center">
  <img height="120px" src="https://pbs.twimg.com/profile_images/704462412880093184/IkgItC1R_400x400.jpg" />&nbsp;&nbsp;&nbsp;&nbsp;
  <img height="120px" src="https://d1qb2nb5cznatu.cloudfront.net/startups/i/363533-5515f774a5428408331f0358102d9f43-medium_jpg.jpg?buster=1408423281" />&nbsp;&nbsp;&nbsp;&nbsp;
  <img height="120px" src="https://jfiaffe.files.wordpress.com/2015/07/specflow-logo.png" />&nbsp;&nbsp;&nbsp;&nbsp;
  <img height="120px" src="https://avatars2.githubusercontent.com/u/5879127?s=280&v=4" />
</p>

[![Build Status](https://travis-ci.org/xuanzhaopeng/appium-dotnet-specflow-mweb-example.svg?branch=master)](https://travis-ci.org/xuanzhaopeng/appium-dotnet-specflow-mweb-example)

> The project is in C#.Net, with the help of Appium/Selenium(.net version), NUnit, Specflow and Allure.  

### Proejct logic
![Project logic](structure.png?raw=true "Project logic")

### Configure driver and timeout
```xml
  <!-- You could config Driver and timeout in App.config -->
  <TestSettings>
    <Driver PlatformName="Android" BrowserName="Chrome" DeviceName="Android Emulator" ServerUrl="http://localhost:4723/wd/hub"></Driver>
	<!-- You also could add SauceLabsDriver / BrowserStackDriver -->
  </TestSettings>
```
### Predefined Drivers
|                    | Support Mobile Web | Support Desktop Web |
|--------------------|:------------------:|:-------------------:|
| LocalDriver        |          ✓         |          ✓          |
| SauceLabsDriver    |          ✓         |                     |
| BrowserStackDriver |                    |          ✓          |

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
# Ensure you start Appium locally and has conntected Android or Android emulator

nuget restore Spec.Web.sln
nuget install NUnit.Console -Version 3.9.0 -OutputDirectory testrunner
msbuild /p:Configuration=Release Spec.Web.sln
./testrunner/NUnit.ConsoleRunner.3.9.0/tools/nunit3-console.exe ./Spec.Web.Mobile/bin/Release/Spec.Web.Mobile.dll
```

* Run with BrowserStack Desktop Browser
```bash
export SERVER_PROVIDER=BrowserStack
export BS_USER=<Your BrowserStack User>
export BS_KEY=<Your BrowserStack Key>

nuget restore Spec.Web.sln
nuget install NUnit.Console -Version 3.9.0 -OutputDirectory testrunner
msbuild /p:Configuration=Release Spec.Web.sln
./testrunner/NUnit.ConsoleRunner.3.9.0/tools/nunit3-console.exe ./Spec.Web.Desktop/bin/Release/Spec.Web.Desktop.dll
```

* Run with local Chrome Desktop browser
```bash
# Ensure you start selenium standalone server with related chrome driver

nuget restore Spec.Web.sln
nuget install NUnit.Console -Version 3.9.0 -OutputDirectory testrunner
msbuild /p:Configuration=Release Spec.Web.sln
./testrunner/NUnit.ConsoleRunner.3.9.0/tools/nunit3-console.exe ./Spec.Web.Desktop/bin/Release/Spec.Web.Desktop.dll
```

### Test Report
It will generate Allure test report in allure-results folder where the assemly located. Please kindly noticed I do not really properly configured the Allure report, you could extend it such as adding screenshots or recorded videos into report.

### Project structure
* Spec.Web.Core: The core library that could be shared by all test assembly
* Spec.Web.Mobile: The mobile web test project for both Android and iOS.
* Spec.Web.Desktop: The desktop browser test project
* Spec.Web.Bindings: Specflow common bindings(Hook & StepDefinitions) and PageObjects

### Copyright
Please kindly notice this project is only could be used for LEARNING usage of Appium / Selenium / Specflow in C#.Net environment.
