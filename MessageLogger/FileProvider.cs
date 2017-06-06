using System.Text;
using System.IO;
using System.Web.Hosting;

namespace Umbe.Web.MessageLogger
{

    public enum FileDirection
    {
        Request,
        Response
    }

    public class FileProvider : MessageLoggerProvider
    {

        private string _storagePath;

        public override string Name => "FileProvider";

        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
        {
            base.Initialize(name, config);

            _storagePath =  HostingEnvironment.MapPath(config["storagePath"]);
        }

        public override void SerializeRequest(RequestMessage message)
        {
            var fileName = GetStoragePath(message.CorrelationId, FileDirection.Request);

            using (var fs = new FileStream(fileName, FileMode.CreateNew, FileAccess.Write, FileShare.ReadWrite))
            {
                var msg = message.ToString();
                var msgRaw = Encoding.UTF8.GetBytes(msg);

                fs.Write(msgRaw, 0, msgRaw.Length);

            }

        }
        
        public override void SerializeResponse(ResponseMessage message)
        {
            var fileName = GetStoragePath(message.CorrelationId, FileDirection.Response);

            using (var fs = new FileStream(fileName, FileMode.CreateNew, FileAccess.Write, FileShare.ReadWrite))
            {
                var msg = message.ToString();
                var msgRaw = Encoding.UTF8.GetBytes(msg);

                fs.Write(msgRaw, 0, msgRaw.Length);

            }
        }

        private string GetStoragePath(string correlationId, FileDirection direction)
        {
            return Path.Combine(_storagePath, $"{correlationId}_{direction}.txt");
        }
    }
}
