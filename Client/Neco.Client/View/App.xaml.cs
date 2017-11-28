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

        public App(object _context)
        {
            InitializeComponent();

            context = _context;
            key = DependencyService.Get<IAuthStore>().GetKey(context);

            backendConnector = new Network.BackendConnector("192.168.0.214:9000");

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
