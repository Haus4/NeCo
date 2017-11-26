using System.Collections.ObjectModel;

namespace Neco.Client.ViewModel
{
    public class ChatSession : ViewModelBase
    {
        private ObservableCollection<ChatMessage> messageList;

        private Chat chatView;
        private Model.ChatModel chatController;

        public ChatSession()
        {
            messageList = new ObservableCollection<ChatMessage>();
            chatController = new Model.ChatModel(this);
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

        public Model.ChatModel Controller
        {
            get
            {
                return chatController;
            }
        }
    }
}
