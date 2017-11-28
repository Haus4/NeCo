using Neco.Infrastructure.Protocol;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Xamarin.Forms;

namespace Neco.Client.Network
{
    public class BackendConnector
    {
        private TcpClient client;
        private IMessage messageHandler;
        private Thread receiveThread;
        private Dictionary<CommandTypes, Action<Byte[]>> handlers;

        public BackendConnector(String host)
        {
            handlers = new Dictionary<CommandTypes, Action<Byte[]>>();
            messageHandler = DependencyService.Get<IMessage>();

            var separator = host.LastIndexOf(':');
            if (separator < 0) throw new ArgumentException();

            int port = Convert.ToInt32(host.Substring(separator + 1));
            String server = host.Substring(0, separator);

            receiveThread = new Thread(() => Runner(server, port));
            receiveThread.Start();
        }

        public bool Connected
        {
            get
            {
                return client != null && client.Connected;
            }
        }


        public void Stop()
        {
            client?.Close();
            receiveThread?.Join();
        }

        public void Send(CommandTypes type, byte[] data)
        {
            byte[] outData = new byte[data.Length + 8];
            Array.Copy(data, 0, outData, 8, data.Length);
            Array.Copy(BitConverter.GetBytes(outData.Length), 0, outData, 0, 4);
            Array.Copy(BitConverter.GetBytes((int)type), 0, outData, 4, 4);
           

            if (client != null && client.Connected)
            {
                NetworkStream stream = client.GetStream();
                stream.Write(outData, 0, outData.Length);
            }
        }

        private void Runner(String server, int port)
        {
            client = new TcpClient();
            if (!client.ConnectAsync(server, port).Wait(2000))
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    messageHandler.ShowToast("Unable to connect to backend");
                });

                return;
            }

            NetworkStream stream = client.GetStream();
            byte[] bytes = new byte[client.ReceiveBufferSize];
            while (client.Connected && stream != null && stream.CanRead)
            {
                if (stream.DataAvailable)
                {
                    stream.Read(bytes, 0, (int)bytes.Length);

                    int length = Math.Min(BitConverter.ToInt32(bytes, 0), bytes.Length);
                    CommandTypes type = (CommandTypes)BitConverter.ToInt32(bytes, 4);

                    if (handlers.ContainsKey(type))
                    {
                        byte[] data = new byte[length];
                        Array.Copy(bytes, 8, data, 0, length - 8);

                        handlers[type](data);
                    }
                }
                else
                {
                    Thread.Sleep(10);
                }
            }
        }

        public void Receive(CommandTypes type, Action<Byte[]> handler)
        {
            handlers.Add(type, handler);
        }
    }
}
