using log4net;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neco.Server.Infrastructure
{
    public class ChatSessionManager
    {
        private static Dictionary<String, ChatSession> chatSessions;
        private static bool HasSession;
        protected static readonly ILog log = LogManager.GetLogger(typeof(ChatSessionManager));

        static ChatSessionManager()
        { 
            HasSession = false;
            chatSessions = new Dictionary<String, ChatSession>();
        }

        public static void CreateSession(ClientSession hostSession)
        {
            ChatSession chatSession = new ChatSession(hostSession, "CHAT"+hostSession.SessionID,2);
            log.Info("Chat Session created");
            chatSessions.Add(chatSession.SessionId, chatSession);
            HasSession = true;
        }

        public static ChatSession GetSession(String SessionId)
        {
            ChatSession session;
            if (chatSessions.TryGetValue(SessionId, out session))
            {
                return session;
            }
            return null;
        }

        public static void JoinSession(String SessionId, ClientSession ses)
        {
            var hostSession = GetSession(SessionId);
            if(hostSession != null && hostSession.IsOpen != false){
                hostSession.JoinSession(ses);
            }
        }

        public static void LeaveSession(String SessionId, ClientSession ses)
        {
            var hostSession = GetSession(SessionId);
            if (hostSession != null && hostSession.IsOpen != false)
            {
                hostSession.LeaveSession(ses);
            }
        }

        public static void CloseSession(String SessionId)
        {
            chatSessions.Remove(SessionId);
            if(chatSessions.Count < 1) HasSession = false;
        }

        public static String GetIdForNextOpenSession()
        {
            return chatSessions.Values.First(value => value.IsOpen == true).SessionId;
        }

        public static bool IsSessionAvailable()
        {
            if (!HasSession) return false;
            else return chatSessions.Values.Any(value => value.IsOpen == true);
        }
    }
}
