/*
Copyright 2020 Adobe
All Rights Reserved.

NOTICE: Adobe permits you to use, modify, and distribute this file in
accordance with the terms of the Adobe license agreement accompanying
it. If you have received this file from a source other than Adobe,
then your use, modification, or distribution of it requires the prior
written permission of Adobe. (See LICENSE-MIT in the root folder for details)
*/

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Foundation;
using Com.Adobe.Marketing.Mobile;

namespace ACPAnalyticsTestApp.iOS
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
            var callback = new Action<ACPMobilePrivacyStatus>(handleCallback);
            ACPCore.GetPrivacyStatus(callback);
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
            ACPCore.LogLevel = ACPMobileLogLevel.Verbose;
            stringOutput.SetResult("completed");
            return stringOutput;
        }

        public TaskCompletionSource<string> SetPrivacyStatus()
        {
            stringOutput = new TaskCompletionSource<string>();
            ACPCore.SetPrivacyStatus(ACPMobilePrivacyStatus.OptIn);
            stringOutput.SetResult("completed");
            return stringOutput;
        }

        public TaskCompletionSource<string> TrackAction()
        {
            stringOutput = new TaskCompletionSource<string>();
            var data = new NSMutableDictionary<NSString, NSString>
            {
                ["key"] = new NSString("value")
            };
            ACPCore.TrackAction("action", data);
            stringOutput.SetResult("completed");
            return stringOutput;
        }

        public TaskCompletionSource<string> TrackState()
        {
            stringOutput = new TaskCompletionSource<string>();
            var data = new NSMutableDictionary<NSString, NSString>
            {
                ["key"] = new NSString("value")
            };
            ACPCore.TrackState("state", data);
            stringOutput.SetResult("completed");
            return stringOutput;
        }

        public TaskCompletionSource<string> UpdateConfig()
        {
            stringOutput = new TaskCompletionSource<string>();
            var config = new NSMutableDictionary<NSString, NSObject>
            {
                ["someConfigKey"] = new NSString("configValue"),
                ["analytics.batchLimit"] = new NSNumber(5)
            };
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
            var callback = new Action<NSString>(handleCallback);
            ACPAnalytics.GetTrackingIdentifier(callback);
            stringOutput.SetResult("");
            return stringOutput;
        }

        public TaskCompletionSource<string> GetVisitorIdentifier()
        {
            stringOutput = new TaskCompletionSource<string>();
            var callback = new Action<NSString>(handleCallback);
            ACPAnalytics.GetVisitorIdentifier(callback);
            stringOutput.SetResult("");
            return stringOutput;
        }

        public TaskCompletionSource<string> GetQueueSize()
        {
            stringOutput = new TaskCompletionSource<string>();
            var callback = new Action<nuint>(handleCallback);
            ACPAnalytics.GetQueueSize(callback);
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
        private void handleCallback(ACPMobilePrivacyStatus privacyStatus)
        {
            Console.WriteLine("Privacy status: " + privacyStatus.ToString());
        }

        private void handleCallback(NSString content)
        {
            if(content == null)
            {
                Console.WriteLine("String callback is null");
            }
            else
            {
                Console.WriteLine("String callback: " + content);
            }
        }

        private void handleCallback(nuint value)
        {
            Console.WriteLine("Queue size: " + value);
        }

    }

}
