using Neco.Client.Network;
using Neco.DataTransferObjects;
using System.Collections.ObjectModel;

namespace Neco.Client.ViewModel
{
    public class ChatSession : ViewModelBase
    {
        private ObservableCollection<ChatMessage> messageList;

        private ChatPage chatView;
        private Model.ChatModel chatModel;

        public ChatSession()
        {
            messageList = new ObservableCollection<ChatMessage>();
            chatModel = new Model.ChatModel(this);
            chatView = new ChatPage(this);
        }

        public ObservableCollection<ChatMessage> Messages
        {
            get
            {
                return messageList;
            }
        }

        public ChatPage View
        {
            get
            {
                return chatView;
            }
        }

        public Model.ChatModel Model
        {
            get
            {
                return chatModel;
            }
        }
    }
}
