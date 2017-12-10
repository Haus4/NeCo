using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neco.DataTransferObjects;
using Neco.Server.Infrastructure;

namespace Neco.Server.Application
{
    public class LobbyService : BaseService
    {
        public LobbyResponse Lobby(ClientSession session, LobbyRequest request)
        {
            session.PublicKey = request.PublicKey;
            var response = new LobbyResponse();
            response.Token = request.Token;
            if (session.HasLobby) session.LeaveChatLobby();
            try
            {
                if (ChatLobbyManager.IsLobbyAvailable())
                {
                    var chatLobbyId = ChatLobbyManager.GetIdForNextOpenLobby();
                    var chatLobby = ChatLobbyManager.GetLobby(chatLobbyId);
                    response.Latitude = chatLobby.Latitude;
                    response.Longitude = chatLobby.Longitude;
                    response.LobbyId = chatLobby.LobbyId;
                    response.MemberPublicKeys = chatLobby.GetMemberKeys();
                    response.Range = chatLobby.Range;
                    response.Success = true;
                    return response;
                }
                else
                {
                    var chatLobbyId = ChatLobbyManager.CreateLobby(session, request.Longitude, request.Latitude);
                    var chatLobby = ChatLobbyManager.GetLobby(chatLobbyId);
                    response.Latitude = chatLobby.Latitude;
                    response.Longitude = chatLobby.Longitude;
                    response.LobbyId = chatLobby.LobbyId;
                    response.MemberPublicKeys = chatLobby.GetMemberKeys();
                    response.Range = chatLobby.Range;
                    response.Success = true;
                    return response;
                }
            }
            catch (Exception exc)
            {
                response.Success = false;
                return response;
            }
        }

        public LeaveLobbyResponse LeaveLobby(ClientSession session, LeaveLobbyRequest request)
        {
            var response = new LeaveLobbyResponse();
            response.Token = request.Token;
            return response;
        }
    }
}
