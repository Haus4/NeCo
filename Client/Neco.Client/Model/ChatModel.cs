using Google.ProtocolBuffers;
using System;
using System.Text;
using System.Threading;
using Xamarin.Forms;

namespace Neco.Client.Model
{
    public class ChatModel
    {
        private ViewModel.ChatSession sessionViewmodel;

        private bool testThreadTerminate;
        private Thread messageTestThread;

        public ChatModel(ViewModel.ChatSession model)
        {
            sessionViewmodel = model;

            testThreadTerminate = false;
            /*messageTestThread = new Thread(new ThreadStart(() =>
            {
                Thread.Sleep(3000);

                while (!testThreadTerminate)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        PushForeignMessage("EatDat***445", "Good evening Twitter");
                    });

                    for (int i = 0; i < 50 && !testThreadTerminate; ++i)
                        Thread.Sleep(100);
                }
            }));*/
            //messageTestThread.Start();

            App.Instance.Connector.Receive(Infrastructure.Protocol.CommandTypes.Message, (data) =>
            {
                try
                {
                    Neco.Proto.Message message = Neco.Proto.Message.ParseFrom(data);
                    if (message != null)
                    {
                        PushForeignMessage("User", Encoding.UTF8.GetString(message.Data.ToByteArray()));
                    }
                }
                catch(Exception)
                {

                }
            });
        }

        public void Close()
        {
            testThreadTerminate = true;
            if (messageTestThread != null && messageTestThread.IsAlive)
            {
                messageTestThread.Join();
            }
        }

        public void PushMessage(String message)
        {
            sessionViewmodel.Messages.Add(new ViewModel.ChatMessage
            {
                User = "You",
                Time = DateTime.Now,
                Message = message,
                IsForeign = false
            });

            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            byte[] signature = new byte[1];

            Proto.Message msg = Proto.Message.CreateBuilder()
                .SetData(ByteString.CopyFrom(messageBytes))
                .SetSignature(ByteString.CopyFrom(signature))
                .BuildPartial();

            App.Instance.Connector.Send(Infrastructure.Protocol.CommandTypes.Message, msg.ToByteArray());
        }

        private void PushForeignMessage(String user, String message)
        {
            sessionViewmodel.Messages.Add(new ViewModel.ChatMessage
            {
                User = user,
                Time = DateTime.Now,
                Message = message,
                IsForeign = true
            });
        }
    }
}
