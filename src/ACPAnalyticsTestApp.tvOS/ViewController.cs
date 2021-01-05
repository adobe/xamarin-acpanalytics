using System;
using Foundation;
using UIKit;
using Com.Adobe.Marketing.Mobile;

namespace ACPAnalyticsTestApp.tvOS
{
    public partial class ViewController : UIViewController
    {
        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();            
            // Perform any additional setup after loading the view, typically from a nib.
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
        
        //ACPCore
        partial void GetExtensionVersionCore(UIButton sender)
        {
            string version = ACPCore.ExtensionVersion();
            Console.WriteLine("GetExtensionVersionCore: " + version);
        }

        partial void SetLogLevel(UIButton sender)
        {
            ACPCore.LogLevel = ACPMobileLogLevel.Verbose;
            Console.WriteLine("SetLogLevel: Completed");
        }

        partial void SetAdvertisingIdentifier(UIButton sender)
        {
            ACPCore.SetAdvertisingIdentifier("testAdvertisingIdentifier");            
            Console.WriteLine("SetAdvertisingIdentifier: Completed");
        }

        partial void GetPrivacyStatus(UIButton sender)
        {
            var callback = new Action<ACPMobilePrivacyStatus>(handleCallback);
            ACPCore.GetPrivacyStatus(callback);
            Console.WriteLine("GetPrivacyStatus: Completed");
        }

        partial void SetPrivacyStatus(UIButton sender)
        {
            ACPCore.SetPrivacyStatus(ACPMobilePrivacyStatus.OptIn);
            Console.WriteLine("SetPrivacyStatus: Completed");
        }

        partial void TrackAction(UIButton sender)
        {

            var data = new NSMutableDictionary<NSString, NSString>
            {
                ["key"] = new NSString("value")
            };
            ACPCore.TrackAction("action", data);
            Console.WriteLine("TrackAction: Completed");
        }

        partial void TrackState(UIButton sender)
        {
            var data = new NSMutableDictionary<NSString, NSString>
            {
                ["key"] = new NSString("value")
            };
            ACPCore.TrackState("state", data);
            Console.WriteLine("TrackState: Completed");
        }

        partial void UpdateConfig(UIButton sender)
        {
            var config = new NSMutableDictionary<NSString, NSObject>
            {
                ["someConfigKey"] = new NSString("configValue"),
                ["analytics.batchLimit"] = new NSNumber(5)
            };
            ACPCore.UpdateConfiguration(config);
            Console.WriteLine("UpdateConfig: Completed");
        }

        //ACPAnalytics

        partial void GetQueueSize(UIButton sender)
        {
            var callback = new Action<nuint>(handleCallback);
            ACPAnalytics.GetQueueSize(callback);
            Console.WriteLine("GetQueueSize: Completed");
        }

        partial void SetVisitorIdentifier(UIButton sender)
        {
            ACPAnalytics.SetVisitorIdentifier("testVisitorIdentifier");
            Console.WriteLine("SetVisitorIdentifier: Completed");
        }

        partial void GetVisitorIdentifier(UIButton sender)
        {
            var callback = new Action<NSString>(handleCallback);
            ACPAnalytics.GetVisitorIdentifier(callback);
            Console.WriteLine("GetVisitorIdentifier: Completed");
        }

        partial void GetTrackingIdentifier(UIButton sender)
        {
            var callback = new Action<NSString>(handleCallback);
            ACPAnalytics.GetTrackingIdentifier(callback);
            Console.WriteLine("GetTrackingIdentifier: Completed");
        }

        partial void ClearQueue(UIButton sender)
        {
            ACPAnalytics.ClearQueue();
            Console.WriteLine("ClearQueue: Completed");
        }

        partial void GetExtensionVersionAnalytics(UIButton sender)
        {
            string version = ACPAnalytics.ExtensionVersion();
            Console.WriteLine("GetExtensionVersionAnalytics: " + version);
        }

        partial void SendQueuedHits(UIButton sender)
        {
            ACPAnalytics.SendQueuedHits();
            Console.WriteLine("SendQueuedHits: Completed");
        }

        // callbacks
        private void handleCallback(ACPMobilePrivacyStatus privacyStatus)
        {            
            Console.WriteLine("Privacy status: " + privacyStatus.ToString());
        }

        private void handleCallback(NSString content)
        {
            if (content == null)
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

