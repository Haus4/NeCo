using Neco.Client.Core;
using Neco.DataTransferObjects;
using Neco.Infrastructure.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        private Dictionary<Type, Func<RequestBase, ResponseBase>> handlers;
        private Dictionary<long, ResponseBase> responses;

        private string hostname;
        private int port;

        public BackendConnector(String host)
        {
            dataQueue = new Queue<byte>();
            handlers = new Dictionary<Type, Func<RequestBase, ResponseBase>>();
            responses = new Dictionary<long, ResponseBase>();

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
                return client != null && client.Connected && client.GetStream() != null && client.GetStream().CanRead;
            }
        }

        public void Connect()
        {
            if (receiveThread == null || !receiveThread.IsAlive)
            {
                CurrentState = State.Unknown;
                receiveThread = new Thread(() => Runner());
                receiveThread.Start();
            }
        }

        public void Stop()
        {
            CloseSocket();
            receiveThread?.Join();
        }

        public async Task SendResponse(ResponseBase response)
        {
            if (Connected)
            {
                byte[] data = RequestSerializer.Serialize(response);
                await SendCommand(CommandTypes.Response, data);
            }
        }

        public async Task<ResponseBase> SendRequest(RequestBase request, int timeout = 3000)
        {
            ResponseBase result = null;

            if (Connected)
            {
                byte[] data = RequestSerializer.Serialize(request);
                await SendCommand(CommandTypes.Request, data);

                lock (responses)
                {
                    // This tells the system that we're waiting for a specific response with this token
                    responses[request.Token] = null;
                }

                WaitForResponse(request.Token).Wait(timeout);

                lock (responses)
                {
                    if (responses.ContainsKey(request.Token))
                    {
                        result = responses[request.Token];
                        responses.Remove(request.Token);
                    }
                }
            }

            return result;
        }

        private async Task WaitForResponse(long token)
        {
            while (true)
            {
                lock (responses)
                {
                    if (!responses.ContainsKey(token)) break;
                    if (responses[token] != null) break;
                }

                await Task.Delay(10);
            }
        }

        public async Task SendCommand(CommandTypes type, byte[] data)
        {
            Command command = new Command(type, data);
            byte[] outData = CommandParser.ToBytes(command);

            await SendData(outData);
        }

        private async Task<bool> SendData(byte[] bytes)
        {
            if (Connected)
            {
                try
                {
                    var stream = client.GetStream();
                    await stream.WriteAsync(bytes, 0, bytes.Length);
                    stream.Flush();
                    return true;
                }
                catch (Exception) { }
            }

            return false;
        }

        void CloseSocket()
        {
            try { client?.GetStream()?.Close(); } catch (Exception) { }
            try { client?.Close(); } catch (Exception) { }
            client = null;
        }

        private bool OpenSocket()
        {
            IPHostEntry host = Dns.GetHostEntry(hostname);
            for(int i = 0; i < host.AddressList.Length; ++i)
            {
                try
                {
                    client = new TcpClient(host.AddressList[i].AddressFamily);
                    if (client.ConnectAsync(host.AddressList[i], port).Wait(2000))
                    {
                        return true;
                    }
                }
                catch (Exception)
                {
                }
            }

            CloseSocket();
            return false;
        }

        private void Runner()
        {
            if(!OpenSocket())
            {
                CurrentState = State.Error;
                return;
            }

            CurrentState = State.Success;

            byte[] bytes = new byte[0x2000];
            while (Connected)
            {
                HandleData();

                var stream = client.GetStream();
                if (stream.DataAvailable)
                {
                    try
                    {
                        int length = stream.Read(bytes, 0, (int)bytes.Length);
                        lock (dataQueue)
                        {
                            for (int i = 0; i < length; ++i) dataQueue.Enqueue(bytes[i]);
                        }
                    }
                    catch (Exception)
                    {

                    }
                }
                else
                {
                    Thread.Sleep(50);
                }
            }

            CloseSocket();
            dataQueue.Clear();
            CurrentState = State.Error;
        }

        private void HandleData()
        {
            lock (dataQueue)
            {
                while (dataQueue.Count > 8)
                {
                    int length = CommandParser.ParseBodyLength(dataQueue.ToArray(), 0, dataQueue.Count);
                    CommandTypes type = CommandParser.ParseCommandType(dataQueue.Skip(4).Take(4).ToArray());

                    if (length <= dataQueue.Count)
                    {
                        // Skip header bytes
                        for (int i = 0; i < 8; ++i) dataQueue.Dequeue();

                        byte[] data = new byte[length];
                        for (int i = 0; i < length; ++i)
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
        }

        private void DispatchData(CommandTypes type, byte[] bytes)
        {
            if (type == CommandTypes.Request)
            {
                try
                {
                    RequestBase request = RequestSerializer.Deserialize<RequestBase>(bytes);
                    ResponseBase response = null;

                    lock (handlers)
                    {
                        if (handlers.ContainsKey(request.GetType()))
                        {
                            response = handlers[request.GetType()](request);
                        }
                    }

                    if (response == null)
                    {
                        response = request.CreateResponse();
                    }

                    Task.Run(async () => await SendResponse(response));
                }
                catch(Exception)
                {

                }
            }
            else if (type == CommandTypes.Response)
            {
                try
                {
                    ResponseBase response = RequestSerializer.Deserialize<ResponseBase>(bytes);

                    lock (responses)
                    {
                        if (responses.ContainsKey(response.Token))
                        {
                            responses[response.Token] = response;
                        }
                    }
                }
                catch (Exception)
                {

                }
            }
        }

        public void Receive<T>(Action<T> handler) where T : RequestBase
        {
            lock (handlers)
            {
                handlers[typeof(T)] = (data) =>
                {
                    if (data is T obj)
                    {
                        handler(obj);
                    }

                    return null;
                };
            }
        }

        public void Receive<T>(Func<T, ResponseBase> handler) where T : RequestBase
        {
            lock (handlers)
            {
                handlers[typeof(T)] = (data) =>
                {
                    if (data is T obj)
                    {
                        return handler(obj);
                    }

                    return null;
                };
            }
        }
    }
}