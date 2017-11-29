using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperSocket.SocketBase;
using SuperSocket.SocketEngine;
using SuperSocket.SocketBase.Config;

namespace Neco.Server.Infrastructure
{
    public class InfrastructureInitializer
    {
        private readonly Settings _settings;
        private readonly SocketServer _necoSocketServer;
        public InfrastructureInitializer(Settings settings)
        {
            _settings = settings;
            _necoSocketServer = new SocketServer();
        }

        public async void Init()
        {
            var rootConfig = new RootConfig();
            var serverConfig = new ServerConfig
            {
                Port = _settings.ServerPort,
                Ip = "Any",
                MaxConnectionNumber = 2000,
                Mode = SocketMode.Tcp,
                Name = "NecoServer",
                DisableSessionSnapshot = true,
                LogAllSocketException = false,
                LogBasicSessionActivity = false,
                LogCommand = false,
            };
            var setuped = _necoSocketServer.Setup(rootConfig, serverConfig);
            var started = _necoSocketServer.Start();
        }
    }
}
