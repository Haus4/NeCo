using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Neco.Client.Core;

namespace Neco.Client
{
    public partial class MainPage : ContentPage
    {
        private ViewModel.ChatSession session;

        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Task.Delay(200);
                await logo.ScaleTo(0.6, 1000, Easing.BounceOut);

                App.Instance.Locator.StateChanged += ((sender, e) => UpdateButtonState());
                App.Instance.Connector.StateChanged += ((sender, e) => UpdateButtonState());
            });
        }

        private void UpdateButtonState()
        {
            if (App.Instance.Locator.CurrentState != State.Unknown &&
                App.Instance.Connector.CurrentState != State.Unknown)
            {
                if (App.Instance.Locator.CurrentState == State.Error ||
                    App.Instance.Connector.CurrentState == State.Error)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        IMessage messageHandler = DependencyService.Get<IMessage>();

                        if (App.Instance.Locator.CurrentState == State.Error)
                        {
                            messageHandler.ShowToast("Unable to connect to get location");
                        }

                        if (App.Instance.Connector.CurrentState == State.Error)
                        {
                            messageHandler.ShowToast("Unable to connect to server");
                        }

                        chatButton.Clicked -= StartSessionHandler;
                        chatButton.Clicked -= ReconnectHandler;
                        chatButton.Clicked += ReconnectHandler;
                        chatButton.Text = "Retry";
                        chatButton.IsEnabled = true;
                        spinner.IsRunning = false;
                    });
                }
                else
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        chatButton.Clicked -= ReconnectHandler;
                        chatButton.Clicked -= StartSessionHandler;
                        chatButton.Clicked += StartSessionHandler;
                        chatButton.Text = "Start chatting";
                        chatButton.IsEnabled = true;
                        spinner.IsRunning = false;
                    });
                }
            }
        }

        private void ReconnectHandler(object sender, EventArgs e)
        {
            chatButton.Clicked -= ReconnectHandler;
            if (App.Instance.Locator.CurrentState == State.Error)
            {
                App.Instance.Locator.Listen();
            }

            if (App.Instance.Connector.CurrentState == State.Error)
            {
                App.Instance.Connector.Connect();
            }

            chatButton.Text = "Connecting";
            chatButton.IsEnabled = false;
            spinner.IsRunning = true;
        }

        private void StartSessionHandler(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                chatButton.Text = "Searching";
                chatButton.IsEnabled = false;
                spinner.IsRunning = true;
            });

            Task.Run(async () =>
            {
                session = new ViewModel.ChatSession();
                bool success = await session.Model.Join();

                Device.BeginInvokeOnMainThread(async () =>
                {
                    if (success)
                    {
                        await Navigation.PushAsync(session.View, true);
                    }
                    else
                    {
                        IMessage messageHandler = DependencyService.Get<IMessage>();
                        messageHandler?.ShowToast("Unable to join a chat lobby");
                        session.Model.CloseSession();
                    }

                    chatButton.Text = "Start chatting";
                    chatButton.IsEnabled = true;
                    spinner.IsRunning = false;
                });

            });
        }
    }
}
