using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Config;
using SuperSocket.SocketBase.Protocol;
using Neco.Server.Application.Interfaces;

namespace Neco.Server.Infrastructure
{
    public class SocketServer : AppServer<NecoSession>, ISessionManager
    {
        protected override bool Setup(IRootConfig rootConfig, IServerConfig config)
        {
            Console.WriteLine("setting up");
            return base.Setup(rootConfig, config);
        }

        protected override void OnStartup()
        {
            base.OnStartup();
        }

        protected override void OnStopped()
        {
            base.OnStopped();
        }

        protected override void ExecuteCommand(NecoSession session, StringRequestInfo requestInfo)
        {
            Console.WriteLine("Command invoked.");
            base.ExecuteCommand(session, requestInfo);
        }

        protected override void OnNewSessionConnected(NecoSession session)
        {
            Console.WriteLine("New session connected.");
            base.OnNewSessionConnected(session);
        }

        protected override void OnSessionClosed(NecoSession session, CloseReason reason)
        {
            Console.WriteLine("Session closed.");
            base.OnSessionClosed(session, reason);
        }
    }
}
