using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Protocol;

namespace Neco.Server.Infrastructure
{
    public class TEST : CommandBase<NecoSession, StringRequestInfo>
    {
        private String user = "NO_NAME_GIVEN";

        public override void ExecuteCommand(NecoSession session, StringRequestInfo requestInfo)
        {
            if(requestInfo.Body.StartsWith("user:"))
            {
                user = requestInfo.Body.Split(':')[1];
                session.Send(" Hello " + user + Environment.NewLine);
            } else
            {
                session.Send(" Message recieved from " + user + Environment.NewLine);
            }
        }
    }
}
