using libsignal.ecc;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using System;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace Neco.Client
{
    public partial class App : Application
    {
        private Network.BackendConnector backendConnector;

        private MainPage mainPage;
        private NotifyingNavigationPage notifyingNavigationPage;
        private object context;
        private Core.CryptoHandler cryptoHandler;

        private Core.GeoLocator locator;

        public App(object _context)
        {
            InitializeComponent();

            context = _context;

            locator = new Core.GeoLocator();
            cryptoHandler = new Core.CryptoHandler(context);
            backendConnector = new Network.BackendConnector("192.168.2.107:9000");

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

        public Core.CryptoHandler CryptoHandler
        {
            get
            {
                return cryptoHandler;
            }
        }

        public static App Instance
        {
            get
            {
                return Current as App;
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
