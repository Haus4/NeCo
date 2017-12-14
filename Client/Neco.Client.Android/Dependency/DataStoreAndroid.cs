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

[assembly: Xamarin.Forms.Dependency(typeof(Neco.Client.Droid.DataStoreAndroid))]
namespace Neco.Client.Droid
{
    public class DataStoreAndroid : IDataStore
    {
        private ISharedPreferences GetSharedPreferences(object context)
        {
            return PreferenceManager.GetDefaultSharedPreferences(context as MainActivity);
        }

        public void SetString(object context, string key, string value)
        {
            var prefEditor = GetSharedPreferences(context).Edit();
            prefEditor.PutString(key, value);
            prefEditor.Commit();
        }

        public string GetString(object context, string key)
        {
           return GetSharedPreferences(context).GetString(key, null);
        }
    }
}
