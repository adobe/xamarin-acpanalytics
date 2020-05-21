/*
 Copyright 2020 Adobe. All rights reserved.
 This file is licensed to you under the Apache License, Version 2.0 (the "License");
 you may not use this file except in compliance with the License. You may obtain a copy
 of the License at http://www.apache.org/licenses/LICENSE-2.0
 Unless required by applicable law or agreed to in writing, software distributed under
 the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR REPRESENTATIONS
 OF ANY KIND, either express or implied. See the License for the specific language
 governing permissions and limitations under the License.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Foundation;
using UIKit;
using Com.Adobe.Marketing.Mobile;

namespace ACPAnalyticsTestApp.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());

            // set the wrapper type
            ACPCore.SetWrapperType(ACPMobileWrapperType.Xamarin);

            // set log level
            ACPCore.LogLevel = ACPMobileLogLevel.Verbose;

            // register SDK extensions
            ACPLifecycle.RegisterExtension();
            ACPIdentity.RegisterExtension();
            ACPAnalytics.RegisterExtension();

            // start core
            ACPCore.Start(startCallback);

            // register dependency service to link interface from ACPCoreTestApp base project
            DependencyService.Register<IACPAnalyticsExtensionService, ACPAnalyticsExtensionService>();
            return base.FinishedLaunching(app, options);
        }

        // Called when the application is launched and every time the app returns to the foreground.
        public override void OnActivated(UIApplication uiApplication)
        {
            base.OnActivated(uiApplication);
            ACPCore.LifecycleStart(null);
        }

        // Called when the application is about to enter the background, be suspended, or when the user receives an interruption such as a phone call or text.
        public override void OnResignActivation(UIApplication uiApplication)
        {
            base.OnResignActivation(uiApplication);
            ACPCore.LifecyclePause();
        }

        private void startCallback()
        {
            // set launch config
            ACPCore.ConfigureWithAppID("launch-ENf8ed5382efc84d5b81a9be8dcc231be1-development");
        }
    }
}
