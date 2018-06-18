using Neco.Client.Network;
using Neco.DataTransferObjects;
using System.Collections.ObjectModel;

namespace Neco.Client.ViewModel
{
    public class ChatViewModel : ViewModelBase
    {
        private ObservableCollection<ChatMessage> messageList;

        private ChatPage chatView;
        private Model.ChatModel chatModel;

        public ChatViewModel(bool showMessage = true, bool noView = false)
        {
            messageList = new ObservableCollection<ChatMessage>();
            chatModel = new Model.ChatModel(this, showMessage);
            chatView = noView ? null : new ChatPage(this);
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
