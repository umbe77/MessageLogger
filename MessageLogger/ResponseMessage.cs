using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umbe.Web.MessageLogger
{
    public class ResponseMessage : Message
    {
        public int Status { get; set; }

        public override string ToString()
        {
            var v = $"{Version.Major}.{Version.Minor}";

            var message = $@"HTTP/{v} {Status}
{Headers}
{Content}";

            return message;
        }

    }
}
