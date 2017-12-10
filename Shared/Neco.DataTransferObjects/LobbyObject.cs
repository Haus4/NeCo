using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neco.DataTransferObjects
{
    public class LobbyRequest : RequestBase
    {
        public String LobbyId { get; set; }
        public byte[] PublicKey { get; set; }
        public byte[] Signature { get; set; }
        public Double Latitude { get; set; }
        public Double Longitude { get; set; }
    }

    public class LobbyResponse : ResponseBase
    {
        public bool Success { get; set; }
        public String LobbyId { get; set; }
        public Double Latitude { get; set; }
        public Double Longitude { get; set; }
        public long Range { get; set; }
        public List<byte[]> MemberPublicKeys { get; set; }
    }
}
