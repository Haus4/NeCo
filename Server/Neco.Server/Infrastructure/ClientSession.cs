using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Protocol;
using log4net;
using Neco.Server.Application;
using Neco.DataTransferObjects;
using Neco.Infrastructure.Protocol;

namespace Neco.Server.Infrastructure
{
    public class ClientSession : AppSession<ClientSession, BinaryRequestInfo>
    {
        public bool HasChat { get; private set; }
        public String ChatSessionId { get; private set; }
        public int ChatMemberId { get; private set; }
        public byte[] PublicKey { get; set; }
        protected static readonly ILog log = LogManager.GetLogger(typeof(ClientSession));
        protected override void OnSessionStarted()
        {
            //base.Send("Welcome to Neco Chat!");
        }

        protected override void HandleException(Exception e)
        {
            log.Error("Application error: " + e.Message);
        }

        protected override void OnSessionClosed(CloseReason reason)
        {
            if(HasChat) LeaveChatSession();
            //add you logics which will be executed after the session is closed
            base.OnSessionClosed(reason);
        }

        public void JoinChatSession(String sessionId, int memberId)
        {
            HasChat = true;
            ChatMemberId = memberId;
            ChatSessionId = sessionId;
        }

        public void LeaveChatSession()
        {
            if(HasChat) ChatSessionManager.CloseSession(ChatSessionId, this);
            HasChat = false;
            ChatMemberId = -1;
            ChatSessionId = null;
        }

        public void Send<T>(T obj) where T : class
        {
            try
            {
                log.InfoFormat("SEND ({1}): {0}", obj.GetType().Name, SessionID);
                var objBytes = RequestSerializer.Serialize(obj);
                var cmd = CommandTypes.Request;
                if (obj is ResponseBase)
                    cmd = CommandTypes.Response;

                var commandBytes = CommandParser.ToBytes(new Command(cmd, objBytes));
                Send(commandBytes, 0, commandBytes.Length);
            }
            catch (Exception exc)
            {
                log.ErrorFormat("{0}"+Environment.NewLine+"Tried to send {1} to {2}", exc.Message, obj == null ? "NULL" : obj.GetType().Name, SessionID);
            }
        }

        internal void AppendResponse(byte[] responseData)
        {
            try
            {
                var response = RequestSerializer.Deserialize<ResponseBase>(responseData);
                if (response != null)
                    RequestsHandler.AppendResponse(response);
            }
            catch (Exception exc)
            {
                log.ErrorFormat("{0}"+Environment.NewLine+"Tried to recognize {1} bytes as request for {2}", exc.Message, responseData == null ? "NULL" : responseData.Length.ToString(), SessionID);
            }
        }
    }
}
