using Google.ProtocolBuffers;
using log4net;
using Neco.DataTransferObjects;
using Neco.Infrastructure.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Neco.Server.Infrastructure
{
    public class ChatLobby
    {

        private static bool HasSession;
        private Dictionary<string, ChatSession> chatSessions;
        private Dictionary<string, ClientSession> lobbyMembers;

        public String LobbyId { get; private set; }
        public bool IsOpen { get; private set; }
        public int TotalMembers { get; }
        public int CurrentMembers { get; private set; }

        public Double Longitude { get; private set; }
        public Double Latitude { get; private set; }
        public long Range { get; private set; }

        protected static readonly ILog log = LogManager.GetLogger(typeof(ChatLobby));

        public ChatLobby(ClientSession lobbyCreator, String lobbyId, int allowedMembers, Double longitude, Double latitude, long range)
        {
            LobbyId = lobbyId;
            IsOpen = true;
            TotalMembers = allowedMembers;
            CurrentMembers = 1;
            Longitude = longitude;
            Latitude = latitude;
            Range = range;
            lobbyMembers = new Dictionary<string, ClientSession>();
            lobbyMembers.Add(BitConverter.ToString(lobbyCreator.PublicKey), lobbyCreator);
        }

        public void JoinLobby(ClientSession lobbyMember)
        {
            if (IsOpen && CurrentMembers < TotalMembers)
            {
                lobbyMembers.Add(BitConverter.ToString(lobbyMember.PublicKey), lobbyMember);
                CurrentMembers++;
                if (CurrentMembers == TotalMembers) IsOpen = false;
            } else
            {
                log.Error("Client tried to connect to full lobby... " + CurrentMembers + "/" + TotalMembers + " " + lobbyMember.ChatLobbyId);
            }
        }

        public List<string> GetMemberKeys()
        {
            return lobbyMembers.Keys.ToList();
        }

        public void LeaveLobby(ClientSession lobbyMember)
        {
            string key = BitConverter.ToString(lobbyMember.PublicKey);
            lobbyMembers.Remove(key);
            CurrentMembers--;
            if (CurrentMembers == TotalMembers) IsOpen = false;
        }

        public Task<byte[]> CreateSession(ClientSession hostSession)
        {
            ChatSession chatSession = new ChatSession(hostSession, LobbyId, "CHAT" + hostSession.SessionID, 2);
            log.Info("Chat Session created");
            chatSessions.Add(chatSession.SessionId, chatSession);
            return chatSession.AwaitMemberKey();
        }

        public bool HasMemberSession(byte[] memberKey)
        {
            ClientSession session;
            string key = BitConverter.ToString(memberKey);
            if (lobbyMembers.TryGetValue(key, out session))
            {
                return session.HasChat;
            }
            return false;
        }

        public ChatSession GetSession(byte[] memberKey)
        {
            string key = BitConverter.ToString(memberKey);
            if (lobbyMembers.TryGetValue(key, out ClientSession session) && 
                chatSessions.TryGetValue(session.SessionID, out ChatSession chatSession))
            {
                return chatSession;
            }
            return null;
        }

        public ChatSession GetSession(String sessionId)
        {
            if (chatSessions.TryGetValue(sessionId, out ChatSession chatSession))
            {
                return chatSession;
            }
            return null;
        }

        public byte[] JoinSession(String SessionId, ClientSession ses)
        {
            var hostSession = GetSession(SessionId);
            if (hostSession != null && hostSession.IsOpen != false)
            {
                return hostSession.JoinSession(ses);
            }
            return null;
        }

        public void CloseSession(String SessionId, ClientSession ses)
        {
            var hostSession = GetSession(SessionId);
            if (hostSession != null)
            {
                hostSession.CloseSession(ses);
                chatSessions.Remove(SessionId);
            }
            if (chatSessions.Count < 1) HasSession = false;
        }

        public void SendEachMember(string message)
        {
            //TODO
            //SendEachMember(cmdBytes, 0, cmdBytes.Length);
        }

        public void SendEachMember(byte[] data, int offset, int length)
        {
            foreach (ClientSession session in lobbyMembers.Values)
            {
                session.Send(data, offset, length);
            }
        }

    }
}
