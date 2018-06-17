using libsignal.ecc;
using Neco.Client.Network;
using Neco.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Neco.Client.Model
{
    public class ChatModel
    {
        private ViewModel.ChatViewModel chatViewModel;
        private IMessage messageHandler;
        private byte[] remotePublicKey;

        public ChatModel(ViewModel.ChatViewModel viewModel, bool showMessage = true)
        {
            messageHandler = showMessage ? DependencyService.Get<IMessage>() : null;
            chatViewModel = viewModel;

            App.Instance.Connector.Receive<MessageRequest>((message) =>
            {
                if (App.Instance.CryptoHandler.VerifySignature(remotePublicKey, message.Message, message.Signature))
                {
                    PushForeignData(message.Message);
                }
            });

            App.Instance.Connector.Receive<SessionCloseRequest>((message) =>
            {
                chatViewModel.View.Close();

                Device.BeginInvokeOnMainThread(() =>
                {
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
                IsForeign = false,
                IsImage = false
            });

            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            MessageRequest request = new MessageRequest
            {
                Message = messageBytes,
                Signature = App.Instance.CryptoHandler.CalculateSignature(messageBytes)
            };

            Task.Run(async () => await App.Instance.Connector.SendRequest(request));
        }

        public void PushImage(byte[] image)
        {
            MemoryStream stream = new MemoryStream(image);
            stream.Seek(0, SeekOrigin.Begin);

            chatViewModel.Messages.Add(new ViewModel.ChatMessage
            {
                Time = DateTime.Now,
                Image = ImageSource.FromStream(() => stream),
                IsForeign = false,
                IsImage = true
            });

            List<byte> list = new List<byte>
            {
                0xFF,
                0xFF,
                0xFF,
                0xFF
            };
            list.AddRange(image);

            byte[] messageBytes = list.ToArray();
            MessageRequest request = new MessageRequest
            {
                Message = messageBytes,
                Signature = App.Instance.CryptoHandler.CalculateSignature(messageBytes)
            };

            Task.Run(async () => await App.Instance.Connector.SendRequest(request));
        }

        private void PushForeignData(byte[] data)
        {
            if(data.Length >= 4 && data[0] == 0xFF && data[1] == 0xFF && data[2] == 0xFF && data[3] == 0xFF)
            {
                PushForeignImage(new List<byte>(data).Skip(4).ToArray());
            }
            else
            {
                PushForeignMessage(Encoding.UTF8.GetString(data));
            }
        }

        private void PushForeignImage(byte[] image)
        {
            MemoryStream stream = new MemoryStream(image);
            stream.Seek(0, SeekOrigin.Begin);

            chatViewModel.Messages.Add(new ViewModel.ChatMessage
            {
                Time = DateTime.Now,
                Image = ImageSource.FromStream(() => stream),
                IsForeign = true,
                IsImage = true
            });
        }

        private void PushForeignMessage(String message)
        {
            chatViewModel.Messages.Add(new ViewModel.ChatMessage
            {
                Time = DateTime.Now,
                Message = message,
                IsForeign = true,
                IsImage = false
            });
        }
    }
}
