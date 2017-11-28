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
using Android.Preferences;

[assembly: Xamarin.Forms.Dependency(typeof(Neco.Client.Droid.AuthStoreAndroid))]
namespace Neco.Client.Droid
{
    public class AuthStoreAndroid : IAuthStore
    {
        public string GetKey(object context)
        {
            var prefs = PreferenceManager.GetDefaultSharedPreferences(context as MainActivity);

            string key = prefs.GetString("key", "");

            if (key.Length == 0)
            {
                key = "blub"; // Generate a key

                var prefEditor = prefs.Edit();
                prefEditor.PutString("key", key);
                prefEditor.Commit();
            }

            return key;
        }
    }
}
