using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperSocket.Common;
using SuperSocket.Facility.Protocol;
using SuperSocket.SocketBase.Protocol;
using Neco.Infrastructure.Protocol;


namespace Neco.Server.Infrastructure
{
    public class NecoReceiveFilter : FixedHeaderReceiveFilter<BinaryRequestInfo>
    {

        public NecoReceiveFilter() : base(8) { }

        protected override int GetBodyLengthFromHeader(byte[] header, int offset, int length)
        {
            var bytes = header.Skip(offset).Take(4).ToArray();
            var parsedLength = BitConverter.ToInt32(bytes, 0);
            return parsedLength-length;
        }

        protected override BinaryRequestInfo ResolveRequestInfo(ArraySegment<byte> header, byte[] bodyBuffer, int offset, int length)
        {
            //nameBytesCount = 4;
            var headerArr = header.Array.Skip(header.Offset+4).Take(4).ToArray();
            var commandType = (CommandTypes)BitConverter.ToInt32(headerArr, 0);
            var commandName = commandType.ToString();
            var bodyArr = bodyBuffer.CloneRange(offset, length);
            return new BinaryRequestInfo(commandName, bodyArr);
        }
    }
}
