using libsignal.ecc;
using Neco.Client.Network;
using Neco.DataTransferObjects;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Neco.Client.Model
{
    public class ChatModel
    {
        private ViewModel.ChatSession sessionViewmodel;
        //private byte[] remotePublicKey; // TODO: Get that data from the server

        public ChatModel(ViewModel.ChatSession model)
        {
            sessionViewmodel = model;

            App.Instance.Connector.Receive<MessageRequest>((message) =>
            {
                PushForeignMessage(Encoding.UTF8.GetString(message.Message));
            });

            SessionRequest request = new SessionRequest
            {
                PublicKey = App.Instance.CryptoHandler.SerializePublicKey(),
                Signature = App.Instance.CryptoHandler.CalculateSecuritySignature(),
                Latitude = App.Instance.Locator.Position?.Latitude ?? 0.0,
                Longitude = App.Instance.Locator.Position?.Longitude ?? 0.0
            };

            Task.Run(async () => await App.Instance.Connector.SendRequest(request));
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
            MessageRequest request = new MessageRequest
            {
                Message = messageBytes,
                Signature = App.Instance.CryptoHandler.CalculateSignature(messageBytes)
            };

            Task.Run(async () =>
            {
                var response = await App.Instance.Connector.SendRequest(request);
                if (response != null)
                {
                    Console.WriteLine("Response received!");
                }
            });
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
