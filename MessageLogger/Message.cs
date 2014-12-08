using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umbe.Web.MessageLogger
{
    public class Message
    {
        public string CorrelationId { get; set; }
        public string Content { get; set; }
        public Headers Headers { get; set; }
        public Version Version { get; set; }
    }
}
