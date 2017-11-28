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
