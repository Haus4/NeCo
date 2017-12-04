using Neco.Client.Core;
using Neco.DataTransferObjects;
using Neco.Infrastructure.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Neco.Client.Network
{
    public class BackendConnector : StateHandler
    {
        private TcpClient client;
        private Thread receiveThread;
        private Queue<byte> dataQueue;
        private Dictionary<Type, Action<RequestBase>> handlers;

        private string hostname;
        private int port;

        public BackendConnector(String host)
        {
            dataQueue = new Queue<byte>();
            handlers = new Dictionary<Type, Action<RequestBase>>();

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

        public void Send(CommandTypes type, DataTransferObjects.RequestBase request)
        {
            if (client != null && client.Connected)
            {
                byte[] data = RequestSerializer.Serialize(request);
                Command command = new Command(type, data);
                byte[] outData = CommandParser.ToBytes(command);

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
                    client.Close();
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

            stream?.Close();
            client?.Close();
            client = null;
            dataQueue.Clear();
            CurrentState = State.Error;
        }

        private void HandleData()
        {
            while(dataQueue.Count > 8)
            {
                int length = CommandParser.ParseBodyLength(dataQueue.ToArray(), 0, dataQueue.Count);
                CommandTypes type = CommandParser.ParseCommandType(dataQueue.Skip(4).ToArray());
                
                if (length <= dataQueue.Count)
                {
                    for(int i = 0; i < 8; ++i) dataQueue.Dequeue();

                    byte[] data = new byte[length];
                    for(int i = 0; i < length; ++i)
                    {
                        data[i] = dataQueue.Dequeue();
                    }

                    DispatchData(type, data);
                }
                else
                {
                    break;
                }
            }
        }

        private void DispatchData(CommandTypes type, byte[] bytes)
        {
            DataTransferObjects.RequestBase obj = RequestSerializer.Deserialize<DataTransferObjects.RequestBase>(bytes);

            if (type == CommandTypes.Request)
            {
                if (handlers.ContainsKey(obj.GetType()))
                {
                    handlers[obj.GetType()](obj);
                }
            }
            else if(type == CommandTypes.Response)
            {

            }
        }

        public void Receive<T>(Action<RequestBase> handler)
        {
            if(handlers.ContainsKey(typeof(T)))
            {
                handlers.Remove(typeof(T));
            }

            handlers.Add(typeof(T), handler);
        }
    }
}
