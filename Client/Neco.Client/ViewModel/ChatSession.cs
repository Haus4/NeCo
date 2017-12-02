using Google.ProtocolBuffers;
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

            Proto.Session msg = Proto.Session.CreateBuilder()
                .SetPublicKey(ByteString.CopyFrom(publicKey))
                .SetSignature(ByteString.CopyFrom(signature))
                .SetLat(App.Instance.Locator.Position?.Latitude ?? 0.0)
                .SetLon(App.Instance.Locator.Position?.Longitude ?? 0.0)
                .BuildPartial();

            App.Instance.Connector.Send(Infrastructure.Protocol.CommandTypes.Session, msg.ToByteArray());
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
