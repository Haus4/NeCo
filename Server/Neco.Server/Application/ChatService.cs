using Neco.Server.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neco.DataTransferObjects;

namespace Neco.Server.Application
{
    public class ChatService : BaseService
    {
        public void Message(ClientSession session, MessageRequest request)
        {
            if (session.HasChat)
            {
                var chatSession = ChatSessionManager.GetSession(session.ChatSessionId);
                var data = RequestSerializer.Serialize<RequestBase>(request);
                chatSession.SendToSpecificMember(session.RemoteEndPoint, data);
            }
        }
    }
}
