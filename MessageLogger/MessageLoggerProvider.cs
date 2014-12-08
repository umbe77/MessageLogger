using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration.Provider;
using System.Threading.Tasks;
using System.Net.Http;


namespace Umbe.Web.MessageLogger
{
    public abstract class MessageLoggerProvider : ProviderBase
    {
        public abstract void SerializeRequest(RequestMessage message);

        public abstract void SerializeResponse(ResponseMessage message);
    }

    public class MessageLoggerCollection : ProviderCollection
    {
        new public MessageLoggerProvider this[string name]
        {
            get
            {
                return base[name] as MessageLoggerProvider;
            }
        }
    }
}
