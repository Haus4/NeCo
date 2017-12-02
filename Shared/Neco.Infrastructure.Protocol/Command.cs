namespace Neco.Infrastructure.Protocol
{
    public class Command
    {
        public CommandTypes Type { get; private set; }
        public byte[] Data { get; private set; }

        public Command(CommandTypes type, byte[] data)
        {
            Type = type;
            Data = data;
        }
    }
}