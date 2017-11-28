namespace Neco.Infrastructure.Protocol
{
    public class Command
    {
        public CommandTypes Name { get; private set; }
        public byte[] Data { get; private set; }

        public Command(CommandTypes name, byte[] data)
        {
            Name = name;
            Data = data;
        }
    }
}