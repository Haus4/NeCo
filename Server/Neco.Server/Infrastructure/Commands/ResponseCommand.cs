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
    public class ResponseCommand : NecoCommandBase
    {
        public override void ExecuteExternalCommand(ClientSession session, byte[] data)
        {
            session.AppendResponse(data);
        }

        protected override CommandTypes CommandType
        {
            get { return CommandTypes.Response; }
        }
    }
}
