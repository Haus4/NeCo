using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

#if DEBUG
//[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
#endif
namespace Neco.Client
{
    public partial class LobbyPage : NotifiableContentPage
    {
        private Model.LobbyModel model;
        private ViewModel.ChatViewModel chatViewModel;
        public LobbyPage (ViewModel.LobbyViewModel viewModel)
		{
			InitializeComponent ();
            SetupComponents(viewModel);

            App.Instance.Connector.StateChanged += OnBackendStateChanged;
        }

        public override void OnPopped()
        {
            App.Instance.Connector.StateChanged -= OnBackendStateChanged;
            model.LeaveLobby();
        }

        public void Close()
        {
            Device.BeginInvokeOnMainThread(async () => await App.Instance.MainPage.Navigation.PopAsync(true));
        }

        private void OnBackendStateChanged(object sender, EventArgs e)
        {
            if (sender is Network.BackendConnector connector && connector.CurrentState == Core.State.Error)
            {
                Close();
            }
        }

        private void SetupComponents(ViewModel.LobbyViewModel viewModel)
        {
            model = viewModel.Model;
            memberList.ItemsSource = viewModel.Members;

            memberList.ItemTapped += (object sender, ItemTappedEventArgs e) =>
            {
                if (e.Item == null) return;
                var buttonText = (e.Item as Button).Text;
                if (buttonText != null && Int32.TryParse(buttonText, out int x)) StartSession(x);
                memberList.SelectedItem = null;
            };

            viewModel.Members.CollectionChanged += (object sender, NotifyCollectionChangedEventArgs e) =>
            {
                for (int i = e.NewStartingIndex; i < e.NewItems.Count; ++i)
                {
                    (e.NewItems[i] as ViewModel.ChatSession).PropertyChanged += (object s, PropertyChangedEventArgs ev) =>
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            // Trigger an update
                            memberList.ItemsSource = null;
                            memberList.ItemsSource = viewModel.Members;
                        });
                    };
                }
            };
        }

        private void StartSession(int sessionNum)
        {
            var memberKey = model.GetMemberKey(sessionNum);
            Task.Run(async () =>
            {
                chatViewModel = new ViewModel.ChatViewModel();
                bool success = await chatViewModel.Model.Join(memberKey);

                Device.BeginInvokeOnMainThread(async () =>
                {
                    if (success)
                    {
                        await Navigation.PushAsync(chatViewModel.View, true);
                    }
                    else
                    {
                        IMessage messageHandler = DependencyService.Get<IMessage>();
                        messageHandler?.ShowToast("Unable open chat session");
                        chatViewModel.Model.CloseSession();
                    }
                });

            });
        }
    }
}
