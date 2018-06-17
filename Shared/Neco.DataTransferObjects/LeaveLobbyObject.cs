using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neco.DataTransferObjects
{
    public class LeaveLobbyRequest : RequestBase
    {
        public byte[] PublicKey { get; set; }
    }

    public class LeaveLobbyResponse : ResponseBase
    {
        public bool Success { get; set; }
    }
}
