using Neco.DataTransferObjects;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

#if DEBUG
[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
#endif
namespace Neco.Client
{
    public partial class ChatPage : NotifiableContentPage
    {
        private Model.ChatModel model;

        public ChatPage(ViewModel.ChatSession viewModel)
        {
            InitializeComponent();
            SetupComponents(viewModel);

            App.Instance.Connector.StateChanged += OnBackendStateChanged;
        }

        public override void OnPopped()
        {
            App.Instance.Connector.StateChanged -= OnBackendStateChanged;
            model.CloseSession();
        }

        public async void Close()
        {
            await App.Instance.MainPage.Navigation.PopAsync(true);
        }

        private void OnBackendStateChanged(object sender, EventArgs e)
        {
            if(sender is Network.BackendConnector connector && connector.CurrentState == Core.State.Error)
            {
                Device.BeginInvokeOnMainThread(() => Close());
            }
        }

        private void SetupComponents(ViewModel.ChatSession viewModel)
        {
            model = viewModel.Model;
            messageList.ItemsSource = viewModel.Messages;

            Appearing += delegate
            {
                textArea.Focus();
            };

            messageList.ItemTapped += (object sender, ItemTappedEventArgs e) =>
            {
                if (e.Item == null) return;
                messageList.SelectedItem = null;
            };

            textArea.Completed += delegate
            {
                SubmitMessage();
            };

            sendButton.Clicked += delegate
            {
                SubmitMessage();
            };

            viewModel.Messages.CollectionChanged += (object sender, NotifyCollectionChangedEventArgs e) =>
            {
                for (int i = e.NewStartingIndex; i < e.NewItems.Count; ++i)
                {
                    (e.NewItems[i] as ViewModel.ChatMessage).PropertyChanged += (object s, PropertyChangedEventArgs ev) =>
                    {
                        if (ev.PropertyName == "IsForeign") // Trigger a template update
                        {
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                // Trigger an update
                                messageList.ItemsSource = null;
                                messageList.ItemsSource = viewModel.Messages;
                            });
                        }
                    };
                }

                if (e.NewItems != null && (e.OldItems == null || e.NewItems.Count > e.OldItems.Count))
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        //ForceLayout();
                        messageList.ScrollTo(viewModel.Messages.LastOrDefault(), ScrollToPosition.Start, true);
                    });
                }
            };
        }

        private void SubmitMessage()
        {
            if (textArea == null || textArea.Text == null) return;

            String msg = textArea.Text.Trim();
            if (msg.Length <= 0) return;

            model.PushMessage(msg);
            textArea.Text = String.Empty;
        }
    }
}
