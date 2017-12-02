using Google.ProtocolBuffers;
using Neco.Infrastructure.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Neco.Server.Infrastructure
{
    public class ChatSession
    {

        private ClientSession sessionCreator;
        private ClientSession[] sessionMembers;

        public String SessionId { get; private set; }
        public bool IsOpen { get; private set; }
        public int TotalMembers { get; }
        public int CurrentMembers { get; private set; }

        public ChatSession(ClientSession _sessionCreator, String _sessionId, int allowedMembers)
        {
            sessionCreator = _sessionCreator;
            SessionId = _sessionId;
            IsOpen = true;
            TotalMembers = allowedMembers;
            CurrentMembers = 1;
            sessionMembers = new ClientSession[allowedMembers];
            _sessionCreator.JoinChatSession(SessionId, 0);
        }

        public void JoinSession(ClientSession sessionMember)
        {
            for(var i=0; i<sessionMembers.Length; i++)
            {
                if(sessionMembers[i] == null)
                {
                    sessionMembers[i] = sessionMember;
                    sessionMember.JoinChatSession(SessionId, i);
                    CurrentMembers++;
                    if (CurrentMembers == TotalMembers) IsOpen = false;
                } else
                {
                    throw new Exception("Client tried to connect to full session...");
                }
            }
            //SendEachMember(_sessionMember.SessionID + " joined your session... say hello 😃");
        }

        public bool IsCreator(String sessId)
        {
            return sessId == sessionCreator.SessionID;
        }

        public void SendToSpecificMember(IPEndPoint endPoint, byte[] data)
        {
            Command cmd = new Command(CommandTypes.Request, data);
            byte[] cmdBytes = CommandParser.ToBytes(cmd);
            if (sessionMembers.Length > 1)
            {
                if (endPoint == sessionCreator.RemoteEndPoint)
                {
                    foreach (ClientSession ses in sessionMembers)
                    {
                        if(ses != null) ses.Send(cmdBytes, 0, cmdBytes.Length);
                    }
                }
                else
                {
                    sessionCreator.Send(cmdBytes, 0, cmdBytes.Length);
                    foreach (ClientSession ses in sessionMembers)
                    {
                        if (ses != null && ses.RemoteEndPoint != endPoint)
                        {
                            ses.Send(cmdBytes, 0, cmdBytes.Length);
                        }
                    }
                }
            }
        }

        public void LeaveSession(ClientSession ses)
        {
            if(ses.HasChat && ses == sessionCreator)
            {
                sessionCreator = null;
                CloseSession();
            } else if(ses.ChatSessionId == SessionId && sessionMembers[ses.ChatMemberId] == ses)
            {
                sessionMembers[ses.ChatMemberId] = null;
                CurrentMembers--;
                if(CurrentMembers == 0) CloseSession();
            }
        }

        public void SendEachMember(string message)
        {
            //TODO
            //SendEachMember(cmdBytes, 0, cmdBytes.Length);
        }

        public void SendEachMember(byte[] data, int offset, int length)
        {
            sessionCreator.Send(data, offset, length);
            foreach (ClientSession ses in sessionMembers)
            {
                ses.Send(data, offset, length);
            }
        }

        public void CloseSession()
        {
            IsOpen = false;
            ChatSessionManager.CloseSession(SessionId);
        }

    }
}
