using Xamarin.Forms;

namespace Neco.Client
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            chatButton.Clicked += async (sender, args) =>
            {
                ChatSession session = new ChatSession();
                await Navigation.PushAsync(session.View);
            };
        }
    }
}
