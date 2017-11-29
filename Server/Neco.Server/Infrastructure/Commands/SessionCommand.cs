using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Protocol;
using Neco.Infrastructure.Protocol;

namespace Neco.Server.Infrastructure.Commands
{
    public class SessionCommand : NecoCommandBase
    {
        private bool hasSession = false;
        public override void ExecuteExternalCommand(ClientSession session, byte[] data)
        {
            if (hasSession == false)
            {
                ChatSessionManager.CreateSession(new ChatSession(session,1));
                hasSession = true;
            }
        }

        protected override CommandTypes CommandType
        {
            get { return CommandTypes.Session; }
        }
    }
}
