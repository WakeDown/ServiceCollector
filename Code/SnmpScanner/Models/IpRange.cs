using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlServerCe;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnmpScanner.Models
{
    class IpRange
    {
        public int Id;
        public string IpFrom;
        public string IpTo;

        public string Name
        {
            get
            {
                return String.Format("{0} - {1}", IpFrom, IpTo);
            }
        }

        public IpRange(string ipFrom, string ipTo)
        {
            IpFrom = ipFrom;
            IpTo = ipTo;
        }

        public IpRange(int id, string ipFrom, string ipTo)
        {
            Id = id;
            IpFrom = ipFrom;
            IpTo = ipTo;
        }

        public void Save()
        {
            string command = "insert into ip_ranges (ip_from, ip_to, dattim1) values(@ip_from, @ip_to, @dattim1)";
            SqlCeParameter pIpFrom = new SqlCeParameter() { ParameterName = "ip_from", Value = IpFrom, SqlDbType = SqlDbType.NVarChar };
            SqlCeParameter pIpTo = new SqlCeParameter() { ParameterName = "ip_to", Value = IpTo, SqlDbType = SqlDbType.NVarChar };
            SqlCeParameter pDattim1 = new SqlCeParameter() { ParameterName = "dattim1", Value = DateTime.Now, SqlDbType = SqlDbType.DateTime };

            Db.ExequteSqlCommand(command, pIpFrom, pIpTo, pDattim1);
        }

        //public static void Delete(string ipFrom, string ipTo)
        //{
        //    string command = "delete top 1 from ip_ranges where ip_from = @ip_from and ip_to = @ip_to";
        //    SqlCeParameter pIpFrom = new SqlCeParameter() { ParameterName = "ip_from", Value = ipFrom, SqlDbType = SqlDbType.NVarChar };
        //    SqlCeParameter pIpTo = new SqlCeParameter() { ParameterName = "ip_to", Value = ipTo, SqlDbType = SqlDbType.NVarChar };

        //    Db.ExequteSqlCommand(command, pIpFrom, pIpTo);
        //}

        public static void Delete(int id)
        {
            string command = "delete ip_ranges where id = @id";
            SqlCeParameter pId = new SqlCeParameter() { ParameterName = "id", Value = id, SqlDbType = SqlDbType.Int };

            Db.ExequteSqlCommand(command, pId);
        }

        public static IpRange[] GetList()
        {
            string query = "select id, ip_from, ip_to from ip_ranges";

            DataTable dt = Db.ExecuteSqlQuery(query);

            List<IpRange> lst = new List<IpRange>();

            foreach (DataRow row in dt.Rows)
            {
                lst.Add(new IpRange((int)row["id"], row["ip_from"].ToString(), row["ip_to"].ToString()));
            }

            return lst.ToArray();
        }
    }
}
