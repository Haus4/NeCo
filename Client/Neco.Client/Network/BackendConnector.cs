using System;
using System.Net.Sockets;
using System.Text;

namespace Neco.Client.Network
{
    public class BackendConnector
    {
        private TcpClient client;

        public BackendConnector(String host)
        {
            var separator = host.LastIndexOf(':');
            if (separator < 0) throw new ArgumentException();

            int port = Convert.ToInt32(host.Substring(separator + 1));
            String server = host.Substring(0, separator);

            client = new TcpClient(server, port);
        }

        public void Send(String data)
        {
            Byte[] bytes = Encoding.ASCII.GetBytes(data);

            NetworkStream stream = client.GetStream();
            stream.Write(bytes, 0, bytes.Length);
        }

        public void Receive()
        {
            // TODO: Use event handlers to dispatch incoming data
        }
    }
}
