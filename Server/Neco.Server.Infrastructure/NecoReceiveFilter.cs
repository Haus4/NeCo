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
    public class NecoReceiveFilter : FixedHeaderReceiveFilter<StringRequestInfo>
    {

        public NecoReceiveFilter() : base(8) { }

        protected override int GetBodyLengthFromHeader(byte[] header, int offset, int length)
        {
            var nameBytesCount = 4;
            var bytes = new[]
                {
                    header[offset + nameBytesCount],
                    header[offset + nameBytesCount + 1],
                    header[offset + nameBytesCount + 2],
                    header[offset + nameBytesCount + 3]
                };
            var parsedLength = BitConverter.ToInt32(bytes, 0);
            return parsedLength;
        }

        protected override StringRequestInfo ResolveRequestInfo(ArraySegment<byte> header, byte[] bodyBuffer, int offset, int length)
        {
            var headerArr = header.Array.Skip(header.Offset).Take(4).ToArray();
            var headerStr = System.Text.Encoding.Default.GetString(headerArr);
            var bodyArr = bodyBuffer.CloneRange(offset, length);
            var bodyStr = System.Text.Encoding.Default.GetString(bodyArr);
            return new StringRequestInfo(headerStr, bodyStr, null);
        }
    }
}
