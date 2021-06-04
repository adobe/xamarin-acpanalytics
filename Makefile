# Makefile requires Visual Studio for Mac Community version to be installed
# Tested with 8.5.3 (build 16)
setup:
	cd src/Adobe.ACPAnalytics.Android/ && msbuild -t:restore
	cd src/Adobe.ACPAnalytics.iOS/ && msbuild -t:restore
	cd src/Adobe.ACPAnalytics.tvOS/ && msbuild -t:restore

msbuild-clean:
	cd src && msbuild -t:clean

clean-folders:
	rm -rf src/Adobe.ACPAnalytics.Android/obj
	rm -rf src/Adobe.ACPAnalytics.Android/bin/Debug
	rm -rf src/Adobe.ACPAnalytics.iOS/bin/Debug
	rm -rf src/Adobe.ACPAnalytics.iOS/obj
	rm -rf src/Adobe.ACPAnalytics.tvOS/bin/Debug
	rm -rf src/Adobe.ACPAnalytics.tvOS/obj
	rm -rf bin

clean: msbuild-clean clean-folders setup

# Makes ACPAnalytics bindings and NuGet package. The binding (.dll) will be available in BindingDirectory/bin/Debug
# The NuGet package is created in the same directory but then moved to src/bin.
release:
	cd src/Adobe.ACPAnalytics.Android/ && msbuild -t:pack
	cd src/Adobe.ACPAnalytics.iOS/ && msbuild -t:build	
	cd src/Adobe.ACPAnalytics.tvOS/ && msbuild -t:build
	mkdir bin
	cp src/Adobe.ACPAnalytics.Android/bin/Debug/*.nupkg ./bin
	cp src/Adobe.ACPAnalytics.iOS/bin/Debug/*.nupkg ./bin
	cp src/Adobe.ACPAnalytics.tvOS/bin/Debug/*.nupkg ./bin

ACPANALYTICS_SDK_PATH = ./acp-sdk
ACPANALYTICS_SDK_IOS_ANALYTICS_PATH = ./acp-sdk/iOS/ACPAnalytics
ACPANALYTICS_SDK_TVOS_ANALYTICS_PATH = ./acp-sdk/tvOS/ACPAnalytics
UNIVERSAL_ANALYTICS_IOS_PATH = ./acp-sdk/universal-analytics-ios
UNIVERSAL_ANALYTICS_TVOS_PATH = ./acp-sdk/universal-analytics-tvos
UNIVERSAL_ANALYTICS_IOS_ACPANALYTICS_PATH = ./acp-sdk/universal-analytics-ios/ACPAnalytics
UNIVERSAL_ANALYTICS_TVOS_ACPANALYTICS_PATH = ./acp-sdk/universal-analytics-tvos/ACPAnalytics
SIMULATOR_DIRECTORY_NAME = ios-arm64_i386_x86_64-simulator
SIMULATOR_TVOS_DIRECTORY_NAME = tvos-arm64_x86_64-simulator
DEVICE_DIRECTORY_NAME = ios-arm64_armv7_armv7s
DEVICE_TVOS_DIRECTORY_NAME = tvos-arm64

download-acp-sdk:
	mkdir -p $(ACPANALYTICS_SDK_PATH)
	git clone --depth 1 https://github.com/Adobe-Marketing-Cloud/acp-sdks.git $(ACPANALYTICS_SDK_PATH)

update-analytics-ios-static-libraries:
	mkdir -p $(UNIVERSAL_ANALYTICS_IOS_PATH)
	mv $(ACPANALYTICS_SDK_IOS_ANALYTICS_PATH) $(UNIVERSAL_ANALYTICS_IOS_PATH)
	lipo -remove arm64 -output $(UNIVERSAL_ANALYTICS_IOS_ACPANALYTICS_PATH)/ACPAnalytics.xcframework/$(SIMULATOR_DIRECTORY_NAME)/libACPAnalytics_iOS_clean.a $(UNIVERSAL_ANALYTICS_IOS_ACPANALYTICS_PATH)/ACPAnalytics.xcframework/$(SIMULATOR_DIRECTORY_NAME)/libACPAnalytics_iOS.a
	lipo -create $(UNIVERSAL_ANALYTICS_IOS_ACPANALYTICS_PATH)/ACPAnalytics.xcframework/$(DEVICE_DIRECTORY_NAME)/libACPAnalytics_iOS.a $(UNIVERSAL_ANALYTICS_IOS_ACPANALYTICS_PATH)/ACPAnalytics.xcframework/$(SIMULATOR_DIRECTORY_NAME)/libACPAnalytics_iOS_clean.a  -output $(UNIVERSAL_ANALYTICS_IOS_PATH)/libACPAnalytics_iOS.a
	mv $(UNIVERSAL_ANALYTICS_IOS_PATH)/libACPAnalytics_iOS.a ./src/Adobe.ACPAnalytics.iOS

update-analytics-tvos-static-libraries:
	mkdir -p $(UNIVERSAL_ANALYTICS_TVOS_PATH)
	mv $(ACPANALYTICS_SDK_TVOS_ANALYTICS_PATH) $(UNIVERSAL_ANALYTICS_TVOS_PATH)
	lipo -remove arm64 -output $(UNIVERSAL_ANALYTICS_TVOS_ACPANALYTICS_PATH)/ACPAnalyticsTV.xcframework/$(SIMULATOR_TVOS_DIRECTORY_NAME)/libACPAnalytics_tvOS_clean.a $(UNIVERSAL_ANALYTICS_TVOS_ACPANALYTICS_PATH)/ACPAnalyticsTV.xcframework/$(SIMULATOR_TVOS_DIRECTORY_NAME)/libACPAnalytics_tvOS.a
	lipo -create $(UNIVERSAL_ANALYTICS_TVOS_ACPANALYTICS_PATH)/ACPAnalyticsTV.xcframework/$(DEVICE_TVOS_DIRECTORY_NAME)/libACPAnalytics_tvOS.a $(UNIVERSAL_ANALYTICS_TVOS_ACPANALYTICS_PATH)/ACPAnalyticsTV.xcframework/$(SIMULATOR_TVOS_DIRECTORY_NAME)/libACPAnalytics_tvOS_clean.a  -output $(UNIVERSAL_ANALYTICS_TVOS_PATH)/libACPAnalytics_tvOS.a
	mv $(UNIVERSAL_ANALYTICS_TVOS_PATH)/libACPAnalytics_tvOS.a ./src/Adobe.ACPAnalytics.tvOS