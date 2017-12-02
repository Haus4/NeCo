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

            byte[] publicKey = new byte[1];
            byte[] signature = new byte[1];

            SessionRequest request = new SessionRequest {
                PublicKey = publicKey,
                Signature = signature,
                Latitude = App.Instance.Locator.Position?.Latitude ?? 0.0,
                Longitude = App.Instance.Locator.Position?.Longitude ?? 0.0
            };

            App.Instance.Connector.Send(Infrastructure.Protocol.CommandTypes.Request, RequestSerializer.Serialize<RequestBase>(request));
        }

        public bool Available
        {
            get
            {
#if DEBUG
                return true;
#else
                return App.Instance.Connector.Connected;
#endif
            }
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
