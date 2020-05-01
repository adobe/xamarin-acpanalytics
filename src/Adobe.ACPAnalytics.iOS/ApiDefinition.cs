using System;
using Foundation;
using ObjCRuntime;

namespace Com.Adobe.Marketing.Mobile
{
	// @interface ACPAnalytics : NSObject
	[BaseType(typeof(NSObject))]
	interface ACPAnalytics
	{
		// +(void)clearQueue;
		[Static]
		[Export("clearQueue")]
		void ClearQueue();

		// +(NSString * _Nonnull)extensionVersion;
		[Static]
		[Export("extensionVersion")]
		string ExtensionVersion { get; }

		// +(void)getQueueSize:(void (^ _Nonnull)(NSUInteger))callback;
		[Static]
		[Export("getQueueSize:")]
		void GetQueueSize(Action<nuint> callback);

		// +(void)getTrackingIdentifier:(void (^ _Nonnull)(NSString * _Nullable))callback;
		[Static]
		[Export("getTrackingIdentifier:")]
		void GetTrackingIdentifier(Action<NSString> callback);

		// +(void)registerExtension;
		[Static]
		[Export("registerExtension")]
		void RegisterExtension();

		// +(void)sendQueuedHits;
		[Static]
		[Export("sendQueuedHits")]
		void SendQueuedHits();

		// +(void)getVisitorIdentifier:(void (^ _Nonnull)(NSString * _Nullable))callback;
		[Static]
		[Export("getVisitorIdentifier:")]
		void GetVisitorIdentifier(Action<NSString> callback);

		// +(void)setVisitorIdentifier:(NSString * _Nonnull)visitorIdentifier;
		[Static]
		[Export("setVisitorIdentifier:")]
		void SetVisitorIdentifier(string visitorIdentifier);
	}
}