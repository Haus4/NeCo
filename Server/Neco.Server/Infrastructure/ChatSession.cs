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
    public class ChatSession
    {

        private ClientSession sessionCreator;
        private ClientSession[] sessionMembers;

        public String LobbyId { get; private set; }
        public String SessionId { get; private set; }
        public bool IsOpen { get; private set; }
        public int TotalMembers { get; }
        public int CurrentMembers { get; private set; }
        protected static readonly ILog log = LogManager.GetLogger(typeof(ChatSession));

        private bool hasMember = false;

        public ChatSession(ClientSession _sessionCreator, String lobbyId, String sessionId, int allowedMembers)
        {
            sessionCreator = _sessionCreator;
            SessionId = sessionId;
            LobbyId = lobbyId;
            IsOpen = true;
            TotalMembers = allowedMembers;
            CurrentMembers = 1;
            sessionMembers = new ClientSession[allowedMembers];
            _sessionCreator.JoinChatSession(SessionId);
        }

        public byte[] JoinSession(ClientSession sessionMember)
        {
            for(var i=0; i<sessionMembers.Length; i++)
            {
                if(sessionMembers[i] == null)
                {
                    sessionMembers[i] = sessionMember;
                    sessionMember.JoinChatSession(SessionId);
                    CurrentMembers++;
                    if (CurrentMembers == TotalMembers) IsOpen = false;
                    hasMember = true;
                    break;
                } else
                {
                    log.Error("Client tried to connect to full session... " + CurrentMembers + "/" + TotalMembers + " " + sessionMember.SessionID);
                }
            }
            return sessionCreator.PublicKey;
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

        public void CloseSession(ClientSession ses)
        {
            var request = new SessionCloseRequest()
            {
                Token = 0
            };
            if (ses != sessionCreator)
            {
                sessionCreator.Send<RequestBase>(request);
            }
            else
            {
                foreach (ClientSession sess in sessionMembers)
                {
                    if(sess!=null) sess.Send<RequestBase>(request);
                };
            }
        }

    }
}
