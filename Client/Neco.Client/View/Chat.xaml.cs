using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using Xamarin.Forms;

namespace Neco.Client
{
    public partial class Chat : ContentPage
    {
        private ChatController controller;
        private bool preserveFocus = false;

        public Chat(ChatSession model)
        {
            InitializeComponent();
            SetupComponents(model);
            DoXamarinWorkaround();
        }

        protected override void OnDisappearing()
        {
            controller.Close();
        }

        private void SetupComponents(ChatSession model)
        {
            controller = model.Controller;
            messageList.ItemsSource = model.Messages;

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

            model.Messages.CollectionChanged += (object sender, NotifyCollectionChangedEventArgs e) =>
            {
                if (e.NewItems.Count > 0)
                {
                    messageList.ScrollTo((sender as ObservableCollection<ChatMessage>).LastOrDefault(), ScrollToPosition.Start, false);
                }
            };
        }

        private void DoXamarinWorkaround()
        {
            // This is a quick workaround for a Xamarin bug
            // https://forums.xamarin.com/discussion/56523/entry-cell-loses-focus-on-button-press-in-android-but-not-ios-work-around
            // TODO: Check if this can be fixed by providing a custom Entry renderer
            textArea.Unfocused += (object sender, FocusEventArgs e) =>
            {
                if (preserveFocus)
                {
                    textArea.Focus();
                }

                preserveFocus = false;
            };

            textArea.Focused += delegate
            {
                preserveFocus = false;
            };

            sendButton.Pressed += (object sender, EventArgs e) =>
            {
                preserveFocus = textArea.IsFocused;
            };
        }

        private void SubmitMessage()
        {
            if (textArea == null || textArea.Text == null) return;

            String msg = textArea.Text.Trim();
            if (msg.Length <= 0) return;

            controller.PushMessage(msg);
            textArea.Text = String.Empty;
        }
    }
}
