using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        public void SendToSpecificMember(IPEndPoint endPoint, byte[] data, int offset, int length)
        {
            if(endPoint == sessionCreator.LocalEndPoint)
            {
                sessionCreator.Send(data, offset, length);
            } else
            {
                sessionMember.Send(data, offset, length);
            }
        }

        public void SendEachMember(byte[] data, int offset, int length)
        {
            sessionCreator.Send(data, offset, length);
            sessionMember.Send(data, offset, length);
        }

        public void CloseSession()
        {
            IsOpen = false;
        }

    }
}
