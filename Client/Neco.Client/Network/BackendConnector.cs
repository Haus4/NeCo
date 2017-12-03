using Neco.Client.Core;
using Neco.Infrastructure.Protocol;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Neco.Client.Network
{
    public class BackendConnector : Core.StateHandler
    {
        private TcpClient client;
        private Thread receiveThread;
        private Queue<byte> dataQueue;
        private Dictionary<CommandTypes, Action<Byte[]>> handlers;

        private string hostname;
        private int port;

        public BackendConnector(String host)
        {
            dataQueue = new Queue<byte>();
            handlers = new Dictionary<CommandTypes, Action<Byte[]>>();

            var separator = host.LastIndexOf(':');
            if (separator < 0) throw new ArgumentException();

            port = Convert.ToInt32(host.Substring(separator + 1));
            hostname = host.Substring(0, separator);

            Connect();
        }

        public bool Connected
        {
            get
            {
                return client != null && client.Connected;
            }
        }

        public void Connect()
        {
            if(receiveThread == null ||!receiveThread.IsAlive)
            {
                CurrentState = State.Unknown;
                receiveThread = new Thread(() => Runner());
                receiveThread.Start();
            }
        }

        public void Stop()
        {
            client?.GetStream()?.Close();
            client?.Close();
            receiveThread?.Join();
        }

        public void Send(CommandTypes type, byte[] data)
        {
            if (client != null && client.Connected)
            {
                byte[] outData = new byte[data.Length + 8];
                Array.Copy(data, 0, outData, 8, data.Length);
                Array.Copy(BitConverter.GetBytes(outData.Length), 0, outData, 0, 4);
                Array.Copy(BitConverter.GetBytes((int)type), 0, outData, 4, 4);

                Task.Run(async () => await SendData(outData));
            }
        }

        private async Task<bool> SendData(byte[] bytes)
        {
            if (client != null && client.Connected)
            {
                try
                {
                    NetworkStream stream = client.GetStream();
                    await stream.WriteAsync(bytes, 0, bytes.Length);
                    stream.Flush();

                    return true;
                } catch(Exception) {}
            }

            return false;
        }

        private void Runner()
        {
            client = new TcpClient();
            try
            {
                if (!client.ConnectAsync(hostname, port).Wait(3000))
                {
                    client = null;
                }
            }
            catch (Exception)
            {
                client = null;
            }

            if (client == null)
            {
                CurrentState = State.Error;
                return;
            }

            CurrentState = State.Success;

            NetworkStream stream = client.GetStream();
            byte[] bytes = new byte[client.ReceiveBufferSize];
            while (client != null && client.Connected && stream != null && stream.CanRead)
            {
                HandleData();

                if (stream.DataAvailable)
                {
                    try
                    {
                        int length = stream.Read(bytes, 0, (int)bytes.Length);
                        for (int i = 0; i < length; ++i) dataQueue.Enqueue(bytes[i]);
                    }
                    catch(Exception)
                    {

                    }
                }
                else
                {
                    Thread.Sleep(50);
                }
            }

            client = null;
            dataQueue.Clear();
            CurrentState = State.Error;
        }

        private void HandleData()
        {
            while(dataQueue.Count > 4)
            {
                int length = BitConverter.ToInt32(dataQueue.ToArray(), 0);
                if(length <= dataQueue.Count)
                {
                    byte[] data = new byte[length];
                    for(int i = 0; i < length; ++i)
                    {
                        data[i] = dataQueue.Dequeue();
                    }

                    DispatchData(data);
                }
                else
                {
                    break;
                }
            }
        }

        private void DispatchData(byte[] bytes)
        {
            int length = Math.Min(BitConverter.ToInt32(bytes, 0), bytes.Length) - 8;
            CommandTypes type = (CommandTypes)BitConverter.ToInt32(bytes, 4);

            if (handlers.ContainsKey(type))
            {
                byte[] data = new byte[length];
                Array.Copy(bytes, 8, data, 0, length);

                handlers[type](data);
            }
        }

        public void Receive(CommandTypes type, Action<Byte[]> handler)
        {
            if(handlers.ContainsKey(type))
            {
                handlers.Remove(type);
            }

            handlers.Add(type, handler);
        }
    }
}
