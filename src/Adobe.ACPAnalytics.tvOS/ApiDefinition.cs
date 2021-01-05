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
using Foundation;

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
		string ExtensionVersion();

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