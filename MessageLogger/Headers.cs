using System.Collections.Generic;
using System.Text;

namespace Umbe.Web.MessageLogger
{
    public class Headers : Dictionary<string, string>
    {

        public override string ToString()
        {
            var headers = new StringBuilder();

            foreach (var header in this)
            {
                headers.AppendLine($"{header.Key}: {header.Value}");
            }

            return headers.ToString();
        }
    }
}
