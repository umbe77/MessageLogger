using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;

namespace Umbe.Web.MessageLogger
{
    public class SqlServerProvider : MessageLoggerProvider
    {

        private string _connectionString;

        private string _tableName = "RawLogging";
        private const string InitSql = @"
IF NOT EXISTS(SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '{0}')
BEGIN
    CREATE TABLE {0}
    (
        CorrelationId nvarchar(100) NOT NULL PRIMARY KEY,
        RequestDate datetimeoffset NOT NULL,
        RawRequest nvarchar(max) NOT NULL,
        ResponseDate datetimeoffset NULL,
        RawResponse nvarchar(max) NULL
    )
END
";

        public override void Initialize(string name, NameValueCollection config)
        {
            base.Initialize(name, config);
            _connectionString = config["connectionString"];

            if (!string.IsNullOrWhiteSpace(config["tableName"]))
            {
                _tableName = config["tableName"];
            }

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand(string.Format(InitSql, _tableName), conn);
                cmd.ExecuteNonQuery();
            }
        }

        public override void SerializeRequest(RequestMessage message)
        {
            var sql = $@"INSERT INTO {_tableName}
(CorrelationId, RequestDate, RawRequest)
VALUES
(@CorrelationId, SYSDATETIMEOFFSET(), @Raw)";
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand(sql, conn);
                var p = new SqlParameter("@CorrelationId", SqlDbType.NVarChar, 100) {Value = message.CorrelationId};
                cmd.Parameters.Add(p);

                p = new SqlParameter("@Raw", SqlDbType.NVarChar, -1) {Value = message.ToString()};
                cmd.Parameters.Add(p);

                cmd.ExecuteNonQuery();
            }
        }

        public override void SerializeResponse(ResponseMessage message)
        {
            var sql = $@"UPDATE {_tableName}
SET ResponseDate = SYSDATETIMEOFFSET(),
    RawResponse = @Raw
WHERE CorrelationId = @CorrelationId";

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand(sql, conn);
                var p = new SqlParameter("@CorrelationId", SqlDbType.NVarChar, 100) { Value = message.CorrelationId };
                cmd.Parameters.Add(p);

                p = new SqlParameter("@Raw", SqlDbType.NVarChar, -1) { Value = message.ToString() };
                cmd.Parameters.Add(p);

                cmd.ExecuteNonQuery();
            }
        }
    }
}
