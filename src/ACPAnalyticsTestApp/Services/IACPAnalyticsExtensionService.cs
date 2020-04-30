using System;
using System.Threading.Tasks;

namespace ACPAnalyticsTestApp.Services
{
    public interface IACPAnalyticsExtensionService
    {
        // ACPCore API
        TaskCompletionSource<string> GetExtensionVersionCore();
        TaskCompletionSource<string> GetPrivacyStatus();
        TaskCompletionSource<string> SetAdvertisingIdentifier();
        TaskCompletionSource<string> SetLogLevel();
        TaskCompletionSource<string> SetPrivacyStatus();
        TaskCompletionSource<string> TrackAction();
        TaskCompletionSource<string> TrackState();
        TaskCompletionSource<string> UpdateConfig();
        // ACPAnalytics API
        TaskCompletionSource<string> GetExtensionVersionAnalytics();
        TaskCompletionSource<string> ClearQueue();
        TaskCompletionSource<string> GetTrackingIdentifier();
        TaskCompletionSource<string> GetVisitorIdentifier();
        TaskCompletionSource<string> GetQueueSize();
        TaskCompletionSource<string> SetVisitorIdentifier();
        TaskCompletionSource<string> SendQueuedHits();
    }
}
