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
        private static ArrayList chatSessions;
        private static bool HasSession = false;
        protected static readonly ILog log = LogManager.GetLogger(typeof(ChatSessionManager));

        public static void CreateSession(ChatSession session)
        {
            log.Info("Chat Session created");
            chatSessions = new ArrayList();
            chatSessions.Add(session);
            HasSession = true;
        }

        public static ChatSession GetSession()
        {
            foreach(ChatSession ses in chatSessions)
            {
                return ses;
            }
            return null;
        }

        public static void JoinSession(ClientSession ses)
        {
            foreach (ChatSession sess in chatSessions)
            {
                sess.JoinSession(ses);
            }
        }

        public static void CloseSession(String sessId)
        {
            if(chatSessions != null){
                foreach (ChatSession sess in chatSessions)
                {
                    if (sess.IsCreator(sessId))
                    {
                        chatSessions = new ArrayList();
                        HasSession = false;
                    }
                }
            }
        }

        public static bool IsSessionAvailable()
        {
            return !HasSession;
        }
    }
}
