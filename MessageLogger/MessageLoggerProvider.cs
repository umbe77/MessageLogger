using System.Configuration.Provider;


namespace Umbe.Web.MessageLogger
{
    public abstract class MessageLoggerProvider : ProviderBase
    {
        public abstract void SerializeRequest(RequestMessage message);

        public abstract void SerializeResponse(ResponseMessage message);
    }

    public class MessageLoggerCollection : ProviderCollection
    {
        public new MessageLoggerProvider this[string name] => base[name] as MessageLoggerProvider;
    }
}
