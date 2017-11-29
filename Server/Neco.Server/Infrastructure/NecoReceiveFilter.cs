using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperSocket.Common;
using SuperSocket.Facility.Protocol;
using SuperSocket.SocketBase.Protocol;


namespace Neco.Server.Infrastructure
{
    public class NecoReceiveFilter : FixedHeaderReceiveFilter<BinaryRequestInfo>
    {

        public NecoReceiveFilter() : base(8) { }

        protected override int GetBodyLengthFromHeader(byte[] header, int offset, int length)
        {
            //nameBytesCount = 4;
            var bytes = new[]
                {
                    header[offset + 4],
                    header[offset + 5],
                    header[offset + 6],
                    header[offset + 7]
                };
            var parsedLength = BitConverter.ToInt32(bytes, 0);
            return parsedLength;
        }

        protected override BinaryRequestInfo ResolveRequestInfo(ArraySegment<byte> header, byte[] bodyBuffer, int offset, int length)
        {
            var headerArr = header.Array.Skip(header.Offset).Take(4).ToArray();
            var commandId = BitConverter.ToInt32(headerArr, 0) + "";
            var bodyArr = bodyBuffer.CloneRange(offset, length);
            return new BinaryRequestInfo(commandId, bodyArr);
        }
    }
}
