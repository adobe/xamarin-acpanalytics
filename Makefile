# Makefile requires Visual Studio for Mac Community version to be installed
# Tested with 8.5.3 (build 16)
setup:
	cd src/ACPAnalytics.XamarinAndroidBinding/ && msbuild -t:restore

msbuild-clean:
	cd src && msbuild -t:clean

clean-folders:
	cd src/ACPAnalytics.XamarinAndroidBinding/ && rm -rf obj
	cd src/ACPAnalytics.XamarinAndroidBinding/bin && rm -rf Debug
	rm -rf bin

clean: msbuild-clean clean-folders setup

# Makes ACPAnalytics android bindings and NuGet package. The android binding (.dll) will be available in src/ACPAnalytics.XamarinAndroidBinding/bin/Debug
# The NuGet package is created in the same directory but then moved to src/bin.
all:
	cd src && msbuild -t:pack
	mkdir bin
	cp src/ACPAnalytics.XamarinAndroidBinding/bin/Debug/*.nupkg ./bin