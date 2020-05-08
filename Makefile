# Makefile requires Visual Studio for Mac Community version to be installed
# Tested with 8.5.3 (build 16)
setup:
	cd src/Adobe.ACPAnalytics.Android/ && msbuild -t:restore
	cd src/Adobe.ACPAnalytics.iOS/ && msbuild -t:restore

msbuild-clean:
	cd src && msbuild -t:clean

clean-folders:
	rm -rf src/Adobe.ACPAnalytics.Android//obj
	rm -rf src/Adobe.ACPAnalytics.Android//bin/Debug
	rm -rf src/Adobe.ACPAnalytics.iOS/bin/Debug
	rm -rf src/Adobe.ACPAnalytics.iOS/obj
	rm -rf bin

clean: msbuild-clean clean-folders setup

# Makes ACPAnalytics bindings and NuGet package. The binding (.dll) will be available in BindingDirectory/bin/Debug
# The NuGet package is created in the same directory but then moved to src/bin.
all:
	cd src/Adobe.ACPAnalytics.Android/ && msbuild -t:pack
	cd src/Adobe.ACPAnalytics.iOS/ && msbuild -t:build	
	mkdir bin
	cp src/Adobe.ACPAnalytics.Android//bin/Debug/*.nupkg ./bin
	cp src/Adobe.ACPAnalytics.iOS/bin/Debug/*.nupkg ./bin