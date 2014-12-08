using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umbe.Web.MessageLogger
{
    public class Headers : Dictionary<string, string>
    {

        public override string ToString()
        {
            var headers = new StringBuilder();

            foreach (var header in this)
            {
                headers.AppendFormat("{0}: {1}\n", header.Key, header.Value);
            }

            return headers.ToString();
        }
    }
}
