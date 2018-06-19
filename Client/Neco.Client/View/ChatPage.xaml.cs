using Neco.DataTransferObjects;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
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

        public ChatPage(ViewModel.ChatViewModel viewModel)
        {
            InitializeComponent();
            SetupComponents(viewModel);

            App.Instance.Connector.StateChanged += OnBackendStateChanged;
        }

        public override void OnPopped()
        {
            model.CloseSession();
            App.Instance.Connector.StateChanged -= OnBackendStateChanged;
        }

        public void Close()
        {
            Device.BeginInvokeOnMainThread(async () => await App.Instance.MainPage.Navigation.PopModalAsync(true));
        }

        private void OnBackendStateChanged(object sender, EventArgs e)
        {
            if (sender is Network.BackendConnector connector && connector.CurrentState == Core.State.Error)
            {
                Close();
            }
        }

        private void SetupComponents(ViewModel.ChatViewModel viewModel)
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

            shareButton.Clicked += async (object sender, EventArgs args) =>
            {
                try
                {
                    FileData fileData = await CrossFilePicker.Current.PickFile();
                    if (fileData == null)
                        return; // user canceled file picking

                    if (fileData.FileName.EndsWith("jpg", StringComparison.Ordinal)
                || fileData.FileName.EndsWith("png", StringComparison.Ordinal))
                    {
                        model.PushImage(fileData.DataArray);
                    }

                }
                catch (Exception ex)
                {
                    System.Console.WriteLine("Exception choosing file: " + ex.ToString());
                }
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
                        messageList.ScrollTo(viewModel.Messages.LastOrDefault(), ScrollToPosition.End, true);
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
