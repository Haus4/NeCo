using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Neco.Client
{
    public class ChatMessage
    {
        public string user { get; set; }
        public string message { get; set; }
        public string time { get; set; }
    }

    public partial class Chat : ContentPage
    {
        private ObservableCollection<ChatMessage> messages;
        private bool preserveFocus = false;

        public Chat()
        {
            InitializeComponent();

            messages = new ObservableCollection<ChatMessage>();
            messageList.ItemsSource = messages;

            Appearing += delegate
            {
                textArea.Focus();
            };

            // This is a quick workaround for a Xamarin bug
            // https://forums.xamarin.com/discussion/56523/entry-cell-loses-focus-on-button-press-in-android-but-not-ios-work-around
            textArea.Unfocused += (object sender, FocusEventArgs e) =>
            {
                if(preserveFocus)
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
        }

        private void SubmitMessage()
        {
            if (textArea == null || textArea.Text == null) return;
            String msg = textArea.Text.Trim();
            if (msg.Length <= 0) return;

            messages.Add(new ChatMessage
            {
                user = "You",
                message = msg,
                time = DateTime.Now.ToShortTimeString()
            });

            textArea.Text = String.Empty;
            messageList.ScrollTo(messages.LastOrDefault(), ScrollToPosition.Start, false);
        }
    }
}
