using Xamarin.Forms;

namespace Neco.Client
{
    public partial class MainPage : ContentPage
    {
        private ChatSession session;

        public MainPage()
        {
            InitializeComponent();

            chatButton.Clicked += async (sender, args) =>
            {
                session = new ChatSession();
                await Navigation.PushAsync(session.View);
            };
        }
    }
}
