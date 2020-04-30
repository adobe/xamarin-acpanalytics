using System;
using System.IO;
using System.ComponentModel;
using Xamarin.Forms;
using ACPAnalyticsTestApp.Services;

namespace ACPAnalyticsTestApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        // ACPCore API
        void OnCoreExtensionVersionButtonClicked(object sender, EventArgs args)
        {
            string result = DependencyService.Get<IACPAnalyticsExtensionService>().GetExtensionVersionCore().Task.Result;
            handleStringResult("GetExtensionVersionCore", result);
        }

        void OnGetPrivacyStatusButtonClicked(object sender, EventArgs args)
        {
            string result = DependencyService.Get<IACPAnalyticsExtensionService>().GetPrivacyStatus().Task.Result;
            handleStringResult("GetPrivacyStatus", result);
        }      

        void OnSetAdvertisingIdentifierButtonClicked(object sender, EventArgs args)
        {
            string result = DependencyService.Get<IACPAnalyticsExtensionService>().SetAdvertisingIdentifier().Task.Result;
            handleStringResult("SetAdvertisingIdentifier", result);
        }

        void OnSetLogLevelButtonClicked(object sender, EventArgs args)
        {
            string result = DependencyService.Get<IACPAnalyticsExtensionService>().SetLogLevel().Task.Result;
            handleStringResult("SetLogLevel", result);
        }

        void OnSetPrivacyStatusButtonClicked(object sender, EventArgs args)
        {
            string result = DependencyService.Get<IACPAnalyticsExtensionService>().SetPrivacyStatus().Task.Result;
            handleStringResult("SetPrivacyStatus", result);
        }

        void OnTrackActionButtonClicked(object sender, EventArgs args)
        {
            string result = DependencyService.Get<IACPAnalyticsExtensionService>().TrackAction().Task.Result;
            handleStringResult("TrackAction", result);
        }

        void OnTrackStateButtonClicked(object sender, EventArgs args)
        {
            string result = DependencyService.Get<IACPAnalyticsExtensionService>().TrackState().Task.Result;
            handleStringResult("TrackState", result);
        }

        void OnUpdateConfigurationButtonClicked(object sender, EventArgs args)
        {
            string result = DependencyService.Get<IACPAnalyticsExtensionService>().UpdateConfig().Task.Result;
            handleStringResult("UpdateConfig", result);
        }

        // ACPAnalytics API
        void OnAnalyticsExtensionVersionButtonClicked(object sender, EventArgs args)
        {
            string result = DependencyService.Get<IACPAnalyticsExtensionService>().GetExtensionVersionAnalytics().Task.Result;
            handleStringResult("GetExtensionVersionAnalytics", result);
        }

        void OnClearQueueButtonClicked(object sender, EventArgs args)
        {
            string result = DependencyService.Get<IACPAnalyticsExtensionService>().ClearQueue().Task.Result;
            handleStringResult("ClearQueue", result);
        }

        void OnGetTrackingIdentifierButtonClicked(object sender, EventArgs args)
        {
            string result = DependencyService.Get<IACPAnalyticsExtensionService>().GetTrackingIdentifier().Task.Result;
            handleStringResult("GetTrackingIdentifier", result);
        }

        void OnGetVisitorIdentifierButtonClicked(object sender, EventArgs args)
        {
            string result = DependencyService.Get<IACPAnalyticsExtensionService>().GetVisitorIdentifier().Task.Result;
            handleStringResult("GetVisitorIdentifier", result);
        }

        void OnGetQueueSizeButtonClicked(object sender, EventArgs args)
        {
            string result = DependencyService.Get<IACPAnalyticsExtensionService>().GetQueueSize().Task.Result;
            handleStringResult("GetQueueSize", result);
        }

        void OnSetVisitorIdentifierButtonClicked(object sender, EventArgs args)
        {
            string result = DependencyService.Get<IACPAnalyticsExtensionService>().SetVisitorIdentifier().Task.Result;
            handleStringResult("SetVisitorIdentifier", result);
        }

        void OnSendQueuedHitsButtonClicked(object sender, EventArgs args)
        {
            string result = DependencyService.Get<IACPAnalyticsExtensionService>().SendQueuedHits().Task.Result;
            handleStringResult("SendQueuedHits", result);
        }

        // helper methods
        private void handleStringResult(string apiName, string result)
        {
            if (result != null)
            {
                Console.WriteLine(apiName + ": " + result);
            }
        }
    }
}
