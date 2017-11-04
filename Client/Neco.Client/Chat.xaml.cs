using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Neco.Client
{
    public class ChatMessage
    {
        public virtual string user { get; set; }
        public virtual string message { get; set; }
        public virtual string time { get; set; }
        public virtual string color
        {
            get
            {
                return "#00000000";
            }
        }
    }

    // TODO: Use a DataTemplateSelector for changed colors
    public class ForeignChatMessage : ChatMessage
    {

        public override string color
        {
            get
            {
                return "#40865FC5";
            }
        }
    }

    public partial class Chat : ContentPage
    {
        private ObservableCollection<ChatMessage> messages;
        private bool preserveFocus = false;

        private Thread thread;
        private bool terminate = false;

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

            thread = new Thread(new ThreadStart(() =>
            {
                Thread.Sleep(3000);

                while (!terminate)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        PushMessage(new ForeignChatMessage
                        {
                            user = "Dr. Axel Stoll",
                            message = "Die Sonne ist kalt!",
                            time = DateTime.Now.ToShortTimeString()
                        });
                    });

                    for(int i = 0; i < 50 && !terminate; ++i)
                        Thread.Sleep(100);
                }
            }));
            thread.Start();
        }

        protected override void OnDisappearing()
        {
            terminate = true;
            if (thread != null && thread.IsAlive)
            {
                thread.Join();
            }
        }

        private void SubmitMessage()
        {
            if (textArea == null || textArea.Text == null) return;
            String msg = textArea.Text.Trim();
            if (msg.Length <= 0) return;

            PushMessage(new ChatMessage
            {
                user = "You",
                message = msg,
                time = DateTime.Now.ToShortTimeString()
            });

            textArea.Text = String.Empty;
        }

        private void PushMessage(ChatMessage message)
        {
            messages.Add(message);
            messageList.ScrollTo(messages.LastOrDefault(), ScrollToPosition.Start, false);
        }
    }
}
