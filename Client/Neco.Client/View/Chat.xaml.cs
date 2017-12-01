using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Neco.Client
{
    public partial class Chat : NotifiableContentPage
    {
        private Model.ChatModel model;

        public Chat(ViewModel.ChatSession model)
        {
            InitializeComponent();
            SetupComponents(model);
        }

        public override void OnPopped()
        {
            
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
                if (e.NewItems.Count > 0)
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
