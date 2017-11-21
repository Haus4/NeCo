using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neco.Infrastructure.Protocol;
using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Protocol;

namespace Neco.Server.Infrastructure
{
    public abstract class NecoCommandBase : CommandBase<NecoSession, BinaryRequestInfo>
    {

        /// <summary>
        /// Command requires admin rights to be executed
        /// </summary>
        protected virtual bool RequiresAdminAccess { get { return false; } }

        /// <summary>
        /// Command name
        /// </summary>
        protected abstract CommandTypes CommandType { get; }

        public override string Type
        {
            get { return CommandType.ToString(); }
        }

        public sealed override void ExecuteCommand(NecoSession session, BinaryRequestInfo requestInfo)
        {
           // execute
        }
    }
}
