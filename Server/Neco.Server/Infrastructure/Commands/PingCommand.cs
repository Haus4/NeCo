using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neco.Infrastructure.Protocol;

namespace Neco.Server.Infrastructure.Commands
{
    public class PingCommand : NecoCommandBase
    {
        protected override CommandTypes CommandType
        {
            get { return CommandTypes.Ping; }
        }

        public override void ExecuteExternalCommand(ClientSession session, byte[] data)
        {
            // PONG
        }
    }
}
