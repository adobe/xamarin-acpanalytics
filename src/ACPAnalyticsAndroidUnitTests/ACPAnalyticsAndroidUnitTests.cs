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
using Com.Adobe.Marketing.Mobile;
using System.Threading;
using System.Collections.Generic;

namespace ACPAnalyticsAndroidUnitTests
{
    [TestFixture]
    public class ACPAnalyticsAndroidUnitTests
    {
        static int retrievedQueueSize;
        static string retrievedVisitorIdentifier;
        static CountdownEvent latch;

        [SetUp]
        public void Setup()
        {
            retrievedQueueSize = 0;
            retrievedVisitorIdentifier = "";
            latch = null;
            ACPAnalytics.ClearQueue();
            ACPCore.SetPrivacyStatus(MobilePrivacyStatus.OptIn);
        }

        // ACPAnalytics tests
        [Test]
        public void GetACPAnalyticsExtensionVersion_Returns_CorrectVersion()
        {
            // verify
            Assert.That(ACPAnalytics.ExtensionVersion(), Is.EqualTo("1.2.4"));
        }

        [Test]
        public void GetQueueSize_Returns_CorrectQueueSize()
        {
            // setup
            CountdownEvent latch = new CountdownEvent(1);
            int expectedSize = 2;
            Dictionary<string, Java.Lang.Object> config = new Dictionary<string, Java.Lang.Object>();
            config.Add("analytics.batchLimit", 5);
            ACPCore.UpdateConfiguration(config);
            ACPCore.TrackAction("action", null);
            ACPCore.TrackAction("action", null);
            // test
            ACPAnalytics.GetQueueSize(new QueueSizeCallback());
            latch.Wait();
            latch.Dispose();
            // verify
            Assert.That(retrievedQueueSize, Is.EqualTo(expectedSize));
        }

        [Test]
        public void ClearQueue_Clears_QueuedHits()
        {
            // setup
            int expectedSize = 3;
            CountdownEvent latch = new CountdownEvent(1);
            Dictionary<string, Java.Lang.Object> config = new Dictionary<string, Java.Lang.Object>();
            config.Add("analytics.batchLimit", 5);
            ACPCore.UpdateConfiguration(config);
            ACPCore.TrackAction("action", null);
            ACPCore.TrackAction("action", null);
            ACPCore.TrackAction("action", null);
            // test
            ACPAnalytics.GetQueueSize(new QueueSizeCallback());
            latch.Wait();
            latch.Dispose();
            // verify
            Assert.That(retrievedQueueSize, Is.EqualTo(expectedSize));
            // test
            expectedSize = 0;
            ACPAnalytics.ClearQueue();
            latch = new CountdownEvent(1);
            ACPAnalytics.GetQueueSize(new QueueSizeCallback());
            latch.Wait();
            latch.Dispose();
            // verify
            Assert.That(retrievedQueueSize, Is.EqualTo(expectedSize));
        }

        [Test]
        public void GetCustomVisitorIdentifier_Gets_PreviouslySetCustomVisitorIdentifier()
        {
            // setup
            var latch = new CountdownEvent(1);
            var retrievedIdentifier = "";
            var expectedIdentifier = "someVisitorIdentifier";
            ACPAnalytics.SetVisitorIdentifier(expectedIdentifier);
            // test
            ACPAnalytics.GetVisitorIdentifier(new VisitorIdentifierCallback());
            latch.Wait();
            latch.Dispose();
            // verify
            Assert.That(retrievedIdentifier, Is.EqualTo(expectedIdentifier));
        }

        // callbacks
        class QueueSizeCallback : Java.Lang.Object, IAdobeCallback
        {
            public void Call(Java.Lang.Object queueSize)
            {
                if (queueSize != null)
                {
                    retrievedQueueSize = (int)queueSize;
                }
                else
                {
                    Console.WriteLine("null content in queue size callback");
                }
                if(latch != null)
                {
                    latch.Signal();
                }
            }
        }

        class VisitorIdentifierCallback : Java.Lang.Object, IAdobeCallback
        {
            public void Call(Java.Lang.Object visitorIdentifier)
            {
                if (visitorIdentifier != null)
                {
                    retrievedVisitorIdentifier = (string)visitorIdentifier;
                }
                else
                {
                    Console.WriteLine("null content in visitor identifier callback");
                }
                if (latch != null)
                {
                    latch.Signal();
                }
            }
        }
    }
}
