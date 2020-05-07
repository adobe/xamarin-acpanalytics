using System.Reflection;

using Android.App;
using Android.OS;
using Xamarin.Android.NUnitLite;
using Com.Adobe.Marketing.Mobile;
using System.Threading;

namespace ACPAnalyticsAndroidUnitTests
{
    [Activity(Label = "ACPAnalyticsAndroidUnitTests", MainLauncher = true)]
    public class MainActivity : TestSuiteActivity
    {
        static CountdownEvent latch = new CountdownEvent(1);
        protected override void OnCreate(Bundle bundle)
        {
            // tests can be inside the main assembly
            AddTest(Assembly.GetExecutingAssembly());
            // or in any reference assemblies
            // AddTest (typeof (Your.Library.TestClass).Assembly);

            // Once you called base.OnCreate(), you cannot add more assemblies.
            base.OnCreate(bundle);

            // set log level
            ACPCore.LogLevel = LoggingMode.Verbose;

            // register SDK extensions
            ACPCore.Application = this.Application;
            ACPIdentity.RegisterExtension();
            ACPLifecycle.RegisterExtension();
            ACPAnalytics.RegisterExtension();

            // start core
            ACPCore.Start(new CoreStartCompletionCallback());
            latch.Wait();
            latch.Dispose();
        }

        class CoreStartCompletionCallback : Java.Lang.Object, IAdobeCallback
        {
            public void Call(Java.Lang.Object callback)
            {
                // set config from launch (org: OBUMobile5, app: ryan-xamarin)
                ACPCore.ConfigureWithAppID("94f571f308d5/00fc543a60e1/launch-c861fab912f7-development");
                latch.Signal();
            }
        }
    }
}
