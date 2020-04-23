
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

# TODO
update me for xamarin

## Prerequisites  

Cordova is distributed via [Node Package Management](https://www.npmjs.com/) (aka - `npm`).  

In order to install and build Cordova applications you will need to have `Node.js` installed. [Install Node.js](https://nodejs.org/en/).  

Once Node.js is installed, you can install the Cordova framework from terminal:  

```  
sudo npm install -g cordova  
```  

## Installation

To start using the Analytics plugin for Cordova, navigate to the directory of your Cordova app and install the plugin:
```
cordova plugin add https://github.com/adobe/cordova-acpanalytics.git
```
Check out the documentation for help with APIs

## Usage

##### Getting the SDK version:
```js
ACPAnalytics.extensionVersion(function(version){  
    console.log(version);
}, function(error){  
    console.log(error);  
});
```
##### Registering the extension with ACPCore:  

 > Note: It is required to initialize the SDK via native code inside your AppDelegate and MainApplication for iOS and Android respectively. For more information see how to initialize [Core](https://aep-sdks.gitbook.io/docs/getting-started/initialize-the-sdk).  
  ##### **iOS**  
```objective-c
#import "ACPAnalytics.h"  
[ACPAnalytics registerExtension];  
```  
  ##### **Android:**  
```java
import com.adobe.marketing.mobile.Analytics;  
Analytics.registerExtension();
```
##### Get the tracking identifier:
```js
ACPAnalytics.getTrackingIdentifier(function(trackingId) {  
    console.log(trackingId);
}, function(error){  
    console.log(error);  
});
```
##### Send queued hits:
```js
ACPAnalytics.sendQueuedHits(function(response){  
    console.log("Success in sendQueuedHits");  
}, function(error){  
    console.log(error);  
});  
```
##### Get the queue size:
```js
ACPAnalytics.getQueueSize(function(size) {  
    console.log(size);
}, function(error){  
    console.log(error);  
});
```
##### Clear queued hits:
```js
ACPAnalytics.clearQueue(function(response){  
    console.log("Success in clearing queue");  
}, function(error){  
    console.log(error);  
});
```
##### Set the custom visitor identifier:
```js
ACPAnalytics.setVisitorIdentifier(customVisitorId, function(response) {  
    console.log("Success in setting visitor Id with " + customVisitorId);  
}, function(error){  
    console.log(error);  
});
```
##### Get the custom visitor identifier:
```js
ACPAnalytics.getVisitorIdentifier(function(visitorId) {  
    console.log(visitorId);
}, function(error){  
    console.log(error);  
});
```  

## Running Tests
Install cordova-paramedic `https://github.com/apache/cordova-paramedic`
```bash
npm install -g cordova-paramedic
```

Run the tests
```
cordova-paramedic --platform ios --plugin . --verbose
```
```
cordova-paramedic --platform android --plugin . --verbose
```

## Sample App

A Cordova app for testing the plugin is located in the `https://github.com/adobe/cordova-acpsample`. The app is configured for both iOS and Android platforms.  

## Contributing
Looking to contribute to this project? Please review our [Contributing guidelines](.github/CONTRIBUTING.md) prior to opening a pull request.

We look forward to working with you!

## Licensing  
This project is licensed under the Apache V2 License. See [LICENSE](LICENSE) for more information.
