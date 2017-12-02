using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Config;
using SuperSocket.SocketBase.Protocol;
using System.Collections;
using log4net;

namespace Neco.Server.Infrastructure
{
    public class SocketServer : AppServer<ClientSession, BinaryRequestInfo>
    {
        protected static readonly ILog log = LogManager.GetLogger(typeof(SocketServer));
        public SocketServer()
            : base(new DefaultReceiveFilterFactory<NecoReceiveFilter, BinaryRequestInfo>()) { }

        protected override bool Setup(IRootConfig rootConfig, IServerConfig config)
        {
            return base.Setup(rootConfig, config);
        }

        protected override void OnStopped()
        {
            base.OnStopped();
        }

        protected override void ExecuteCommand(ClientSession session, BinaryRequestInfo requestInfo)
        {
            log.Info("["+ session.RemoteEndPoint + "] Command invoked: " + requestInfo.Key);
            base.ExecuteCommand(session, requestInfo);
        }

        protected override void OnNewSessionConnected(ClientSession session)
        {
            log.Info("[" + session.RemoteEndPoint + "] New session connected.");
            base.OnNewSessionConnected(session);
        }

        protected override void OnSessionClosed(ClientSession session, CloseReason reason)
        {
            log.Info("[" + session.RemoteEndPoint + "] Session closed.");
            base.OnSessionClosed(session, reason);
        }
    }
}
