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
using NUnit.Framework;
using Foundation;
using Com.Adobe.Marketing.Mobile;
using System.Threading;

namespace ACPAnalyticsiOSUnitTests
{
    [TestFixture]
    public class ACPAnalyticsiOSUnitTests
    {
        [SetUp]
        public void Setup()
        {
            ACPAnalytics.ClearQueue();
            ACPCore.SetPrivacyStatus(ACPMobilePrivacyStatus.OptIn);
        }

        // ACPAnalytics tests
        [Test]
        public void GetACPAnalyticsExtensionVersion_Returns_CorrectVersion()
        {
            // verify
            Assert.That(ACPAnalytics.ExtensionVersion(), Is.EqualTo("2.5.2"));
        }

        [Test]
        public void GetQueueSize_Returns_CorrectQueueSize()
        {
            // setup
            var latch = new CountdownEvent(1);
            nuint queueSize = 0;
            var config = new NSMutableDictionary<NSString, NSObject>
            {
                ["analytics.batchLimit"] = new NSNumber(5)
            };
            ACPCore.UpdateConfiguration(config);
            ACPCore.TrackAction("action", null);
            ACPCore.TrackAction("action", null);
            // test
            ACPAnalytics.GetQueueSize(callback =>
            {
                queueSize = callback;
                latch.Signal();
            });
            latch.Wait();
            latch.Dispose();
            // verify
            Assert.That(queueSize, Is.EqualTo(2));
        }

        [Test]
        public void ClearQueue_Clears_QueuedHits()
        {
            // setup
            var latch = new CountdownEvent(1);
            nuint queueSize = 0;
            var config = new NSMutableDictionary<NSString, NSObject>
            {
                ["analytics.batchLimit"] = new NSNumber(5)
            };
            ACPCore.UpdateConfiguration(config);
            ACPCore.TrackAction("action", null);
            ACPCore.TrackAction("action", null);
            ACPCore.TrackAction("action", null);
            // test
            ACPAnalytics.GetQueueSize(callback =>
            {
                queueSize = callback;
                latch.Signal();
            });
            latch.Wait();
            latch.Dispose();
            // verify
            Assert.That(queueSize, Is.EqualTo(3));
            // test
            ACPAnalytics.ClearQueue();
            latch = new CountdownEvent(1);
            ACPAnalytics.GetQueueSize(callback =>
            {
                queueSize = callback;
                latch.Signal();
            });
            latch.Wait();
            latch.Dispose();
            // verify
            Assert.That(queueSize, Is.EqualTo(0));
        }

        [Test]
        public void GetCustomVisitorIdentifier_Gets_PreviouslySetCustomVisitorIdentifier()
        {
            // setup
            var latch = new CountdownEvent(1);
            string retrievedIdentifier = "";
            string expectedIdentifier = "someVisitorIdentifier";
            ACPAnalytics.SetVisitorIdentifier(expectedIdentifier);
            // test
            ACPAnalytics.GetVisitorIdentifier(callback =>
            {
                retrievedIdentifier = callback.ToString();
                latch.Signal();
            });
            latch.Wait();
            latch.Dispose();
            // verify
            Assert.That(retrievedIdentifier, Is.EqualTo(expectedIdentifier));
        }
    }
}