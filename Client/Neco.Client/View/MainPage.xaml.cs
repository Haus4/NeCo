using Android.Preferences;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Neco.Client
{
    public partial class MainPage : ContentPage
    {
        private ViewModel.ChatSession session;

        public MainPage()
        {
            InitializeComponent();

            chatButton.IsEnabled = App.Instance.Position != null && App.Instance.Connector.Connected;
            App.Instance.OnPositionChanged((position) => chatButton.IsEnabled = App.Instance.Connector.Connected);
            App.Instance.Connector.OnConnect(() => chatButton.IsEnabled = App.Instance.Position != null);

            chatButton.Clicked += async (sender, args) =>
            {
                session = new ViewModel.ChatSession();
                if (session.Available)
                {
                    await Navigation.PushAsync(session.View);
                }
            };

            Task.Run(async () =>
            {
                while (true)
                {
                    
                    await Task.Delay(2000);
                }
            });
        }

        protected override void OnAppearing()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Task.Delay(200);
                await logo.ScaleTo(0.6, 1000, Easing.BounceOut);
            });
        }
    }
}
