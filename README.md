
# Adobe Experience Platform - Analytics plugin for Xamarin apps

![CI](https://github.com/adobe/xamarin-acpanalytics/workflows/CI/badge.svg)

[![GitHub](https://img.shields.io/github/license/adobe/xamarin-acpanalytics)](https://github.com/adobe/xamarin-acpanalytics/blob/master/LICENSE)

- [Prerequisites](#prerequisites)  
- [Installation](#installation)
- [Usage](#usage)  
- [Running Tests](#running-tests)
- [Sample App](#sample-app)  
- [Contributing](#contributing)  
- [Licensing](#licensing)  

## Prerequisites  

Xamarin development requires the installation of [Microsoft Visual Studio](https://visualstudio.microsoft.com/downloads/). An Apple developer account and the latest version of Xcode (available from the App Store) are required if you are [building an iOS app](https://docs.microsoft.com/en-us/visualstudio/mac/installation?view=vsmac-2019).

## Installation

**Package Manager Installation**

The ACPAnalytics Xamarin NuGet package for Android or iOS can be added to your project by right clicking the *_"Packages"_* folder within the project you are working on then selecting *_"Manage NuGet Packages"_*. In the window that opens, ensure that your selected source is `nuget.org` and search for *_"Adobe.ACP"_*. After selecting the Xamarin AEP SDK packages that are required, click on the *_"Add Packages"_* button.

**Manual installation**

Local ACPAnalytics NuGet packages can be created via the included Makefile. If building for the first time, run:

```
make setup
```

followed by:

```
make release
```

The created NuGet packages can be found in the `bin` directory and can be added as a reference to a Xamarin project.

## Usage

### [Analytics](https://aep-sdks.gitbook.io/docs/using-mobile-extensions/adobe-analytics)

##### Getting Analytics version:

**iOS and Android**

```c#
Console.WriteLine(ACPAnalytics.ExtensionVersion());
```

##### Registering the extension with ACPCore:  

  ##### **iOS** and Android
```c#
using Com.Adobe.Marketing.Mobile;

ACPAnalytics.RegisterExtension();
ACPCore.Start(null);
```
##### Get the tracking identifier:

**iOS**

```c#
var callback = new Action<NSString>(handleCallback);
ACPAnalytics.GetTrackingIdentifier(callback);

private void handleCallback(NSString content)
{
  Console.WriteLine("String callback: " + content);
}
```

**Android**

```c#
ACPAnalytics.GetTrackingIdentifier(new StringCallback());

class StringCallback : Java.Lang.Object, IAdobeCallback
{
  public void Call(Java.Lang.Object stringContent)
  {
    if (stringContent != null)
    {
      Console.WriteLine("String callback content: " + stringContent);
    }
    else
    {
      Console.WriteLine("null content in string callback");
    }
  }
}
```

##### Send queued hits:

**iOS and Android**

```c#
ACPAnalytics.SendQueuedHits();
```

##### Get the queue size:

**iOS**

```c#
var callback = new Action<nuint>(handleCallback);
ACPAnalytics.GetQueueSize(callback);

private void handleCallback(nuint value)
{
  Console.WriteLine("Queue size: " + value);
}
```

**Android**

```c#
ACPAnalytics.GetQueueSize(new StringCallback());

class StringCallback : Java.Lang.Object, IAdobeCallback
{
  public void Call(Java.Lang.Object stringContent)
  {
    if (stringContent != null)
    {
      Console.WriteLine("String callback content: " + stringContent);
    }
    else
    {
      Console.WriteLine("null content in string callback");
    }
  }
}
```

##### Clear queued hits:

**iOS and Android**

```c#
ACPAnalytics.ClearQueue(); 
```

##### Set the custom visitor identifier:

**iOS and Android**

```js
ACPAnalytics.SetVisitorIdentifier("testVisitorIdentifier");
```
##### Get the custom visitor identifier:

**iOS**

```c#
var callback = new Action<NSString>(handleCallback);
ACPAnalytics.GetVisitorIdentifier(callback);

private void handleCallback(NSString content)
{
  Console.WriteLine("String callback: " + content);
}
```

**Android**

```c#
ACPAnalytics.GetVisitorIdentifier(new StringCallback());

class StringCallback : Java.Lang.Object, IAdobeCallback
{
  public void Call(Java.Lang.Object stringContent)
  {
    if (stringContent != null)
    {
      Console.WriteLine("String callback content: " + stringContent);
    }
    else
    {
      Console.WriteLine("null content in string callback");
    }
  }
}
```

##### Running Tests

iOS and Android unit tests are included within the ACPAnalytics binding solution. Currently they must be built from within Visual Studio then manually triggered from the unit test app that is deployed to an iOS or Android device.

## Sample App

A Xamarin Forms sample app is provided in the Xamarin ACPAnalytics solution file.

## Contributing
Looking to contribute to this project? Please review our [Contributing guidelines](.github/CONTRIBUTING.md) prior to opening a pull request.

We look forward to working with you!

## Licensing  
This project is licensed under the Apache V2 License. See [LICENSE](LICENSE) for more information.
