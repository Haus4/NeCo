using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neco.DataTransferObjects
{
    public class SessionRequest : RequestBase
    {
        public byte[] PublicKey { get; set; }
        public byte[] Signature { get; set; }
        public long Latitude { get; set; }
        public long Longitude { get; set; }
    }

    public class SessionResponse : ResponseBase
    {
        public bool Success { get; set; }
        public String Message { get; set; }
        public byte[] Signature { get; set; }
    }
}
