using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neco.Server.Infrastructure
{
    public class ChatSession
    {

        private ClientSession sessionCreator;
        private ClientSession sessionMember = null;

        public int SessionId { get; private set; }
        public bool IsOpen { get; private set; }

        public ChatSession(ClientSession _sessionCreator, int _sessionId)
        {
            sessionCreator = _sessionCreator;
            SessionId = _sessionId;
            IsOpen = true;
        }

        public void JoinSession(ClientSession _sessionMember)
        {
            sessionMember = _sessionMember;
        }

        public void CloseSession()
        {
            IsOpen = false;
        }

    }
}
