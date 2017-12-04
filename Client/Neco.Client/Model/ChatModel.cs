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
        private byte[] remotePublicKey;

        public ChatModel(ViewModel.ChatSession viewModel)
        {
            sessionViewmodel = viewModel;

            App.Instance.Connector.Receive<MessageRequest>((message) =>
            {
                if (App.Instance.CryptoHandler.VerifySignature(remotePublicKey, message.Message, message.Signature))
                {
                    PushForeignMessage(Encoding.UTF8.GetString(message.Message));
                }
            });
        }

        public async Task<bool> Join()
        {
            SessionRequest request = new SessionRequest
            {
                PublicKey = App.Instance.CryptoHandler.SerializePublicKey(),
                Signature = App.Instance.CryptoHandler.CalculateSecuritySignature(),
                Latitude = App.Instance.Locator.Position?.Latitude ?? 0.0,
                Longitude = App.Instance.Locator.Position?.Longitude ?? 0.0
            };

            var response = await App.Instance.Connector.SendRequest(request, 30000);
            if (response != null && response is SessionResponse sessionResp && sessionResp.Success)
            {
                remotePublicKey = sessionResp.PublicKey;
                return true;
            }

            return false;
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

            Task.Run(async () => await App.Instance.Connector.SendRequest(request));
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
