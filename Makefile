# Makefile requires Visual Studio for Mac Community version to be installed
# Tested with 8.5.3 (build 16)
setup:
	cd src/ACPAnalytics.XamarinAndroidBinding/ && msbuild -t:restore
	cd src/Adobe.ACPAnalytics.iOS/ && msbuild -t:restore

msbuild-clean:
	cd src && msbuild -t:clean

clean-folders:
	rm -rf src/ACPAnalytics.XamarinAndroidBinding/obj
	rm -rf src/ACPAnalytics.XamarinAndroidBinding/bin/Debug
	rm -rf src/Adobe.ACPAnalytics.iOS/bin/Debug
	rm -rf src/Adobe.ACPAnalytics.iOS/obj
	rm -rf bin

clean: msbuild-clean clean-folders setup

# Makes ACPAnalytics bindings and NuGet package. The binding (.dll) will be available in BindingDirectory/bin/Debug
# The NuGet package is created in the same directory but then moved to src/bin.
all:
	cd src/ACPAnalytics.XamarinAndroidBinding/ && msbuild -t:pack
	cd src/Adobe.ACPAnalytics.iOS/ && msbuild -t:build	
	mkdir bin
	cp src/ACPAnalytics.XamarinAndroidBinding/bin/Debug/*.nupkg ./bin
	cp src/Adobe.ACPAnalytics.iOS/bin/Debug/*.nupkg ./bin