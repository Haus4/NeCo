using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Protocol;
using log4net;

namespace Neco.Server.Infrastructure
{
    public class ClientSession : AppSession<ClientSession, BinaryRequestInfo>
    {
        public bool HasChat { get; private set; }
        protected static readonly ILog log = LogManager.GetLogger(typeof(ClientSession));
        protected override void OnSessionStarted()
        {
            //base.Send("Welcome to Neco Chat!");
        }

        protected override void HandleException(Exception e)
        {
            log.Info("Application error: " + e.Message);
        }

        protected override void OnSessionClosed(CloseReason reason)
        {
            ChatSessionManager.CloseSession(SessionID);
            //add you logics which will be executed after the session is closed
            base.OnSessionClosed(reason);
        }
    }
}
