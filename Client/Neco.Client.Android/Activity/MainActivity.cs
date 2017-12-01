using System;

using Android.App;
using Android.Content.PM;
using Android.OS;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Android.Preferences;
using Android.Views;
using System.Threading;

namespace Neco.Client.Droid
{
    [Activity(Label = "NeCo", Icon = "@drawable/icon", Theme = "@style/MainTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private App app;
        private bool fakeFocus;
        private object lockObj = new object();

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            app = new App(this);
            LoadApplication(app);

            App.Current.On<Xamarin.Forms.PlatformConfiguration.Android>().UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);
        }

        public override bool DispatchTouchEvent(MotionEvent ev)
        {
            lock (lockObj)
            {
                fakeFocus = true;
                bool result = base.DispatchTouchEvent(ev);
                fakeFocus = false;
                return result;
            }
        }

        public override View CurrentFocus
        {
            get
            {
                lock (lockObj)
                {
                    if (fakeFocus) return null;
                    return base.CurrentFocus;
                }
            }
        }
    }
}
