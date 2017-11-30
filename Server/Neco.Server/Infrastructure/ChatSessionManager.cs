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

        public static void CreateSession(ChatSession session)
        {
            chatSessions = new ArrayList();
            chatSessions.Add(session);
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
    }
}
