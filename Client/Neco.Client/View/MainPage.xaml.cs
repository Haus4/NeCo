using Android.Preferences;
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
        }
    }
}
