using log4net;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neco.Server.Infrastructure
{
    public class ChatLobbyManager
    {
        private static bool HasLobby;
        private static Dictionary<String, ChatLobby> chatLobbies;
        protected static readonly ILog log = LogManager.GetLogger(typeof(ChatLobbyManager));

        static ChatLobbyManager()
        {
            HasLobby = false;
            chatLobbies = new Dictionary<String, ChatLobby>();
        }

        public static String CreateLobby(ClientSession hostSession, Double longitude, Double latitude)
        {
            ChatLobby chatLobby = new ChatLobby(hostSession, "Lobby:"+hostSession.SessionID,10, longitude, latitude, 5000);
            log.Info("Chat Lobby created");
            chatLobbies.Add(chatLobby.LobbyId, chatLobby);
            HasLobby = true;
            hostSession.JoinChatLobby(chatLobby.LobbyId);
            return chatLobby.LobbyId;
        }

        public static ChatLobby GetLobby(String lobbyId)
        {
            if (chatLobbies.TryGetValue(lobbyId, out ChatLobby lobby))
            {
                return lobby;
            }
            return null;
        }
        public static void JoinLobby(String lobbyId, ClientSession ses)
        {
            var chatLobby = GetLobby(lobbyId);
            if (chatLobby != null && chatLobby.IsOpen != false)
            {
                chatLobby.JoinLobby(ses);
            }
        }

        public static void LeaveLobby(String lobbyId, ClientSession ses)
        {
            var lobby = GetLobby(lobbyId);
            if (lobby != null)
            {
                lobby.LeaveLobby(ses);
                if(lobby.IsOpen == false) chatLobbies.Remove(lobbyId);
            }
            if (chatLobbies.Count < 1) HasLobby = false;
        }

        public static void OpenSession(String lobbyId, ClientSession hostSession)
        {
            var lobby = GetLobby(lobbyId);
            if (lobby != null)
            {
                lobby.CreateSession(hostSession);
            } else
            {
                log.Error("No such lobby for Id: " + lobbyId);
            }
        }

        public static bool HasMemberSession(String lobbyId, byte[] memberKey)
        {
            var lobby = GetLobby(lobbyId);
            if (lobby != null)
            {
                return lobby.HasMemberSession(memberKey);
            }
            else
            {
                log.Error("No such lobby for Id: " + lobbyId);
                return false;
            };
        }

        public static byte[] JoinSession(String lobbyId, byte[] memberKey, ClientSession ses)
        {
            var lobby = GetLobby(lobbyId);
            var hostSession = lobby?.GetSession(memberKey);
            if(hostSession != null && hostSession.IsOpen != false){
                return hostSession.JoinSession(ses);
            }
            log.Error("No such lobby or session for Id and key: "+ Environment.NewLine + "lobby: " +  lobbyId + Environment.NewLine + "memberKey: " + memberKey.ToString());
            return null;
        }

        public static void CloseSession(String lobbyId, String sessionId, ClientSession ses)
        {
            var lobby = GetLobby(lobbyId);
            if(lobby != null) lobby.CloseSession(sessionId, ses);
        }

        public static String GetIdForNextOpenLobby()
        {
            return chatLobbies.Values.First(value => value.IsOpen == true).LobbyId;
        }

        public static bool IsLobbyAvailable()
        {
            if (!HasLobby) return false;
            else return chatLobbies.Values.Any(value => value.IsOpen == true);
        }
    }
}
