using System.Collections.ObjectModel;

namespace Neco.Client
{
    public class ChatSession
    {
        private ObservableCollection<ChatMessage> messageList;

        private Chat chatView;
        private ChatController chatController;

        public ChatSession()
        {
            messageList = new ObservableCollection<ChatMessage>();
            chatController = new ChatController(this);
            chatView = new Chat(this);
        }

        public ObservableCollection<ChatMessage> Messages
        {
            get
            {
                return messageList;
            }
        }

        public Chat View
        {
            get
            {
                return chatView;
            }
        }

        public ChatController Controller
        {
            get
            {
                return chatController;
            }
        }
    }
}
