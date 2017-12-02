using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using System;
using Xamarin.Forms;

namespace Neco.Client
{
    public partial class App : Application
    {
        private Network.BackendConnector backendConnector;

        private MainPage mainPage;
        private NotifyingNavigationPage notifyingNavigationPage;
        private object context;
        private string key;

        private Core.GeoLocator locator;

        public App(object _context)
        {
            InitializeComponent();

            context = _context;
            locator = new Core.GeoLocator();

            LoadKey();

            backendConnector = new Network.BackendConnector(/*"neco.it.dh-karlsruhe.de:9000"*/"192.168.0.214:9000");

            mainPage = new MainPage();
            notifyingNavigationPage = new NotifyingNavigationPage(mainPage);
            MainPage = notifyingNavigationPage;
        }

        public Network.BackendConnector Connector
        {
            get
            {
                return backendConnector;
            }
        }

        public Core.GeoLocator Locator
        {
            get
            {
                return locator;
            }
        }

        public static App Instance
        {
            get
            {
                return Current as App;
            }
        }

        private void LoadKey()
        {
            IDataStore dataStore = DependencyService.Get<IDataStore>();

            key = dataStore.GetString(context, "key");
            if(key == null)
            {
                key = "blub"; // TODO: Generate ECC key using ed25519
                dataStore.SetString(context, "key", key);
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
