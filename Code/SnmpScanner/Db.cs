using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlServerCe;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnmpScanner
{
    public partial class Db
    {
        //private static SqlCeConnection conn { get { return new SqlCeConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString); } }

        private static string connStr = @"Data Source=|DataDirectory|\App_Data\SnmpDevicePool.sdf;Password='1qazXSW@'";//String.Format("Data Source={0};Password='1qazXSW@'", Path.GetFullPath("App_data\\SnmpDevicePool.sdf"));

        public Db()
        {

        }

        public static void ExequteSqlCommand(string command, params SqlCeParameter[] sqlParams)
        {
            using (var conn = new SqlCeConnection(connStr))
            using (var cmd = new SqlCeCommand(command, conn))
            {
                try
                {
                    SqlCeEngine engine = new SqlCeEngine(conn.ConnectionString);
                    engine.Upgrade(conn.ConnectionString);
                }
                catch
                {
                }

                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddRange(sqlParams);

                conn.Open();
                cmd.ExecuteReader();
            }
        }

        public static DataTable ExecuteSqlQuery(string query, params SqlCeParameter[] sqlParams)
        {
            var dt = new DataTable();

            using (var conn = new SqlCeConnection(connStr))
            using (var cmd = new SqlCeCommand(query, conn))
            {
                try
                {
                    SqlCeEngine engine = new SqlCeEngine(conn.ConnectionString);
                    engine.Upgrade(conn.ConnectionString);
                }
                catch
                {
                }

                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddRange(sqlParams);

                conn.Open();
                dt.Load(cmd.ExecuteReader());
            }

            return dt;
        }
    }
}
