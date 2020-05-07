
# Adobe Experience Platform - Analytics plugin for Xamarin apps

[![CI](https://github.com/adobe/xamarin-acpanalytics/workflows/CI/badge.svg)](https://github.com/adobe/xamarin-acpanalytics/actions)
[![npm](https://img.shields.io/npm/v/@adobe/xamarin-acpanalytics)](https://www.npmjs.com/package/@adobe/xamarin-acpanalytics)
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

# TODO (update after NuGet package is available on nuget.org)

**Package Manager Installation**

TODO

**Manual installation**

A local ACPAnalytics NuGet package can be created via the included Makefile. If building for the first time, run:

```
make setup
```

followed by:

```
make release
```

The created NuGet packages can be found in the `bin` directory and can be added as reference to a Xamarin project.

## Usage

### [Analytics](https://aep-sdks.gitbook.io/docs/using-mobile-extensions/adobe-analytics)

The following usage instructions assume [Xamarin Forms](https://dotnet.microsoft.com/apps/xamarin/xamarin-forms) is being used to develop a multiplatform mobile app.

##### Getting Analytics version:

**iOS**

```c#
public TaskCompletionSource<string> GetExtensionVersionAnalytics()
{
  stringOutput = new TaskCompletionSource<string>();
  stringOutput.SetResult(ACPAnalytics.ExtensionVersion);
  return stringOutput;
}
```

**Android**

```c#
public TaskCompletionSource<string> GetExtensionVersionAnalytics()
{
  stringOutput = new TaskCompletionSource<string>();
  stringOutput.SetResult(ACPAnalytics.ExtensionVersion());
  return stringOutput;
}
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
public TaskCompletionSource<string> GetTrackingIdentifier()
{
  stringOutput = new TaskCompletionSource<string>();
  Action<NSString> callback = new Action<NSString>(handleCallback);
  ACPAnalytics.GetTrackingIdentifier(callback);
  stringOutput.SetResult("");
  return stringOutput;
}

private void handleCallback(NSString content)
{
  Console.WriteLine("String callback: " + content);
}
```

**Android**

```c#
public TaskCompletionSource<string> GetTrackingIdentifier()
{
  stringOutput = new TaskCompletionSource<string>();
  ACPAnalytics.GetTrackingIdentifier(new StringCallback());
  stringOutput.SetResult("");
  return stringOutput;
}

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
public TaskCompletionSource<string> SendQueuedHits()
{
  stringOutput = new TaskCompletionSource<string>();
  ACPAnalytics.SendQueuedHits();
  stringOutput.SetResult("completed");
  return stringOutput;
}
```

##### Get the queue size:

**iOS**

```c#
public TaskCompletionSource<string> GetQueueSize()
{
  stringOutput = new TaskCompletionSource<string>();
  Action<nuint> callback = new Action<nuint>(handleCallback);
  ACPAnalytics.GetQueueSize(callback);
  stringOutput.SetResult("");
  return stringOutput;
}

private void handleCallback(nuint value)
{
  Console.WriteLine("Queue size: " + value);
}
```

**Android**

```c#
public TaskCompletionSource<string> GetQueueSize()
{
  stringOutput = new TaskCompletionSource<string>();
  ACPAnalytics.GetQueueSize(new StringCallback());
  stringOutput.SetResult("");
  return stringOutput;
}

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
public TaskCompletionSource<string> ClearQueue()
{
  stringOutput = new TaskCompletionSource<string>();
  ACPAnalytics.ClearQueue(); 
  stringOutput.SetResult("");
  return stringOutput;
}
```

##### Set the custom visitor identifier:

**iOS and Android**

```js
public TaskCompletionSource<string> SetVisitorIdentifier()
{
  stringOutput = new TaskCompletionSource<string>();
  ACPAnalytics.SetVisitorIdentifier("testVisitorIdentifier");
  stringOutput.SetResult("completed");
  return stringOutput;
}
```
##### Get the custom visitor identifier:

**iOS**

```c#
public TaskCompletionSource<string> GetVisitorIdentifier()
{
  stringOutput = new TaskCompletionSource<string>();
  Action<NSString> callback = new Action<NSString>(handleCallback);
  ACPAnalytics.GetVisitorIdentifier(callback);
  stringOutput.SetResult("");
  return stringOutput;
}

private void handleCallback(NSString content)
{
  Console.WriteLine("String callback: " + content);
}
```

**Android**

```c#
public TaskCompletionSource<string> GetVisitorIdentifier()
{
  stringOutput = new TaskCompletionSource<string>();
  ACPAnalytics.GetVisitorIdentifier(new StringCallback());
  stringOutput.SetResult("");
  return stringOutput;
}

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
