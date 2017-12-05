using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neco.DataTransferObjects
{
    public class MessageRequest : RequestBase
    {
        public byte[] Message { get; set; }
        public byte[] Signature { get; set; }
    }

    public class MessageResponse : ResponseBase
    {
        public byte[] Signature { get; set; }
        public bool Success { get; set; }
        public String Message { get; set; }
    }
}
