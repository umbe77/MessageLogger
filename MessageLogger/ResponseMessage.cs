using System.Text;

namespace Umbe.Web.MessageLogger
{
    public class ResponseMessage : Message
    {
        public int Status { get; set; }

        public override string ToString()
        {
            var v = $"{Version.Major}.{Version.Minor}";

            var message = new StringBuilder();
            message.AppendLine($"HTTP/{v} {Status}");
            message.AppendLine($"{Headers}");
            message.AppendLine($"{Content}");
            return message.ToString();
        }

    }
}
