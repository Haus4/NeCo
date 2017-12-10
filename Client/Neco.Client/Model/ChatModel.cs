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
        private ViewModel.ChatViewModel chatViewModel;
        private byte[] remotePublicKey;

        public ChatModel(ViewModel.ChatViewModel viewModel)
        {
            chatViewModel = viewModel;

            App.Instance.Connector.Receive<MessageRequest>((message) =>
            {
                if (App.Instance.CryptoHandler.VerifySignature(remotePublicKey, message.Message, message.Signature))
                {
                    PushForeignMessage(Encoding.UTF8.GetString(message.Message));
                }
            });

            App.Instance.Connector.Receive<SessionCloseRequest>((message) =>
            {
                chatViewModel.View.Close();

                Device.BeginInvokeOnMainThread(() =>
                {
                    IMessage messageHandler = DependencyService.Get<IMessage>();
                    messageHandler?.ShowToast("Partner left the chat");
                });
            });
        }

        public void CloseSession()
        {
            App.Instance.Connector.Receive<MessageRequest>(null);
            App.Instance.Connector.Receive<SessionCloseRequest>(null);

            SessionCloseRequest request = new SessionCloseRequest
            {
                Signature = new byte[1]
            };

            Task.Run(async () => await App.Instance.Connector.SendRequest(request));
        }

        public async Task<bool> Join(byte[] memberKey)
        {
            SessionRequest request = new SessionRequest
            {
                MemberKey = memberKey
            };

            var response = await App.Instance.Connector.SendRequest(request, 30000);
            if (response != null && response is SessionResponse sessionResp && sessionResp.Success)
            {
                remotePublicKey = memberKey;
                return true;
            }

            return false;
        }

        public void PushMessage(String message)
        {
            chatViewModel.Messages.Add(new ViewModel.ChatMessage
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
            chatViewModel.Messages.Add(new ViewModel.ChatMessage
            {
                Time = DateTime.Now,
                Message = message,
                IsForeign = true
            });
        }
    }
}
