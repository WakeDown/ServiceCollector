using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlServerCe;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnmpScanner.Models
{
    public class Request
    {
        public string SerialNum;
        public string Oid;
        public string OidType;
        public string Value;
        public DateTime DateRequest;
        public bool Swap;
        public string Host;
        

        public Request() { }

        public Request(string serialNum, string oid, string value, string oidType, DateTime dateRequest)
        {
            SerialNum = serialNum;
            Oid = oid;
            Value = value;
            OidType = oidType;
            DateRequest = dateRequest;
        }

        public void Save()
        {
            string command = "insert into requests (oid, value, oid_type, date_request, swap, dattim1, host, serial_num) values(@oid, @value, @oid_type, @date_request, 0, @dattim1, @host, @serial_num)";
            SqlCeParameter pSerialNum = new SqlCeParameter() { ParameterName = "serial_num", Value = SerialNum, SqlDbType = SqlDbType.NVarChar };
            SqlCeParameter pOid = new SqlCeParameter() { ParameterName = "oid", Value = Oid, SqlDbType = SqlDbType.NVarChar };
            SqlCeParameter pValue = new SqlCeParameter() { ParameterName = "value", Value = Value, SqlDbType = SqlDbType.NVarChar };
            SqlCeParameter pOidType = new SqlCeParameter() { ParameterName = "oid_type", Value = OidType, SqlDbType = SqlDbType.NVarChar };
            SqlCeParameter pDateRequest = new SqlCeParameter() { ParameterName = "date_request", Value = DateRequest, SqlDbType = SqlDbType.DateTime };
            SqlCeParameter pDattim1 = new SqlCeParameter() { ParameterName = "dattim1", Value = DateTime.Now, SqlDbType = SqlDbType.DateTime };
            SqlCeParameter pHost = new SqlCeParameter() { ParameterName = "host", Value = Host, SqlDbType = SqlDbType.NVarChar };
            

            Db.ExequteSqlCommand(command, pSerialNum, pOid, pValue, pOidType, pDateRequest, pDattim1, pHost);
        }

        public static Request[] GetList()
        {
            string query = "select oid, value, oid_type, host, date_request, serial_num from requests";

            DataTable dt = Db.ExecuteSqlQuery(query);

            List<Request> lst = new List<Request>();

            foreach (DataRow row in dt.Rows)
            {
                lst.Add(new Request(row["serial_num"].ToString(), row["oid"].ToString(), row["value"].ToString(), row["oid_type"].ToString(), Convert.ToDateTime(row["date_request"].ToString())) { Host = row["host"].ToString() });
            }

            return lst.ToArray();
        }

        internal static void DeleteAll()
        {
            string command = "delete requests";

            Db.ExequteSqlCommand(command);
        }
    }
}
