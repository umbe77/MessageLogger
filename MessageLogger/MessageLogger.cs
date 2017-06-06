using System.Web.Http;

namespace Umbe.Web.MessageLogger
{
    public static class MessageLogger
    {
        public static void Register(HttpConfiguration conf)
        {
            conf.MessageHandlers.Add(new MessageLoggerHandler());
        }
    }
}
