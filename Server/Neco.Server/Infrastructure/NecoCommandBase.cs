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
    public abstract class NecoCommandBase : CommandBase<ClientSession, BinaryRequestInfo>
    {

        /// <summary>
        /// Command requires admin rights to be executed
        /// </summary>
        //protected virtual bool RequiresAdminAccess { get { return false; } }

        /// <summary>
        /// Command name
        /// </summary>
        protected abstract CommandTypes CommandType { get; }

        /// <summary>
        /// Command handler
        /// </summary>
        public abstract void ExecuteExternalCommand(ClientSession session, byte[] data);

        public override string Name
        {
            get { return CommandType.ToString(); }
        }

        public sealed override void ExecuteCommand(ClientSession session, BinaryRequestInfo requestInfo)
        {
            /*if (RequiresAdminAccess)
            {
                if (!session.IsAuthorized || session.User.Role != UserRole.Admin)
                {
                    //Logger.Warn("Sending access denided (ADMIN command) to {0}", Name);
                    //session.SendCommand(Name, Answers.AccessDenided);
                    return;
                }
            }
            else
            {
                //Logger.Warn("Sending access denided to {0}", Name);
                //session.SendCommand(Name, Answers.AccessDenided);
                return;
            }
            */
            try
            {
                ExecuteExternalCommand(session, requestInfo.Body);
            }
            catch (Exception exc)
            {
                //TODO: log here?
                //Console.WriteLine(exc);
            }
        }
    }
}
