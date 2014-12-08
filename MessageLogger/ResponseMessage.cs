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
            var v = string.Format("{0}.{1}", Version.Major, Version.Minor);

            var message = string.Format(@"HTTP/{0} {1}
{2}
{3}", v, Status, Headers, Content);

            return message;
        }

    }
}
