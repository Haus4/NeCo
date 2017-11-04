using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Threading.Tasks;

namespace Neco.Client.Droid
{
    [Activity(Label = "NeCo", Icon = "@drawable/icon", Theme = "@style/MainTheme.Splash", MainLauncher = true, NoHistory = true)]
    public class StartActivity : Activity
    {
        private bool resumed = false;

        protected override void OnResume()
        {
            base.OnResume();

            if (!resumed)
            {
                resumed = true;
                StartActivity(new Intent(Application.Context, typeof(MainActivity)));
            }
        }
    }
}