using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Protocol;

namespace Neco.Server.Infrastructure
{
    public class NecoSession : AppSession<NecoSession, BinaryRequestInfo>
    {
        protected override void OnSessionStarted()
        {
            base.Send("Welcome to Neco Chat!");
        }

        protected override void HandleException(Exception e)
        {
            base.Send("Application error: {0}", e.Message);
        }

        protected override void OnSessionClosed(CloseReason reason)
        {
            //add you logics which will be executed after the session is closed
            base.OnSessionClosed(reason);
        }
    }
}
