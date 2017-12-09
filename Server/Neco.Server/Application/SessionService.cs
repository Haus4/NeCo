using Neco.DataTransferObjects;
using Neco.Server.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neco.Server.Application
{
    public class SessionService : BaseService
    {
        public async Task<SessionResponse> Session(ClientSession session, SessionRequest request)
        {
            session.PublicKey = request.PublicKey;
            var response = request.CreateResponse<SessionResponse>();
            if (session.HasChat) session.LeaveChatSession();
            try
            {
                if (ChatSessionManager.IsSessionAvailable())
                {
                    var chatSessionId = ChatSessionManager.GetIdForNextOpenSession();
                    response.PublicKey = ChatSessionManager.JoinSession(chatSessionId, session);
                    response.Success = true;
                    return response;
                }
                else
                {
                    var publicKey = await ChatSessionManager.CreateSession(session);
                    response.Success = true;
                    response.PublicKey = publicKey;
                    return response;
                }
            }
            catch (Exception exc)
            {
                response.Success = false;
                return response;
            }
        }
        public SessionCloseResponse SessionClose(ClientSession session, SessionCloseRequest request)
        {
            var response = request.CreateResponse<SessionCloseResponse>();
            try
            {
                if (session.HasChat) session.LeaveChatSession();
                response.Success = true;
                return response;
            } catch(Exception e)
            {
                response.Success = false;
                return response;
            }
        }
    }
}
