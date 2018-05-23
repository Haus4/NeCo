using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neco.Server
{
    public class Settings
    {
        public bool IsServerBusy { get; private set; }
        public string ServerAddress => ConfigurationSettings.AppSettings["ServerAddress"]; 
        public int ServerPort => Int32.Parse(ConfigurationSettings.AppSettings["ServerPort"]);
        public string Subject => ConfigurationSettings.AppSettings["Subject"]; 

        public String IpAddress => ConfigurationSettings.AppSettings["IpAddress"]; 

    }
}
