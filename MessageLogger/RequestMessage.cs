using System;
using System.Text;

namespace Umbe.Web.MessageLogger
{
    public class RequestMessage : Message
    {
        public string Method { get; set; }
        public Uri RequestUri { get; set; }

        public override string ToString()
        {
            var v = $"{Version.Major}.{Version.Minor}";

            var message =new StringBuilder();
            message.AppendLine($"{Method} {RequestUri.AbsolutePath} HTTP/{v}");
            message.AppendLine($"{Headers}");
            message.AppendLine($"{Content}");
            return message.ToString();
        }

    }
}
