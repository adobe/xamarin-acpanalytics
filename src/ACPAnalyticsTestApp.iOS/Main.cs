/*
Copyright 2020 Adobe
All Rights Reserved.

NOTICE: Adobe permits you to use, modify, and distribute this file in
accordance with the terms of the Adobe license agreement accompanying
it. If you have received this file from a source other than Adobe,
then your use, modification, or distribution of it requires the prior
written permission of Adobe. (See LICENSE-MIT for details)
*/

using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace ACPAnalyticsTestApp.iOS
{
    public class Application
    {
        // This is the main entry point of the application.
        static void Main(string[] args) =>
            UIApplication.Main(args, null, typeof(AppDelegate));
    }
}
