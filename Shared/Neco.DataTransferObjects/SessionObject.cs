using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neco.DataTransferObjects
{
    public class SessionRequest : RequestBase
    {
        public byte[] MemberKey { get; set; }
    }

    public class SessionResponse : ResponseBase
    {
        public bool Success { get; set; }
        public byte[] PublicKey { get; set; }
    }
}
