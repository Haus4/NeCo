using Neco.Client.Network;
using Neco.DataTransferObjects;
using System;
using System.Text;
using System.Threading;
using Xamarin.Forms;

namespace Neco.Client.Model
{
    public class ChatModel
    {
        private ViewModel.ChatSession sessionViewmodel;

        public ChatModel(ViewModel.ChatSession model)
        {
            sessionViewmodel = model;

            App.Instance.Connector.Receive(Infrastructure.Protocol.CommandTypes.Request, (data) =>
            {
                try
                {
                    MessageRequest message = RequestSerializer.Deserialize<RequestBase>(data) as MessageRequest;
                    if (message != null)
                    {
                        PushForeignMessage(Encoding.UTF8.GetString(message.Message));
                    }
                }
                catch(Exception)
                {

                }
            });
        }

        public void PushMessage(String message)
        {
            sessionViewmodel.Messages.Add(new ViewModel.ChatMessage
            {
                Time = DateTime.Now,
                Message = message,
                IsForeign = false
            });

            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            byte[] signature = new byte[1];

            MessageRequest request = new MessageRequest
            {
                Message = messageBytes,
                Signature = signature
            };

            App.Instance.Connector.Send(Infrastructure.Protocol.CommandTypes.Request, RequestSerializer.Serialize<RequestBase>(request));
        }

        private void PushForeignMessage(String message)
        {
            sessionViewmodel.Messages.Add(new ViewModel.ChatMessage
            {
                Time = DateTime.Now,
                Message = message,
                IsForeign = true
            });
        }
    }
}
