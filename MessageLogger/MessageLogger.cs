using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
