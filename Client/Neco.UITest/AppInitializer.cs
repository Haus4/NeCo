using System;
using System.IO;
using System.Linq;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace Neco.UITest
{
    public class AppInitializer
    {
        public static IApp StartApp(Platform platform)
        {
            if (platform == Platform.Android)
            {
                return ConfigureApp
                    .Android
#if DEBUG
                    .ApkFile("../../../Neco.Client.Android/bin/Debug/de.dhbw.neco.client-Signed.apk")
#else
                    .ApkFile("../../../Neco.Client.Android/bin/Release/de.dhbw.neco.client-Signed.apk")
#endif
                    .EnableLocalScreenshots()
                    .PreferIdeSettings()
                    .StartApp();
            }

            return ConfigureApp
                .iOS
                .StartApp();
        }
    }
}
