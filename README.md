# Adobe Experience Platform - Analytics plugin for Xamarin apps

## Notice of deprecation

Since *April 25, 2023*, [Apple has required](https://developer.apple.com/news/?id=jd9wcyov) apps submitted to the App Store to be built with Xcode 14.1 or later. The Experience Platform Mobile SDKs and extensions outlined below were built with prior versions of Xcode and are no longer compatible with iOS and iPadOS given Apple’s current App Store requirements. Consequently, on *August 31, 2023*, Adobe will be deprecating support for the following Experience Platform Mobile SDKs and wrapper extensions:

- [ACP iOS SDK](https://developer.adobe.com/client-sdks/previous-versions/documentation/sdk-versions/#ios)
- [Cordova](https://developer.adobe.com/client-sdks/previous-versions/documentation/sdk-versions/#cordova)
- [Flutter for ACP](https://developer.adobe.com/client-sdks/previous-versions/documentation/sdk-versions/#flutter)
- [React Native for ACP](https://developer.adobe.com/client-sdks/previous-versions/documentation/sdk-versions/#react-native)
- [Xamarin](https://developer.adobe.com/client-sdks/previous-versions/documentation/sdk-versions/#xamarin)

After *August 31, 2023*, applications already submitted to the App Store that contain these SDKs and wrapper extensions will continue to operate, however, Adobe will not be providing security updates or bug fixes, and these SDKs and wrapper extensions will be provided as-is exclusive of any warranty, due to the App Store policy outlined above.

We encourage all customers to migrate to the latest Adobe Experience Platform versions of the Mobile SDK to ensure continued compatibility and support. Documentation for the latest versions of the Adobe Experience Platform Mobile SDKs can be found [here](https://developer.adobe.com/client-sdks/documentation/current-sdk-versions/). The iOS migration guide can be found [here](https://developer.adobe.com/client-sdks/previous-versions/documentation/migrate-to-swift/).

---

![CI](https://github.com/adobe/xamarin-acpanalytics/workflows/CI/badge.svg)
[![NuGet](https://buildstats.info/nuget/Adobe.ACPAnalytics.Android)](https://www.nuget.org/packages/Adobe.ACPAnalytics.Android/)
[![NuGet](https://buildstats.info/nuget/Adobe.ACPAnalytics.iOS)](https://www.nuget.org/packages/Adobe.ACPAnalytics.iOS/)
[![NuGet](https://buildstats.info/nuget/Adobe.ACPAnalytics.tvOS)](https://www.nuget.org/packages/Adobe.ACPAnalytics.tvOS/)
[![GitHub](https://img.shields.io/github/license/adobe/xamarin-acpanalytics)](https://github.com/adobe/xamarin-acpanalytics/blob/master/LICENSE)

- [Prerequisites](#prerequisites)  
- [Installation](#installation)
- [Usage](#usage)  
- [Running Tests](#running-tests)
- [Sample App](#sample-app)  
- [Contributing](#contributing)  
- [Licensing](#licensing)  

## Prerequisites  

Xamarin development requires the installation of [Microsoft Visual Studio](https://visualstudio.microsoft.com/downloads/). Information regarding installation for Xamarin development is available for [Mac](https://docs.microsoft.com/en-us/visualstudio/mac/installation?view=vsmac-2019) or [Windows](https://docs.microsoft.com/en-us/visualstudio/install/install-visual-studio?view=vs-2019).

An [Apple developer account](https://developer.apple.com/programs/enroll/) and the latest version of Xcode (available from the App Store) are required if you are [building an iOS or tvOS app](https://docs.microsoft.com/en-us/visualstudio/mac/installation?view=vsmac-2019).

## Installation

**Package Manager Installation**

The ACPAnalytics Xamarin NuGet package for Android or iOS or tvOS can be added to your project by right clicking the *_"Packages"_* folder within the project you are working on then selecting *_"Manage NuGet Packages"_*. In the window that opens, ensure that your selected source is `nuget.org` and search for *_"Adobe.ACP"_*. After selecting the Xamarin AEP SDK packages that are required, click on the *_"Add Packages"_* button. After exiting the "Add Packages" menu, right click the main solution or the "Packages" folder and select "Restore" to ensure the added packages are downloaded.

**Manual installation**

Local ACPAnalytics NuGet packages can be created via the included Makefile. If building for the first time, run:

```
make setup
```

followed by:

```
make release
```

The created NuGet packages can be found in the `bin` directory. This directory can be added as a local nuget source and packages within the directory can be added to a Xamarin project following the steps in the "Package Manager Installation" above.

## Usage

The ACPAnalytics binding can be opened by loading the ACPAnalyticsBinding.sln with Visual Studio. The following targets are available in the solution:

- Adobe.ACPAnalytics.iOS - The ACPAnalytics iOS bindings.
- Adobe.ACPAnalytics.tvOS - The ACPAnalytics tvOS bindings.
- Adobe.ACPAnalytics.Android - The ACPAnalytics Android binding.
- ACPAnalyticsTestApp - The Xamarin.Forms base app used by the iOS and Android test apps.
- ACPAnalyticsTestApp.iOS - The Xamarin.Forms based iOS manual test app.
- ACPAnalyticsTestApp.tvOS - The Storyboard based tvOS manual test app.
- ACPAnalyticsTestApp.Android - The Xamarin.Forms based Android manual test app.
- ACPAnalyticsiOSUnitTests - iOS unit test app.
- ACPAnalyticsAndroidUnitTests - Android unit test app.

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

iOS and Android unit tests are included within the ACPAnalytics binding solution. They must be built from within Visual Studio then manually triggered from the unit test app that is deployed to an iOS or Android device.

## Sample App

A Xamarin Forms sample app is provided in the Xamarin ACPAnalytics solution file.

## Contributing
Looking to contribute to this project? Please review our [Contributing guidelines](.github/CONTRIBUTING.md) prior to opening a pull request.

We look forward to working with you!

## Licensing  
This project is licensed under the Apache V2 License. See [LICENSE](LICENSE) for more information.
