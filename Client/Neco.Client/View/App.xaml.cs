using Xamarin.Forms;

namespace Neco.Client
{
    public partial class App : Application
    {
        private Network.BackendConnector backendConnector;

        public App()
        {
            InitializeComponent();

            backendConnector = new Network.BackendConnector("192.168.0.214:9000");
            backendConnector.Send("TEST user:xnp\r\n");
            MainPage = new NotifyingNavigationPage(new MainPage());
        }

        public Network.BackendConnector Connector
        {
            get
            {
                return backendConnector;
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
