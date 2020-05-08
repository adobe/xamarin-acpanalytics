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
using System.Threading.Tasks;
using Android.Runtime;
using Com.Adobe.Marketing.Mobile;

namespace ACPAnalyticsTestApp.Droid
{
    public class ACPAnalyticsExtensionService : IACPAnalyticsExtensionService
    {
        TaskCompletionSource<string> stringOutput;

        public ACPAnalyticsExtensionService()
        {
        }
        
        // ACPCore methods
        public TaskCompletionSource<string> GetExtensionVersionCore()
        {
            stringOutput = new TaskCompletionSource<string>();
            stringOutput.SetResult(ACPCore.ExtensionVersion());
            return stringOutput;
        }

        public TaskCompletionSource<string> GetPrivacyStatus()
        {
            stringOutput = new TaskCompletionSource<string>();
            ACPCore.GetPrivacyStatus(new StringCallback());
            stringOutput.SetResult("completed");
            return stringOutput;
        }

        public TaskCompletionSource<string> SetAdvertisingIdentifier()
        {
            stringOutput = new TaskCompletionSource<string>();
            ACPCore.SetAdvertisingIdentifier("testAdvertisingIdentifier");
            stringOutput.SetResult("completed");
            return stringOutput;
        }

        public TaskCompletionSource<string> SetLogLevel()
        {
            stringOutput = new TaskCompletionSource<string>();
            ACPCore.LogLevel = LoggingMode.Verbose;
            stringOutput.SetResult("completed");
            return stringOutput;
        }

        public TaskCompletionSource<string> SetPrivacyStatus()
        {
            stringOutput = new TaskCompletionSource<string>();
            ACPCore.SetPrivacyStatus(MobilePrivacyStatus.OptIn);
            stringOutput.SetResult("completed");
            return stringOutput;
        }

        public TaskCompletionSource<string> TrackAction()
        {
            stringOutput = new TaskCompletionSource<string>();
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("key", "value");
            ACPCore.TrackAction("action", data);
            stringOutput.SetResult("completed");
            return stringOutput;
        }

        public TaskCompletionSource<string> TrackState()
        {
            stringOutput = new TaskCompletionSource<string>();
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("key", "value");
            ACPCore.TrackState("state", data);
            stringOutput.SetResult("completed");
            return stringOutput;
        }

        public TaskCompletionSource<string> UpdateConfig()
        {
            stringOutput = new TaskCompletionSource<string>();
            Dictionary<string, Java.Lang.Object> config = new Dictionary<string, Java.Lang.Object>();
            config.Add("someConfigKey", "configValue");
            config.Add("analytics.batchLimit", 5);
            ACPCore.UpdateConfiguration(config);
            stringOutput.SetResult("completed");
            return stringOutput;
        }

        // ACPAnalytics methods
        public TaskCompletionSource<string> GetExtensionVersionAnalytics()
        {
            stringOutput = new TaskCompletionSource<string>();
            stringOutput.SetResult(ACPAnalytics.ExtensionVersion());
            return stringOutput;
        }

        public TaskCompletionSource<string> ClearQueue()
        {
            stringOutput = new TaskCompletionSource<string>();
            ACPAnalytics.ClearQueue();
            stringOutput.SetResult("");
            return stringOutput;
        }

        public TaskCompletionSource<string> GetTrackingIdentifier()
        {
            stringOutput = new TaskCompletionSource<string>();
            ACPAnalytics.GetTrackingIdentifier(new StringCallback());
            stringOutput.SetResult("");
            return stringOutput;
        }

        public TaskCompletionSource<string> GetVisitorIdentifier()
        {
            stringOutput = new TaskCompletionSource<string>();
            ACPAnalytics.GetVisitorIdentifier(new StringCallback());
            stringOutput.SetResult("");
            return stringOutput;
        }

        public TaskCompletionSource<string> GetQueueSize()
        {
            stringOutput = new TaskCompletionSource<string>();
            ACPAnalytics.GetQueueSize(new StringCallback());
            stringOutput.SetResult("");
            return stringOutput;
        }

        public TaskCompletionSource<string> SetVisitorIdentifier()
        {
            stringOutput = new TaskCompletionSource<string>();
            ACPAnalytics.SetVisitorIdentifier("testVisitorIdentifier");
            stringOutput.SetResult("completed");
            return stringOutput;
        }

        public TaskCompletionSource<string> SendQueuedHits()
        {
            stringOutput = new TaskCompletionSource<string>();
            ACPAnalytics.SendQueuedHits();
            stringOutput.SetResult("completed");
            return stringOutput;
        }

        // callbacks
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
    }

}
