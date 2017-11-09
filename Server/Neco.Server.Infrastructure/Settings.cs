using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neco.Server.Application.Interfaces;

namespace Neco.Server.Infrastructure
{
    public class Settings : ISettings
    {
        public bool IsServerBusy { get; private set; }
        public string ServerAddress { get { return "localhost"; } }
        public int ServerPort { get { return 9000; } }
        public string Subject { get { return "Welcome to Neco Chat!"; } }
    }
}
