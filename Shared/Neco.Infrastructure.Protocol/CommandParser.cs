using System;
using System.Collections.Generic;

namespace Neco.Infrastructure.Protocol
{
    /// <summary>
    /// protocol:
    /// length (4) + command type (4) + request body
    /// </summary>
    public static class CommandParser
    {
        private const int NumLengthBytes = 4;
        private const int NumTypeBytes = 4;

        public static byte[] ToBytes(Command command)
        {
            return ToBytes(command.Type, command.Data);
        }

        private static byte[] ToBytes(CommandTypes name, byte[] data)
        {
            var result = new List<byte>(data.Length + NumLengthBytes + NumTypeBytes);
            result.AddRange(BitConverter.GetBytes(data.Length));
            result.AddRange(CommandTypeToBytes(name));
            result.AddRange(data);
            return result.ToArray();
        }
        
        public static CommandTypes ParseCommandType(byte[] bytes)
        {
            try
            {
                if (bytes.Length != NumTypeBytes)
                    throw new ArgumentException();

                var commandIndex = BitConverter.ToInt32(bytes, 0);
                return (CommandTypes)commandIndex;
            }
            catch (Exception)
            {
                return CommandTypes.Unknown;
            }
        }

        public static int ParseBodyLength(byte[] header, int offset, int length)
        {
            var bytes = new[]
                {
                    header[offset + NumTypeBytes], 
                    header[offset + NumTypeBytes + 1],
                    header[offset + NumTypeBytes + 2],
                    header[offset + NumTypeBytes + 3]
                };
            var parsedLength = BitConverter.ToInt32(bytes, 0);
            return parsedLength;
        }

        private static IEnumerable<byte> CommandTypeToBytes(CommandTypes name)
        {
            var bytes = new List<byte>(BitConverter.GetBytes((int) name));
            if (bytes.Count > NumTypeBytes)
                throw new InvalidOperationException();
            
            while (bytes.Count < NumTypeBytes)
            {
                bytes.Insert(0, 0);
            }
            return bytes;
        }
    }
}
