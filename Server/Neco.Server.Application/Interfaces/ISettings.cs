using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neco.Server.Application.Interfaces
{
    public interface ISettings
    {
        bool IsServerBusy { get; }
        string ServerAddress { get; }
        int ServerPort { get; }
        string Subject { get; }
    }
}
