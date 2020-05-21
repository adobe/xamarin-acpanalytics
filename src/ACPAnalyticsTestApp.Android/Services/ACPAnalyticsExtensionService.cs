/*
Copyright 2020 Adobe
All Rights Reserved.

NOTICE: Adobe permits you to use, modify, and distribute this file in
accordance with the terms of the Adobe license agreement accompanying
it. If you have received this file from a source other than Adobe,
then your use, modification, or distribution of it requires the prior
written permission of Adobe. (See LICENSE-MIT for details)
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
            var data = new Dictionary<string, string>();
            data.Add("key", "value");
            ACPCore.TrackAction("action", data);
            stringOutput.SetResult("completed");
            return stringOutput;
        }

        public TaskCompletionSource<string> TrackState()
        {
            stringOutput = new TaskCompletionSource<string>();
            var data = new Dictionary<string, string>();
            data.Add("key", "value");
            ACPCore.TrackState("state", data);
            stringOutput.SetResult("completed");
            return stringOutput;
        }

        public TaskCompletionSource<string> UpdateConfig()
        {
            stringOutput = new TaskCompletionSource<string>();
            var config = new Dictionary<string, Java.Lang.Object>();
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
