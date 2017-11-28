namespace Neco.Infrastructure.Protocol
{
    public enum CommandTypes
    {
        Unknown = -1,
        Echo = 0,
        Ping,
        Response,
        Request,
        Data,

        Session,
        Message
    }
}
