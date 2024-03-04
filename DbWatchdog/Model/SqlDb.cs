using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace DbWatchdog.Model
{
    public record MonitorType
    {
        string Id { get; set;}
        string Desp { get; set;}
    }

    class SqlDb
    {
        private string connectionString;

        public SqlDb(string connectionString)
        {
            this.connectionString = connectionString;
        }

        
        public async Task<IEnumerable<MonitorType>> GetMonitorTypes()
        { 
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<MonitorType>("SELECT * FROM [dbo].[monitorType]");
        }
        
    }
}
