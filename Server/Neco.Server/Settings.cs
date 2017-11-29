using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neco.Server
{
    public class Settings
    {
        public bool IsServerBusy { get; private set; }
        public string ServerAddress { get { return "localhost"; } }
        public int ServerPort { get { return 9000; } }
        public string Subject { get { return "Welcome to Neco Chat!"; } }
    }
}
