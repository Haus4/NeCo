using Neco.DataTransferObjects;
using Neco.Server.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neco.Server.Application
{
    public class SessionService : BaseService
    {
        public void Session(ClientSession session, SessionRequest request)
        {
            if (session.HasChat) session.LeaveChatSession();

            if (ChatSessionManager.IsSessionAvailable())
            {
                var chatSessionId = ChatSessionManager.GetIdForNextOpenSession();
                ChatSessionManager.JoinSession(chatSessionId, session);
            } else
            {
                ChatSessionManager.CreateSession(session);
            }
        }
    }
}
