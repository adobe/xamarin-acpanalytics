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
        static int retrivedQueueSize;
        static string retrievedVisitorIdentifier;
        static CountdownEvent latch;

        [SetUp]
        public void Setup()
        {
            retrivedQueueSize = 0;
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
            Assert.True(ACPAnalytics.ExtensionVersion() == "1.2.4");
        }

        [Test]
        public void GetQueueSize_Returns_CorrectQueueSize()
        {
            // setup
            CountdownEvent latch = new CountdownEvent(1);
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
            Assert.True(retrivedQueueSize == 2);
        }

        [Test]
        public void ClearQueue_Clears_QueuedHits()
        {
            // setup
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
            Assert.True(retrivedQueueSize == 3);
            // test
            ACPAnalytics.ClearQueue();
            latch = new CountdownEvent(1);
            ACPAnalytics.GetQueueSize(new QueueSizeCallback());
            latch.Wait();
            latch.Dispose();
            // verify
            Assert.True(retrivedQueueSize == 0);
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
            Assert.True(retrievedIdentifier == expectedIdentifier);
        }

        // callbacks
        class QueueSizeCallback : Java.Lang.Object, IAdobeCallback
        {
            public void Call(Java.Lang.Object queueSize)
            {
                if (queueSize != null)
                {
                    retrivedQueueSize = (int)queueSize;
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
