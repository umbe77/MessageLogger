using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umbe.Web.MessageLogger
{
    public class RequestMessage : Message
    {
        public string Method { get; set; }
        public Uri RequestUri { get; set; }

        public override string ToString()
        {
            var v = $"{Version.Major}.{Version.Minor}";

            var message = $@"{Method} {RequestUri.AbsolutePath} HTTP/{v}
{Headers}
{Content}";

            return message;
        }

    }
}
