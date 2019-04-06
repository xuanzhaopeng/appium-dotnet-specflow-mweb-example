# # Mobile web/Desktop Browser test starter

>> The project is in C#.Net, with the help of Appium/Selenium(.net version), NUnit and Specflow.  

### Configure driver and timeout
```xml
  <!-- You could config Driver and timeout in App.config -->
  <TestSettings>
    <Driver PlatformName="Android" BrowserName="Chrome" DeviceName="Android Emulator" ServerUrl="http://localhost:4723/wd/hub"></Driver>
    <Timeout NewCommandTimeout="90000" ImplicitWait="90000" PageLoad="90000"></Timeout>
  </TestSettings>
```
### Project structure
* Spec.Web.Core: The core library that could be shared by all test assembly
* Spec.Web.Android: The android mobile web test project, but I gonnna rename it to Spec.Web.Mobile as it could be shared between IOS and Android mweb test.

### Copyright
Please kindly notice this project is only could be used for LEARNING usage of Appium / Selenium / Specflow in C#.Net environment.
