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
        private ClientSession sessionMember = null;

        public int SessionId { get; private set; }
        public bool IsOpen { get; private set; }

        public ChatSession(ClientSession _sessionCreator, int _sessionId)
        {
            sessionCreator = _sessionCreator;
            SessionId = _sessionId;
            IsOpen = true;
        }

        public void JoinSession(ClientSession _sessionMember)
        {
            sessionMember = _sessionMember;
            //SendEachMember(_sessionMember.SessionID + " joined your session... say hello 😃");
        }

        public bool IsCreator(String sessId)
        {
            return sessId == sessionCreator.SessionID;
        }

        public void SendToSpecificMember(IPEndPoint endPoint, byte[] data, int offset, int length)
        {
            byte[] newValues = new byte[length + 8];
            byte[] _length = BitConverter.GetBytes(length + 8);
            byte[] type = BitConverter.GetBytes((int)CommandTypes.Message);
            Array.Copy(data, offset, newValues, 8, length);
            Array.Copy(type, 0, newValues, 4, type.Length);
            Array.Copy(_length, 0, newValues, 0, _length.Length);
            if (sessionMember != null)
            {
                if (endPoint == sessionMember.RemoteEndPoint)
                {
                    sessionCreator.Send(newValues, 0, newValues.Length);
                }
                else
                {
                    sessionMember.Send(newValues, 0, newValues.Length);
                }
            }
        }
        
        public void SendEachMember(string message)
        {
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            byte[] signature = new byte[1];
            Neco.Proto.Message msg = Neco.Proto.Message.CreateBuilder()
                .SetData(ByteString.CopyFrom(messageBytes))
                .SetSignature(ByteString.CopyFrom(signature))
                .BuildPartial();
            byte[] data = msg.ToByteArray();

            byte[] outData = new byte[data.Length + 8];
            Array.Copy(data, 0, outData, 8, data.Length);
            Array.Copy(BitConverter.GetBytes(outData.Length), 0, outData, 0, 4);
            Array.Copy(BitConverter.GetBytes((int)CommandTypes.Message), 0, outData, 4, 4);

            SendEachMember(outData, 0, outData.Length);
        }

        public void SendEachMember(byte[] data, int offset, int length)
        {
            sessionCreator.Send(data, offset, length);
            sessionMember.Send(data, offset, length);
        }

        public void CloseSession()
        {
            IsOpen = false;
        }

    }
}
