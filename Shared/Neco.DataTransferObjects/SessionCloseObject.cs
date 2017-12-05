using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neco.DataTransferObjects
{
    public class SessionCloseRequest : RequestBase
    {
        public byte[] Signature { get; set; }
    }

    public class SessionCloseResponse : ResponseBase
    {
        public bool Success { get; set; }
    }
}
