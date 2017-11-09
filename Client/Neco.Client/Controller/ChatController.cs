using System;
using System.Threading;
using Xamarin.Forms;

namespace Neco.Client
{
    public class ChatController
    {
        private ChatSession sessionModel;

        private bool testThreadTerminate;
        private Thread messageTestThread;

        public ChatController(ChatSession model)
        {
            sessionModel = model;

            testThreadTerminate = false;
            messageTestThread = new Thread(new ThreadStart(() =>
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
            }));
            messageTestThread.Start();
        }

        public void Close()
        {
            testThreadTerminate = true;
            if (messageTestThread.IsAlive)
            {
                messageTestThread.Join();
            }
        }

        public void PushMessage(String message)
        {
            sessionModel.Messages.Add(new ChatMessage
            {
                User = "You",
                Time = DateTime.Now,
                Message = message,
                IsForeign = false
            });

            App.Instance.Connector.Send("TEST " + message + "\r\n");
        }

        private void PushForeignMessage(String user, String message)
        {
            sessionModel.Messages.Add(new ChatMessage
            {
                User = user,
                Time = DateTime.Now,
                Message = message,
                IsForeign = true
            });
        }
    }
}
