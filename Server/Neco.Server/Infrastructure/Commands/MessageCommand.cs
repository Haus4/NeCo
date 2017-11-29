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
    public class ChatCommand : NecoCommandBase
    {
        private String user = "NO_NAME_GIVEN";

        public override void ExecuteExternalCommand(ClientSession session, byte[] data)
        {
            ChatSession ses = ChatSessionManager.GetSession();
            if(ses != null)
            {
                ses.SendToSpecificMember(session.LocalEndPoint, data, 0, data.Length);
            }
        }

        protected override CommandTypes CommandType
        {
            get { return CommandTypes.Message; }
        }
    }
}
