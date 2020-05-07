
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
            Assert.True(ACPAnalytics.ExtensionVersion == "2.2.3");
        }

        [Test]
        public void GetQueueSize_Returns_CorrectQueueSize()
        {
            // setup
            CountdownEvent latch = new CountdownEvent(1);
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
            Assert.True(queueSize == 2);
        }

        [Test]
        public void ClearQueue_Clears_QueuedHits()
        {
            // setup
            CountdownEvent latch = new CountdownEvent(1);
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
            Assert.True(queueSize == 3);
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
            Assert.True(queueSize == 0);
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
            ACPAnalytics.GetVisitorIdentifier(callback =>
            {
                retrievedIdentifier = callback;
                latch.Signal();
            });
            latch.Wait();
            latch.Dispose();
            // verify
            Assert.True(retrievedIdentifier == expectedIdentifier);
        }
    }
}